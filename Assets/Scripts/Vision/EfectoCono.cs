using UnityEngine;

public class EfectoCono : MonoBehaviour
{
    [SerializeField]
    private MeshFilter filter = null;

    [SerializeField]
    private LayerMask visionConeLayerMask;

    [SerializeField]
    private float fov = 90f, viewDistance = 50f;

    [SerializeField]
    private int rayCount = 50;

    private Mesh mesh;
    private float startingAngle;

    private void Start()
    {
        mesh = new Mesh();
        filter.mesh = mesh;

        startingAngle = GetAngleFromVectorFloat(transform.right) + fov / 2f;
    }

    private void LateUpdate()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = transform.InverseTransformPoint(transform.position);

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D =
                Physics2D.Raycast(transform.position, GetVectorFromAngle(angle), viewDistance, visionConeLayerMask);
            if (raycastHit2D.collider == null)
            {
                // No hit
                vertex = transform.position + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = transform.InverseTransformPoint(vertex);

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(transform.position, Vector3.one * 1000f);

        
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}