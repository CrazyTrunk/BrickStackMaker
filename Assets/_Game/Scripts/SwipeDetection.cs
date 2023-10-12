using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float swipeMinDistance = 100f; // Khoảng cách tối thiểu để xem là một vuốt
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
                        Debug.Log("Vuốt chưa đủ");
                        return;
                    }
                    else
                    {
                        if (Mathf.Abs(swipeX) > Mathf.Abs(swipeY))
                        {
                            // Vuốt ngang (trái hoặc phải)
                            if (swipeX > swipeMinDistance)
                            {
                                // Vuốt sang phải
                                Debug.Log("Vuốt sang phải");
                                OnSwipe?.Invoke(Direct.Right);
                            }
                            else if (swipeX < -swipeMinDistance)
                            {
                                // Vuốt sang trái
                                Debug.Log("Vuốt sang trái");
                                OnSwipe?.Invoke(Direct.Left);

                            }
                        }
                        else
                        {
                            // Vuốt dọc (lên hoặc xuống)
                            if (swipeY > swipeMinDistance)
                            {
                                // Vuốt lên
                                Debug.Log("Vuốt lên");
                                OnSwipe?.Invoke(Direct.Forward);
                            }
                            else if (swipeY < -swipeMinDistance)
                            {
                                // Vuốt xuống
                                Debug.Log("Vuốt xuống");
                                OnSwipe?.Invoke(Direct.Back);
                            }
                        }
                    }
                   
                    break;
            }
        }
    }
}
