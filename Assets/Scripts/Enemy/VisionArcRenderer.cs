using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisionArcRenderer : MonoBehaviour
{
    private VisionDetector vision;

    [Header("Arc Settings")]
    [SerializeField] private int segments = 40;
    

    [Header("Occlusion")]
    [SerializeField] private LayerMask occluders; 
    [SerializeField] private float skin = 0.01f; 
    

    private LineRenderer lr;

    

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        lr.loop = false;

        vision = GetComponent<VisionDetector>();
        

        
    }

    

    private void LateUpdate()
    {
        if (vision == null) return;

        
        DrawOccludedCone();
        
    }

    private void DrawOccludedCone()
    {
        Vector3 origin = transform.position;

        Vector2 forward = vision.Forward2D;
        if (forward.sqrMagnitude < 0.0001f) forward = Vector2.right;
        forward.Normalize();

        float range = vision.DetectionRange;
        float angle = vision.VisionAngle;
        float halfRad = (angle * 0.5f) * Mathf.Deg2Rad;

        
        lr.positionCount = segments + 3;
        lr.SetPosition(0, origin);

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float current = Mathf.Lerp(-halfRad, +halfRad, t);

            Vector2 dir = Rotate2D(forward, current);

            
            float dist = range;
            if (occluders.value != 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, range, occluders);
                if (hit.collider != null)
                    dist = Mathf.Max(0f, hit.distance - skin);
            }

            Vector3 p = origin + (Vector3)(dir * dist);
            lr.SetPosition(i + 1, p);
        }

        lr.SetPosition(segments + 2, origin);
    }

    private void DrawOccludedMesh()
    {
       

        Vector3 origin = transform.position;

        Vector2 forward = vision.Forward2D;
        if (forward.sqrMagnitude < 0.0001f) forward = Vector2.right;
        forward.Normalize();

        float range = vision.DetectionRange;
        float angle = vision.VisionAngle;
        float halfRad = (angle * 0.5f) * Mathf.Deg2Rad;

        
        int vertexCount = segments + 2;
        Vector3[] verts = new Vector3[vertexCount];
        int[] tris = new int[segments * 3];

        verts[0] = origin;

        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float current = Mathf.Lerp(-halfRad, +halfRad, t);

            Vector2 dir = Rotate2D(forward, current);

            float dist = range;
            if (occluders.value != 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, range, occluders);
                if (hit.collider != null)
                    dist = Mathf.Max(0f, hit.distance - skin);
            }

            verts[i + 1] = origin + (Vector3)(dir * dist);
        }

        for (int i = 0; i < segments; i++)
        {
            int tri = i * 3;
            tris[tri] = 0;
            tris[tri + 1] = i + 1;
            tris[tri + 2] = i + 2;
        }

        
    }

    

    private Vector2 Rotate2D(Vector2 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}
