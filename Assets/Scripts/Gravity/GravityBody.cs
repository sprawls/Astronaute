using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Object that is affected by all gravity modifier
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class GravityBody : GravityObject {

    //Getters
    public float Weigth { get { return _rigidBody.mass; } private set { _rigidBody.mass = value; } }
    public Vector2 Velocity { get { return _rigidBody.velocity; } set { _rigidBody.velocity = value; } }
    [SerializeField] private Vector2 START_VELOCITY = Vector2.zero;

    
    private Rigidbody2D _rigidBody; 
    private int gravityModifierLayerMask;
    private int playerLayerMask;
    private int gravityBodyLayerMask;
    //Collision
    private float timeNoCollision = 0f;
    public bool collisionEnabled { get; private set; }

    public List<GravityModifier> currentGravObjects = new List<GravityModifier>();


    override protected void Start () {
        base.Start();

        gravityModifierLayerMask = 8; //Get gravity modifier layermask
        playerLayerMask = 9; //Get player layermask
        gravityBodyLayerMask = 10; //Get gravity body layermask
        if (gameObject.layer != playerLayerMask) gameObject.layer = 10;
        collisionEnabled = true;

        SetupRigidbody();
        SetupCollider();
    }

    void Update() {
        CheckCollisionEnabled();
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

    #region Components Initialization

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
    #endregion

    #region MonoBehaviour Collision Methods
    /// <summary>
    /// Manages the objects that are within its radius by adding them to the currentGrabOjects list when they collide
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == gravityModifierLayerMask && other.gameObject != gameObject) {
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
        if (other.gameObject.layer == gravityModifierLayerMask) {
            GravityModifier gravityObjScript = other.gameObject.GetComponent<GravityModifier>();
            if (gravityObjScript != null) {
                if(currentGravObjects.Contains(gravityObjScript)) currentGravObjects.Remove(gravityObjScript);
            }
        }
    }
    #endregion

    #region Advanced Collision Functions

    /// <summary>
    /// Resets the list of current gravity modifier. Used in various cases (ex: When teleporting through wormhole)
    /// </summary>
    public void ResetGravityObjects() {
        currentGravObjects = new List<GravityModifier>();
    }


    /// <summary>
    /// Checks if collision needs to be reactivated
    /// </summary>
    private void CheckCollisionEnabled() {
        if(!collisionEnabled) {
            timeNoCollision -= Time.deltaTime;
            if(timeNoCollision <= 0f) {
                collisionEnabled = true;
                timeNoCollision = 0f;
                _collider.enabled = true;
            }
        }
    }

    /// <summary>
    /// Disable the collision for a small amount of time (still affected by gravity)
    /// </summary>
    /// <param name="time">time in seconds </param>
    public void DisableCollision(float time) {
        if (timeNoCollision < time) timeNoCollision = time;
        collisionEnabled = false;
         _collider.enabled = false;
    }



    #endregion
}
