using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle_Renderer : MonoBehaviour
{

    
    public float radius = 2.0f;
    public int points = 40;
    // Start is called before the first frame update
    void Start()
    {
        CreateLineRenderer();
        //CreateSegments(transform.parent.position.x, transform.parent.position.z);
    }

    private void CreateLineRenderer() {
        // Instanciate a new lineRenderer and initialize accordingly
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        
    }

    public void CreateSegments(float x, float z) {

        float angle = 0.0f;
        LineRenderer lineRenderer;

        if (gameObject.GetComponent<LineRenderer>() != null) {

            lineRenderer = gameObject.GetComponent<LineRenderer>();
        } else { 
            CreateLineRenderer();
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.loop = true;
            lineRenderer.alignment = LineAlignment.TransformZ;
            lineRenderer.positionCount = points;
        }
        // Given desired angle, radius and segment count, draw a circle
        for (int i = 0; i < points; i++) {
            lineRenderer.SetPosition(i, new Vector3(
                                            (x + radius * Mathf.Cos(Mathf.Deg2Rad * angle)),
                                            0.0f,
                                            (z + radius * Mathf.Sin(Mathf.Deg2Rad * angle))
                                        )
                                    );
            angle += (360f / points);
        }
    }
}
