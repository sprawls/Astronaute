using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GravityForceZone : GravityModifier {

    
    [SerializeField] private Vector2 FORCE = Vector2.zero;
    [SerializeField] private bool INDEPENDANT_FROM_MASS = false;
    private float gravityForceFactor = 0.1f; //Force applied by gravityForceZone is multiplied by this

	// Use this for initialization
	void Start () {
        base.Start();
	}

    /// <summary>
    /// Method called every FixedUpdate by objects affected by this gravity modifier
    /// </summary>
    /// <param name="planet">object affected by this modifier</param>
    /// <returns>force to apply to object</returns>
    public override Vector2 ApplyGravityForce(GravityBody planet) {
        //Calculate acceleration based on planet's weigth
        Vector2 acc = GRAVITYCONSTANT * gravityForceFactor * FORCE;
        if (!INDEPENDANT_FROM_MASS) acc /= planet.Weigth;
        return acc;
    }
}
