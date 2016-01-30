using UnityEngine;
using System.Collections;

public class Wormhole : MonoBehaviour {

    //Editor Variables
    [Header("Visuals")]
    [SerializeField] public ParticleSystem FX_Warp_In;
    [SerializeField] public ParticleSystem FX_Warp_Out;
    [Header("Worhole Properties")]
    [SerializeField] private Wormhole targetWormhole;  
    [SerializeField] private float EXPULSION_FORCE = 4f;
    [SerializeField] private LayerMask GravityLayerMask;

    [HideInInspector] public Vector3 warpPosition { get; private set; }
 
    

	// Use this for initialization
	void Start () {
        warpPosition = transform.position;
	}
	
    /// <summary>
    /// Warps gravity body to the target wormhole
    /// </summary>
    /// <param name="bodyToWarp">Gravity Body to warp</param>
    /// <param name="bodyGameObject">GameOBject of gravity body (GO could be parent of gravityBody) </param>
    private void WarpToTarget(GravityBody bodyToWarp, GameObject bodyGameObject) {
        Vector2 randomDirection = Random.insideUnitCircle;
        //Disable collision and teleport
        bodyToWarp.DisableCollision(2f);
        ToggleTrailRenderer(bodyGameObject, false);
        bodyGameObject.transform.position = targetWormhole.warpPosition + (Vector3)randomDirection * 0f;
        ToggleTrailRenderer(bodyGameObject, true);
        //Apply Force
        bodyToWarp.Velocity = randomDirection * EXPULSION_FORCE;
        bodyToWarp.ResetGravityObjects();
        //Spawn Visuals
        Instantiate(FX_Warp_In, transform.position, Quaternion.identity);
        Instantiate(FX_Warp_Out, targetWormhole.transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Starts Warp if collision with graviy body
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other) {
        if ( ((1 << other.gameObject.layer) & GravityLayerMask) != 0 && other.gameObject != gameObject) {
            GravityBody gravityBodyScript = other.gameObject.GetComponentInChildren<GravityBody>();
            if (gravityBodyScript != null) {
                if(gravityBodyScript.collisionEnabled == true) WarpToTarget(gravityBodyScript, other.gameObject);
            }
        }
    }

    /// <summary>
    /// Toggles on and off the trailrenderer components of given object
    /// </summary>
    /// <param name="GO">Objet to check for TrailRenderers </param>
    /// <param name="active">make active or inactive </param>
    private void ToggleTrailRenderer(GameObject GO, bool active) {
        TrailRenderer[] tRenderers = GO.GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer tr in tRenderers) {
            tr.enabled = active;
        }
    }



}
