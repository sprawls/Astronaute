using UnityEngine;

/// <summary>
/// Gravity Objects that modify the behaviour of objects that enter its radius.
/// </summary>
public abstract class GravityModifier : GravityObject {

    // Use this for initialization
    protected virtual void Start() {
        base.Start();
        if (gameObject.layer != 9) gameObject.layer = 8; //If not player layer, set gravityModifier Layer
    }

    /// <summary>
    /// Method called every FixedUpdate by objects affected by this gravity modifier
    /// </summary>
    /// <param name="planet">object affected by this modifier</param>
    /// <returns>force to apply to object</returns>
    public abstract Vector2 ApplyGravityForce(GravityBody planet);

}
