using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Coil : MonoBehaviour
{
    public int turnCount = 4;
    public float height = 4f;
    public float radius = 1f;

    public Color coilStart = Color.white;
    public Color coilEnd = Color.white;

    public bool torus = false;

    const float TAU = 6.28318530718f;
    const int POINTS_PER_TURN = 32;

    private void OnDrawGizmos()
    {

         Vector2 AngToDir(float angRad) => new Vector2(Mathf.Cos(angRad), Mathf.Sin(angRad));


        int pointCount = turnCount * POINTS_PER_TURN;
        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (pointCount - 1f);
            float THeight = i / (pointCount - 1f);
            points[i] = torus ? GetTorusCoilPoint(t, height, radius, turnCount) : GetLinearCoilPoint(THeight, height, radius, turnCount);
        }

        Handles.DrawAAPolyLine(points);
        if (torus)
        {
            Handles.DrawWireDisc(default, Vector3.forward, height / TAU);
        }


        for (int i = 0; i < points.Length -1; i++)
        {
            float t = i / (pointCount - 1f);
            Handles.color = Color.Lerp(coilStart, coilEnd, t);
        Handles.DrawAAPolyLine(points[i], points[i+1]);
        }

        Handles.color = Color.white;

     Vector3 GetLinearCoilPoint(float t, float height, float radius, int turnCount)
    {
            float TWinding = t * turnCount;

            float angRad = TWinding * TAU;
            Vector3 pt = AngToDir(angRad) * radius;
            pt.z = t * height;

            return pt;
        }

         Vector3 GetTorusCoilPoint(float t, float circunference, float minorRadius, int turnCount)
        {
            float majorRadius = circunference / TAU;
            Vector3 corePt   = AngToDir(t * TAU) * majorRadius;

            float TWinding = t * turnCount;
            float angRad = TWinding * TAU;
            Vector2 localPt = AngToDir(angRad) * minorRadius;

            Vector3 xLocal = corePt.normalized;
            Vector3 yLocal = Vector3.forward; // Z axis

            return corePt + localPt.x * xLocal + localPt.y * yLocal;
        }

    }
}
