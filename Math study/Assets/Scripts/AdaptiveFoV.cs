using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveFoV : MonoBehaviour
{
    public Transform[] points;

    private void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();
        Vector2 camDir = cam.transform.forward;
        Vector2 camPos = cam.transform.position;

        float lowestDot = float.MaxValue;
        Vector2 ptOutermost = default;
        foreach (Transform ptTf in points)
        {
            Vector2 pt = (Vector2)ptTf.position - camPos;
            Vector2 dirToPt = pt.normalized;
            float dot = Vector2.Dot(camDir, dirToPt);
            if (dot < lowestDot)
            {
                lowestDot = dot;
                ptOutermost = ptTf.position;
            }
        }

        float angRad = Mathf.Acos(lowestDot);

        cam.fieldOfView = angRad * 2 * Mathf.Rad2Deg;

        Gizmos.DrawLine(camPos, ptOutermost);

    }

}
