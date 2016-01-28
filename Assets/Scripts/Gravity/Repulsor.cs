using UnityEngine;

/// <summary>
/// Gravity modifier that repulses Objects affected by gravity that enter it's radius
/// </summary>
public class Repulsor : Attractor {

    
    public override Vector2 ApplyGravityForce(GravityBody planet) {
        //Get force direction
        Vector2 forceDirection = transform.position - planet.transform.position;
        //Calculate acceleration based on attractor's force and distance
        Vector2 acc = GRAVITYCONSTANT * (forceDirection.normalized * Force) / (Mathf.Max(forceDirection.sqrMagnitude, MIN_DIST_MULTIPLIER) * planet.Weigth);
        acc *= -1f; //inverse it
        return acc;
    }
}
