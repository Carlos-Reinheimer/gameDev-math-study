using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RadiusTrigger : MonoBehaviour
{
    public float radius = 1f;
    public Transform ptTf;

    void OnDrawGizmos()
    {
        Vector2 center = transform.position;
        Vector2 objPosition = ptTf.position;

        float distance = Vector2.Distance(center, objPosition);
        bool isInside = distance <= radius;

        Handles.color = isInside ? Color.green : Color.red;
        Handles.DrawWireDisc(center, new Vector3(0, 0, 1), radius);
    }
}
