using UnityEngine;


public class Camera2DFollow : MonoBehaviour {

    public Transform target;
    [SerializeField] private float LERP_SPEED = 0.25f;
    [SerializeField] private float INTERP_SPEED = 10f;
    [SerializeField] private float Z_POSITION = -10f;

	void Start () {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 targetDirection = target.position - transform.position;
        Vector2 targetPosition = (Vector2)transform.position + (targetDirection.normalized * targetDirection.magnitude * INTERP_SPEED * Time.deltaTime);
        //Debug.Log(targetPosition);
        targetPosition = Vector2.Lerp(transform.position, targetPosition, LERP_SPEED);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, Z_POSITION);
        
	}
}
