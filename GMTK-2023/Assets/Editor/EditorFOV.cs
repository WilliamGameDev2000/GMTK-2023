using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(FieldOfView))]
public class EditorFOV : Editor
{

    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 fovA = fov.GetAngleDirection(-fov.angle / 2, false);
        Vector3 fovB = fov.GetAngleDirection(fov.angle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + fovA * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + fovB * fov.radius);

        Handles.color = Color.red;
        foreach (Transform targets in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, targets.position);
        }
    }

}
