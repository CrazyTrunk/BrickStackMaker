using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeMinDistance = 100f;
    public delegate void SwipeAction(Direct direction);
    public static event SwipeAction OnSwipe;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    touchEndPos = touch.position;
                    float swipeX = touchEndPos.x - touchStartPos.x;
                    float swipeY = touchEndPos.y - touchStartPos.y;
                    if(Vector3.Distance(touchStartPos, touchEndPos) < swipeMinDistance)

                    {
                        return;
                    }
                    else
                    {
                        if (Mathf.Abs(swipeX) > Mathf.Abs(swipeY))
                        {
                            if (swipeX > swipeMinDistance)
                            {
                                OnSwipe?.Invoke(Direct.Right);
                            }
                            else if (swipeX < -swipeMinDistance)
                            {
                                OnSwipe?.Invoke(Direct.Left);

                            }
                        }
                        else
                        {
                            if (swipeY > swipeMinDistance)
                            {
                                OnSwipe?.Invoke(Direct.Forward);
                            }
                            else if (swipeY < -swipeMinDistance)
                            {
                                OnSwipe?.Invoke(Direct.Back);
                            }
                        }
                    }
                   
                    break;
            }
        }
    }
}
