using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Brain : MonoBehaviour
{

    public Vector3 velocity;
    public GameObject circleRenderer;

    private GameObject displacementCirlce;

    private Circle_Renderer circle_Renderer;
    public Rigidbody rb;
    private Vector3 displacementCircle;
    private Vector3 displacementOrigin;
    private Vector3 displacementTarget;
    private Vector3 displacementTargetUpdated;

    // Start is called before the first frame update
    void Awake()
    {
        /*circle_Renderer = GameObject.Find("Sensory Radius").GetComponent<Circle_Renderer>();*/
        rb = GetComponent<Rigidbody>();
        SetVelocity();

        displacementCirlce = Instantiate(circleRenderer, DisplacementCircle(), Quaternion.identity);
        displacementCircle = DisplacementCircle();


        displacementOrigin = new Vector3(transform.position.x + displacementCircle.x, transform.position.y + displacementCircle.y, transform.position.z + displacementCircle.z);
        displacementTarget = DisplacementVector();
        Debug.Log("DisplacementOrigin: " + displacementOrigin);
        Debug.Log("DisplacementTarget: " + displacementTarget);

        Move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        velocity = rb.velocity;
        Vector3 wanderForce = Wander();
        rb.AddForce(wanderForce.x * 0.2f, 0, wanderForce.z * 0.2f);

        //Debug.Log("Displacement x: " + displacementCircle.x);
        displacementOrigin = new Vector3(transform.position.x + displacementCircle.x, transform.position.y + displacementCircle.y, transform.position.z + displacementCircle.z);
        displacementTargetUpdated = new Vector3(transform.position.x + displacementTarget.x, transform.position.y + displacementTarget.y, transform.position.z + displacementTarget.z);



        displacementCirlce.GetComponent<Circle_Renderer>().CreateSegments(displacementOrigin.x, displacementOrigin.z);
    }


    void SetVelocity() {
        rb.velocity = new Vector3(5.0f, 0.0f, 0.0f) * 5.0f * Time.deltaTime;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        //Gizmos.DrawLine(displacementOrigin, displacementTarget);
        Gizmos.DrawSphere(displacementOrigin, 0.05f);

        // Draw displacement target point
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(displacementTargetUpdated, 0.05f);

        // Draw wander target point
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Wander(), 0.05f);
    }

    // Create a new circle infront of the boid to represent the displacement force
    Vector3 DisplacementCircle() {
        Vector3 circleCenter = rb.velocity;
        return circleCenter;
    }

    Vector3 DisplacementVector() {
        // Obtain random angle
        int randAngle = Random.Range(0, 360);

        Debug.Log("Random Angle:" + randAngle);

        // Create a displacement Vector and provide a random angle
        Vector3 displacementVector = new Vector3(0.75f * Mathf.Cos(Mathf.Deg2Rad * randAngle), 0.0f, 0.75f * Mathf.Sin(Mathf.Deg2Rad * randAngle));

        return displacementVector;
    }

    // Create a new force oiginating from displacement circle
    Vector3 Wander() {

        Vector3 wanderVector = rb.velocity + displacementTargetUpdated;

        return wanderVector;
    }

    void Move() {

        
        rb.velocity = rb.velocity + Wander();
        //circle_Renderer.CreateSegments(transform.position.x, transform.position.z);
    }

}
