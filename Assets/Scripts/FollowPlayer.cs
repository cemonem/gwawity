using System;
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float screenRectX = 12f;
    public float screenRectY = 7.5f;
    private void LateUpdate()
    {
        Vector2 cameraRelative = gameObject.transform.position-player.transform.position;
        float x = Mathf.Clamp(cameraRelative.x, -screenRectX, screenRectX);
        float y = Mathf.Clamp(cameraRelative.y, -screenRectY, screenRectY);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z) + player.transform.position;
    }
}
