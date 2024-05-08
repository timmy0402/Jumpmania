using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Transform healthBar;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Debug.Log(healthBar.position);
        Vector3 desiredPosition = player.position + offset;
        Vector3 desiredHealthBar = player.position + new Vector3(-232, 164, 0);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 smoothedHealthBarPostion = Vector3.Lerp(healthBar.position, desiredHealthBar, smoothSpeed);
        transform.position = smoothedPosition;
        healthBar.position = smoothedHealthBarPostion;
    }
}
