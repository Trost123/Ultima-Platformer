// Smooth towards the target

using System;
using UnityEngine;
using System.Collections;
     
public class DampCamera2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - target.position;
    }

    void Update()
    {
        if (target)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(cameraOffset);
     
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}