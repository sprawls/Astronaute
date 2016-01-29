using UnityEngine;


/// <summary>
/// Gravity Modifier that attracts objects affected
/// </summary>
public class Attractor : GravityModifier {

    /// <summary> multiplier of force applied from planet's velocity compared to force towards planet </summary>
    [SerializeField] private float VELOCITY_FORCE_MULT = 0.1f; ///
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

    public override Vector2 ApplyGravityForce(GravityBody planet) {
        //Apply force toward Attractor
        Vector2 forceDirectionAtt = transform.position - planet.transform.position;  //Get force direction
        Vector2 accAtt = GRAVITYCONSTANT * (forceDirectionAtt.normalized * Force) / (Mathf.Max(forceDirectionAtt.sqrMagnitude, MIN_DIST_MULTIPLIER) * planet.Weigth); //Calculate acceleration based on attractor's force and distance
        //Apply force toward planet's current velocity
        Vector2 forceDirectionVel = planet.Velocity;  //Get force direction
        Vector2 accVel = VELOCITY_FORCE_MULT * accAtt.magnitude * forceDirectionVel.normalized;
        accVel = Vector2.zero;

        return accAtt + accVel;
    }

}
