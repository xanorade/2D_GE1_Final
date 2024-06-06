using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // the target object the camera will follow
    public Transform target;

    // the offset between the camera and the target
    public Vector3 offset;

    // update is called once per frame
    void Update()
    {
        // move the camera to the target position plus the offset
        transform.position = target.position + offset;
    }
}
