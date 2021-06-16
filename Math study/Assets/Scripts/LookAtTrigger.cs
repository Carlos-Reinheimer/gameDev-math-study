using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTrigger : MonoBehaviour
{
    [Range(0f, 1f)]
    public float preciseness = 0.5f;

    public Transform objTf;

    void OnDrawGizmos()
    {
        Vector2 center = transform.position;
        Vector2 playerPosition = objTf.position;
        Vector2 playerLookDirection = objTf.right; // x axis - conversional

        Vector2 playerToTriggerDirection = (center - playerPosition).normalized;

        float lookness = Vector2.Dot(playerToTriggerDirection, playerLookDirection);

        bool isLooking = lookness >= preciseness;

        Gizmos.color = isLooking ? Color.green : Color.red;
        Gizmos.DrawLine(playerPosition, playerPosition + playerToTriggerDirection);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerPosition, playerPosition + playerLookDirection); 
    }
}
