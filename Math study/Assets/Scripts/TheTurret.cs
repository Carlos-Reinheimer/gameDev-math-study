using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheThurret : MonoBehaviour
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
            Matrix4x4 WorldToTurret = turretToWorld.inverse;

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

            Gizmos.color = Color.red;
            for (int i = 0; i < pts.Length; i++)
            {
                Vector3 worldPt = turretToWorld.MultiplyPoint3x4(pts[i]);

                Gizmos.DrawSphere(pts[i], 0.075f);
            }
        }
    }
}
