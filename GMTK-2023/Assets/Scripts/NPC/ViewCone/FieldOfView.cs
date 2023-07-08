using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;

    [Range(0,360)]
    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public float MeshRes;
    public int edgeResIterations;
    public float edgeDistThreshold;
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindTargetsRoutine", .3f);
    }


    IEnumerator FindTargetsRoutine(float dealy)
    {
        while(true)
        {
            yield return new WaitForSeconds(dealy);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        Collider[] colliderArray = Physics.OverlapSphere(transform.position, radius, targetMask);

        for (int i = 0; i < colliderArray.Length; i++)
        {
            Transform target = colliderArray[i].transform;

            Vector3 targetDir = (target.position - transform.position).normalized;


            if (Vector3.Angle(transform.forward, targetDir) < angle/2)
            {
                float targetDistance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, targetDir, targetDistance, obstructMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(angle * MeshRes);

        float stepAngleSize = angle / stepCount;

        List<Vector3> ViewPoints = new List<Vector3>();

        ViewCastInfo oldCast = new ViewCastInfo();

        for (int i = 0; i < stepCount; i++)
        {
            float current_angle = (transform.eulerAngles.y - angle / 2 + stepAngleSize * i);
            ViewCastInfo castInfo = ViewCast(current_angle);
            ViewPoints.Add(castInfo.point);

            if(i > 0)
            {
                bool exceeedsEdgeThrshld = Mathf.Abs(oldCast.length - castInfo.length) > edgeDistThreshold;
                if(oldCast.hit != castInfo.hit || (oldCast.hit && castInfo.hit && exceeedsEdgeThrshld))
                {
                    EdgeInfo edge = FindEdge(oldCast, castInfo);

                    if (edge.PointA != Vector3.zero)
                    {
                        ViewPoints.Add(edge.PointA);
                    }
                    if (edge.PointB != Vector3.zero)
                    {
                        ViewPoints.Add(edge.PointB);
                    }
                }
            }

            oldCast = castInfo;
        }

        int vertex_count = ViewPoints.Count + 1;

        Vector3[] verticies = new Vector3[vertex_count];
        int[] triangles = new int[(vertex_count - 2) * 3];

        verticies[0] = Vector3.zero;
        for (int i = 0; i < vertex_count - 1; i++)
        {
            verticies[i + 1] = transform.InverseTransformPoint( ViewPoints[i]);

            if (i < vertex_count - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = verticies;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = GetAngleDirection(globalAngle, true);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, radius, obstructMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }

        return new ViewCastInfo(false, transform.position + dir * radius, radius, globalAngle);
    }

    EdgeInfo FindEdge(ViewCastInfo MinCast, ViewCastInfo MaxCast)
    {
        float minAngle = MinCast.angle;
        float maxAngle = MaxCast.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newCast = ViewCast(angle);

            bool exceeedsEdgeThrshld = Mathf.Abs(MinCast.length - MaxCast.length) > edgeDistThreshold;

            if (newCast.hit == MinCast.hit && !exceeedsEdgeThrshld)
            {
                minAngle = angle;
                minPoint = newCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    public Vector3 GetAngleDirection(float angleInDegrees, bool isGlobal)
    {
        if(!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void LateUpdate()
    {
        DrawFOV();
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float length;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _length, float _angle)
        {
            hit = _hit;
            point = _point;
            length = _length;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 PointA;
        public Vector3 PointB;

        public EdgeInfo(Vector3 A, Vector3 B)
        {
            PointA = A;
            PointB = B;
        }
    }
}
