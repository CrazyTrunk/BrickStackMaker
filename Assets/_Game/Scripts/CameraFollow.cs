using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset;   
    public float smoothSpeed = 5f;
    void Start()
    {
        target = FindObjectOfType<Player>()?.transform;
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