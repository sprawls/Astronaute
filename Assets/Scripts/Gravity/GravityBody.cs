using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Object that is affected by all gravity modifier
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : GravityObject {

    //Getters
    public float Weigth { get { return _rigidBody.mass; } private set { _rigidBody.mass = value; } }
    public Vector2 Velocity { get { return _rigidBody.velocity; } private set { _rigidBody.velocity = value; } }
    [SerializeField] private Vector2 START_VELOCITY = Vector2.zero;

    
    private Rigidbody2D _rigidBody; 
    private int gravityLayerMask;
    private int playerLayerMask;
    public List<GravityModifier> currentGravObjects = new List<GravityModifier>();


    override protected void Start () {
        base.Start();
        gravityLayerMask = 8; //Get gravity layermask
        playerLayerMask = 9; //Get player layermask
        SetupRigidbody();
        SetupCollider();
    }

    void FixedUpdate() {
        Vector2 acceleration = Vector2.zero;
        foreach (GravityModifier gravObj in currentGravObjects) {
            Vector2 accelChange = gravObj.ApplyGravityForce(this);
            //Debug.Log("force : " + accelChange + "       by gravObj " + gravObj.name);
            acceleration += accelChange;
            
        }
        _rigidBody.velocity += acceleration * Time.fixedDeltaTime;
    }

    void SetupRigidbody() {
        //Get Component References
        _rigidBody = GetComponent<Rigidbody2D>();
        if (_rigidBody == null) _rigidBody = gameObject.AddComponent<Rigidbody2D>();
        //Set Collider values
        _rigidBody.gravityScale = 0;
        //Set Starting Velocity
        _rigidBody.velocity = START_VELOCITY;
    }

    /// <summary>
    /// Setup the _collider
    /// </summary>
    protected virtual void SetupCollider() {
        //Get Component References
        _collider = GetComponent<CircleCollider2D>();
        if (_collider == null) _collider = gameObject.AddComponent<CircleCollider2D>();
        //Set _collider values
        _collider.radius = radius;
        //Set trigger to false if we're not player. 
        if (gameObject.layer != playerLayerMask) _collider.isTrigger = false;
        else _collider.isTrigger = true;
    }

    /// <summary>
    /// Manages the objects that are within its radius by adding them to the currentGrabOjects list when they collide
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == gravityLayerMask && other.gameObject != gameObject) {
            GravityModifier gravityObjScript = other.gameObject.GetComponent<GravityModifier>();
            if (gravityObjScript != null) {
                currentGravObjects.Add(gravityObjScript);
            }
        }
    }

    /// <summary>
    /// Manages the objects that are within its radius by removing them of the currentGrabOjects list when they leave
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == gravityLayerMask) {
            GravityModifier gravityObjScript = other.gameObject.GetComponent<GravityModifier>();
            if (gravityObjScript != null) {
                if(currentGravObjects.Contains(gravityObjScript)) currentGravObjects.Remove(gravityObjScript);
            }
        }
    }


}
