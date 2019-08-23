using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    public Rigidbody rb;
    // public static Vector3 onUnitSphere; 
    public float displacementRadius;
    public int forceTimeDelta;
    public float maxSpeed;

    private Vector3 disTargetCenter;
    private Vector3 newForceCenter;

    void Start() {
        Debug.Log("WANDER-Start: " + rb);

        //setForce();
        float randAngle = Random.Range(0f, 359f);
        disTargetCenter = setDisplacementTarget(randAngle);
        Debug.Log("-------------------------------------");
    }

    void FixedUpdate() {
        // Debug.Log( "WANDER-FixedUpdate: " );

        if (Time.time % forceTimeDelta == 0) {
            float newAngle = Random.Range(0f, 359f);
            disTargetCenter = setDisplacementTarget(newAngle);
        }

        // Gets a random X, Y and Z axis direction on a unit sphere
        // Vector3 randomOnSphereDirection = Random.onUnitSphere;

        // now apply this random target direction as a Force for the X and Z axis only
        rb.AddForce((disTargetCenter.x * 10),                                       // X axis
                          0,														// eliminates Y axis (vertical) compoent of Force
                          (disTargetCenter.z * 10), ForceMode.VelocityChange);      // Z axis

        // now clamp the sphere updated displacement velocity in all axis
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, 0.0f, maxSpeed),			// X axis
                                                  Mathf.Clamp(rb.velocity.y, 0.0f, maxSpeed),			// Y axis (vertical)
                                                  Mathf.Clamp(rb.velocity.z, 0.0f, maxSpeed));			// Z axis
    }

    void OnDrawGizmos() {
        // Debug.Log( "WANDER-OnDrawGizmos: displacementRadius " + displacementRadius + " displacedCircleCentre " + disCircleCenter + " displacedTargetCentre " + disTargetCenter);

        // Draw force target sphere in RED and in relation to the rigidbody sphere
        Gizmos.color = Color.red;
        newForceCenter.x = (transform.position.x + disTargetCenter.x);
        newForceCenter.y = (transform.position.y);
        newForceCenter.z = (transform.position.z + disTargetCenter.z);
        Gizmos.DrawSphere(newForceCenter, 0.05f);
    }

    // Returns a Vector3 indicating the new displacement target, given a new angle 
    Vector3 setDisplacementTarget(float angle) {
        Debug.Log("WANDER-SetDisplacementTarget: angle = " + angle);

        float displacementXAxis = Mathf.Cos(Mathf.Deg2Rad * angle);
        float displacementZAxis = Mathf.Sin(Mathf.Deg2Rad * angle);
        Debug.Log("WANDER-SetDisplacementTarget: X axis = " + displacementXAxis + " Z axis = " + displacementZAxis);

        return new Vector3((displacementRadius * displacementXAxis),					// calculate the X axis component of the new DisplacementTarget vector
                                         displacementRadius,										// Y axis (vertical) remains untouched for  the new DisplacementTarget vector
                                        (displacementRadius * displacementZAxis));					// calculate the Z axis component of the new DisplacementTarget vector
    }

    void setForce() {
        Debug.Log("WANDER-setForce: " + (Vector3.right * 10));
        rb.AddForce((Vector3.right * 10));
    }
}