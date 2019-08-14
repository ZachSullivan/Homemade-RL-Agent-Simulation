using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    public Rigidbody rb;
    public float displacementRadius;
    public float displacementDistance;
    public float maxSpeed;

    private Vector3 disCircleCenter;
    private Vector3 disTargetCenter;

    private Vector3 disTargetOrigin;
    void Start() {
        setForce();
        setDisplacementCircle();

        disTargetOrigin = setDisplacementTarget();
    }

    void FixedUpdate() {
        Debug.Log(GetRandomValue(0, 1));
        if ((Time.time % 5 == 0) && (GetRandomValue(0, 1) == 1)) {
            Debug.Log("True!");
            disTargetOrigin = setDisplacementTarget();
            Debug.Log(disTargetOrigin);
        }

        Vector3 disCircleOrigin = setDisplacementCircle();
        disCircleCenter = new Vector3(Mathf.Clamp(disCircleOrigin.x, 0.0f, displacementDistance) + this.transform.position.x, disCircleOrigin.y, Mathf.Clamp(disCircleOrigin.z, 0.0f, displacementDistance) + this.transform.position.z);

        
        disTargetCenter = new Vector3(disTargetOrigin.x + disCircleCenter.x, disTargetOrigin.y, disTargetOrigin.z + disCircleCenter.z);

        rb.AddForce(disTargetCenter.x * 1, 0, disTargetCenter.z * 1);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, 0.0f, maxSpeed), Mathf.Clamp(rb.velocity.y, 0.0f, maxSpeed), Mathf.Clamp(rb.velocity.z, 0.0f, maxSpeed));


    }

    int GetRandomValue(int min, int max) {
        return Random.Range(min, max);
    }

    void OnDrawGizmos() {

        // Draw displacement sphere
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(disCircleCenter, displacementRadius);

        // Draw velocity vector into displacment sphere
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, disCircleCenter);

        // Draw displacement target sphere
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(disTargetCenter, 0.05f);
    }

    // Returns a Vector3 origin for the displacement circle
    Vector3 setDisplacementCircle() {
        return new Vector3(transform.position.x + displacementDistance, displacementRadius, 0);
    }

    // Returns a Vector3 indicating the displacement target, given a random angle 
    Vector3 setDisplacementTarget() {
        float randAngle = Random.Range(90.0f, -90.0f);

        Debug.Log("Target Angle:" + randAngle);

        return new Vector3(displacementRadius * Mathf.Cos(Mathf.Deg2Rad * randAngle), displacementRadius, displacementRadius * Mathf.Sin(Mathf.Deg2Rad * randAngle));
    }

    void setForce() {
        rb.AddForce(Vector3.right * 1);
    }

}
