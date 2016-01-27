using UnityEngine;
using System.Collections;


/// <summary>
/// Gravity Modifier that attracts objects affected
/// </summary>
public class Attractor : GravityModifier {

    [SerializeField] private float FORCE = 1f;
    public float Force { get { return FORCE; } private set { FORCE = value; }}
    protected float MIN_DIST_MULTIPLIER = 2f;

    void Start () {
        base.Start();
        SetupCollider();
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
        _collider.isTrigger = true;
    }

    public override Vector2 ApplyGravityForce(Attracted planet) {
        //Get force direction
        Vector2 forceDirection = transform.position - planet.transform.position; 
        //Calculate acceleration based on attractor's force and distance
        Vector2 acc = GRAVITYCONSTANT * (forceDirection.normalized * Force) / (Mathf.Max(forceDirection.sqrMagnitude, MIN_DIST_MULTIPLIER) * planet.Weigth);
        return acc;
    }

}
