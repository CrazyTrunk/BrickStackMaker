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
        if (!isMoving) // Chỉ xử lý vuốt nếu nhân vật không đang di chuyển
        {
            currentDirection = direction;
            targetPosition = GetNextPoint(currentDirection);
            isMoving = true;
        }
    }
    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition) // Đã đến điểm đích
        {
            isMoving = false;
            currentDirection = Direct.None;
        }
    }

    private Vector3 GetNextPoint(Direct direction)
    {
        Vector3 directionVector = Vector3.zero;
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
                return Vector3.zero;
        }
        RaycastHit hit;
        Vector3 nextPoint = Vector3.zero;
        for (int i = 1; i < 100; i++)
        {
            if (Physics.Raycast((transform.position + directionVector * i) + Vector3.up * 2, Vector3.down, out hit, 10f, layerBrick))
            {

                nextPoint = hit.collider.transform.position;
            }
            else
            {

                break;
            }
        }
        return nextPoint;
    }


}
