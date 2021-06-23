using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcMeshArea : MonoBehaviour
{
    public Mesh mesh;
    public float area = 0f;

    // ----- Execute every time the value changes -----
    private void OnValidate()
    {
        Vector3[] verts = mesh.vertices;
        int[] tris = mesh.triangles;

        area = 0f;
        for (int i = 0; i < tris.Length; i+=3)
        {
            Vector3 a = verts[tris[i]];
            Vector3 b = verts[tris[i+1]];
            Vector3 c = verts[tris[i+2]];

            area += Vector3.Cross(b - a, c - a).magnitude;
        }
        area /= 2;
    }
}
