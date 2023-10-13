using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private float speed = 5f;
    private Direct currentDirection = Direct.None;
    [SerializeField] private LayerMask layerBrick;
    private bool isMoving = false;
    private Vector3 targetPosition;
    [SerializeField] public GameObject stackBrickParent;
    [SerializeField] public GameObject prevBrick;
    [SerializeField] public GameObject skin;
    Stack<GameObject> stackOfBrick = new Stack<GameObject>();
    private BrickColor playerPickColor;
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

            if(playerPickColor == brick.Color)
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
