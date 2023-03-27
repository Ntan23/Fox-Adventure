using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [Range(1,10)]
    [SerializeField] private float smoothFactor;
    [SerializeField] private Vector3 minValue;
    [SerializeField] private Vector3 maxValue;
    
    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x), Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),Mathf.Clamp(targetPosition.z,minValue.z,maxValue.z));
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}

