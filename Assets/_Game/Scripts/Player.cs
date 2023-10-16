using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private LayerMask layerBrick;
    [SerializeField] private LayerMask layerDrop;

    [SerializeField] public GameObject stackBrickParent;
    [SerializeField] public Transform skin;
    [SerializeField] private float speed = 5f;
    private BrickColor playerPickColor;
    private Direct currentDirection = Direct.None;

    private Vector3 targetPosition;
    private bool isMoving = false;
    Stack<GameObject> stackOfBrick = new Stack<GameObject>();
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
        HasObstacleAhead(currentDirection);
        if (isMoving)
        {
            MovePlayer();
        }

    }
    private void HandleMovePlayer(Direct direction)
    {
        if (!isMoving && direction != Direct.None) // Chỉ xử lý vuốt nếu nhân vật không đang di chuyển
        {
            currentDirection = direction;
            targetPosition = GetNextPoint(currentDirection);
            isMoving = true;
        }
    }
    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
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
    public void OnInit()
    {
        isMoving = false;
        ClearBrick();
        skin.localPosition = Vector3.zero;
    }
    private bool HasObstacleAhead(Direct direction)
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
                directionVector = transform.position;
                break;
        }

        Vector3 rayStartPoint = transform.position + directionVector * 0.1f + Vector3.up;
        Vector3 rayDirection = Vector3.down;
        Debug.DrawRay(rayStartPoint, rayDirection, Color.red, 1f);  // Vẽ ray trong Scene để kiểm tra
        if (Physics.Raycast(rayStartPoint, rayDirection, out hit, 1f, layerDrop))
        {
            Debug.Log("Hit");
            Brick brick = hit.collider.gameObject.GetComponent<Brick>();
            if (brick.Color != playerPickColor)
            {
                isMoving = false;
                return true;
            }
        }

        return false;
    }
    public void AddBrick(GameObject currentBrickWhenCollide)
    {
        stackOfBrick.Push(currentBrickWhenCollide);
        currentBrickWhenCollide.transform.SetParent(stackBrickParent.transform);

        ChangePlayerPos(0.5f);

        Vector3 pos = skin.transform.localPosition;
        pos.y -= 0.5f;
        currentBrickWhenCollide.transform.localPosition = pos;
    }
    public void RemoveBrick(GameObject currentPosDrop)
    {

        if (stackBrickParent.transform.childCount > 0 && stackOfBrick.Count > 0)
        {
            Transform lastChild = stackBrickParent.transform.GetChild(stackBrickParent.transform.childCount - 1);
            stackOfBrick.Pop();
            lastChild.SetParent(null);

            ChangePlayerPos(-0.5f);


            lastChild.transform.position = currentPosDrop.transform.position;
        }
    }
    private void ChangePlayerPos(float adjustmentValue)
    {
        Vector3 characterSkin = skin.transform.localPosition;
        characterSkin.y += adjustmentValue;
        skin.transform.localPosition = characterSkin;
    }
    public void ClearBrick()
    {
        for(int i = 0; i < stackOfBrick.Count; i++)
        {
            var brick = stackOfBrick.Pop();
            Destroy(brick);
        }
        stackOfBrick.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        Brick brick = other.gameObject.GetComponent<Brick>();

        if (other.tag == "PickUpBrick")
        {

            if (stackOfBrick.Count == 0)
            {
                playerPickColor = brick.Color;
            }
            if (playerPickColor == brick.Color)
            {
                AddBrick(other.gameObject);
                BoxCollider boxCollider = other.gameObject.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    Destroy(boxCollider);
                }
            }
        }
        if (other.tag == "DropBrick")
        {

            if (playerPickColor == brick.Color)
            {
                RemoveBrick(other.gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
