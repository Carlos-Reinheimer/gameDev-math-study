using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TheTurret : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 headPos = transform.position;
        Vector3 lookDir = transform.forward;

        void DrawRay(Vector3 p, Vector3 dir) => Gizmos.DrawLine(p, p + dir);

        if (Physics.Raycast(headPos, transform.forward, out RaycastHit hit))
        {
            Vector3 hitPos = hit.point;
            Vector3 up = hit.normal;
            Vector3 right = Vector3.Cross(up, lookDir).normalized;
            Vector3 forward = Vector3.Cross(right, up);

            Quaternion turretRot = Quaternion.LookRotation(forward, up);
            Matrix4x4 turretToWorld = Matrix4x4.TRS(hitPos, turretRot, Vector3.one);
            // Matrix4x4 WorldToTurret = turretToWorld.inverse;

            // from assignment
            Vector3[] pts = new Vector3[]
            {
                new Vector3(1, 0, 1), // bottom 4 positions
                new Vector3(-1, 0, 1),
                new Vector3(-1, 0, -1),
                new Vector3(1, 0, -1),
                new Vector3(1, 2, 1), // top 4 positions
                new Vector3(-1, 2, 1),
                new Vector3(-1, 2, -1),
                new Vector3(1, 2, -1)
            };

            Gizmos.matrix = turretToWorld;

            Gizmos.color = Color.red;
            for (int i = 0; i < pts.Length; i++)
            {
                Gizmos.DrawSphere(pts[i], 0.075f);
            }

            Handles.color = Color.white;
            Handles.DrawAAPolyLine(headPos, hitPos);

            Gizmos.color = Color.red;
            DrawRay(Vector3.zero, Vector3.right);
            Gizmos.color = Color.green  ;
            DrawRay(Vector3.zero, Vector3.up);
            Gizmos.color = Color.blue;
            DrawRay(Vector3.zero, Vector3.forward);
        } else
        {
            Gizmos.color = Color.red;
            DrawRay(headPos, lookDir);
        }
    }
}
