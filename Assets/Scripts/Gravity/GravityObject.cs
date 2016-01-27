using UnityEngine;
using System.Collections;

/// <summary>
/// Basic Gravity Object. Base class used to create objects affected by gravity forces.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class GravityObject : MonoBehaviour {

    static public float GRAVITYCONSTANT = 10f; 

    [SerializeField] protected int attractionLevel = 10; //used to determine what object affter what other gravityObj
    [SerializeField] protected float radius = 5f;

    protected CircleCollider2D _collider;


    protected virtual void Start() {
        
    }


}
