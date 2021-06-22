using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTransformation : MonoBehaviour
{
    // ------ local space point to be transformed ------
    public Vector2 localSpacePoint;

    // ------ world space point to be transformed ------
    public Transform localObjTransform;
    public Vector2 worldSpacePoint;
    void OnDrawGizmos()
    {
        Vector2 objPos = transform.position;
        Vector2 right = transform.right;
        Vector2 up = transform.up;


        Vector2 LocalToWorld(Vector2 localPt)
        {
            Vector2 worldOffset = right * localPt.x + up * localPt.y;

            return (Vector2)transform.position + worldOffset;
        }

        Vector2 WorldToLocal(Vector2 worldPt)
        {
            Vector2 relPoint = worldPt - objPos;
            float x  = Vector2.Dot(relPoint, right);
            float y = Vector2.Dot(relPoint, up);

            return new Vector2(x, y);
        }

        // ------ position in world space ------
        //Vector2 worldSpacePoint = LocalToWorld(localSpacePoint);


        DrawBasisVectors(objPos, right, up);

        // ------ Draw basis vectors in world space ------
        DrawBasisVectors(Vector2.zero, Vector2.right, Vector2.up);

        // ------ Draw worldSpacePoint in worldSpace ------
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(worldSpacePoint, 0.1f);

        localObjTransform.localPosition = WorldToLocal(worldSpacePoint);
    }

    void DrawBasisVectors(Vector2 pos, Vector2 right, Vector2 up)
    {
        // ------ All gizmos functions draw at world space ------
        Gizmos.color = Color.red;
        Gizmos.DrawRay(pos, right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(pos, up);
        Gizmos.color = Color.white;
    }
}
