using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset;   
    public float smoothSpeed = 2f;
    void Start()
    {
        offset = transform.position - target.position;
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smoothSpeed);

    }
}