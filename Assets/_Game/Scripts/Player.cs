using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Direct currentDirection = Direct.None;
    [SerializeField] private LayerMask layerBrick;
    private bool isMoving = false;
    private Vector3 targetPosition;

    private void OnEnable()
    {
        SwipeDetection.OnSwipe += HandleMovePlayer;
    }
    private void OnDisable()
    {
        SwipeDetection.OnSwipe -= HandleMovePlayer;
    }
    private void Update()
    {
        if (isMoving)
        {
            MovePlayer();
        }

    }
    private void HandleMovePlayer(Direct direction)
    {
        if (!isMoving  && direction != Direct.None) // Chỉ xử lý vuốt nếu nhân vật không đang di chuyển
        {
            currentDirection = direction;
            targetPosition = GetNextPoint(currentDirection);
            Debug.Log($"Next Point {targetPosition}");
            isMoving = true;
        }
    }
    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position ==  targetPosition)
        {
            isMoving = false;
            currentDirection = Direct.None;
        }
    }
    private Vector3 GetNextPoint(Direct direction)
    {
        Vector3 directionVector = Vector3.zero;
        RaycastHit hit;
        switch (direction)
        {
            case Direct.Forward:
                directionVector = Vector3.forward;
                break;
            case Direct.Back:
                directionVector = Vector3.back;
                break;
            case Direct.Right:
                directionVector = Vector3.right;
                break;
            case Direct.Left:
                directionVector = Vector3.left;
                break;
            case Direct.None:
                return transform.position;
        }
        Debug.DrawRay(transform.position, directionVector, Color.red, 1000);
        if (Physics.Raycast(transform.position, transform.TransformDirection(directionVector), out hit, Mathf.Infinity, layerBrick))
        {
            directionVector = hit.collider.transform.position;
        }
        else
        {
            directionVector = transform.position;
        }

        return directionVector;
    }

}
