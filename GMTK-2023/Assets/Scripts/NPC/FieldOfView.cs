using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;

    [Range(0,360)]
    public float angle;

    [HideInInspector]
    public Vector3 Headpos;

    public LayerMask targetMask;
    public LayerMask obstructMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public float MeshRes;
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

            Vector3 targetDir = (target.position - Headpos).normalized;
            if(Vector3.Angle(-transform.right, targetDir) < angle/2)
            {
                float targetDistance = Vector3.Distance(Headpos, target.position);

                if (!Physics.Raycast(Headpos, targetDir, targetDistance, obstructMask))
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

        for (int i = 0; i < stepCount; i++)
        {
            float current_angle = (angle / 2 - transform.eulerAngles.y + stepAngleSize * i) + 180;
            ViewCastInfo castInfo = ViewCast(current_angle);
            ViewPoints.Add(castInfo.point);
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

        if(Physics.Raycast(Headpos, dir, out hit, radius, obstructMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }

        return new ViewCastInfo(false, Headpos + dir * radius, radius, globalAngle);
    }

    public Vector3 GetAngleDirection(float angleInDegrees, bool isGlobal)
    {
        if(!isGlobal)
        {
            angleInDegrees -= transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    void FixedUpdate()
    {
        DrawFOV();

        Headpos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
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
}
