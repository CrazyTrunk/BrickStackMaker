using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickUpBrick")
        {
            Player.Instance.AddBrick(other.gameObject);
            other.gameObject.AddComponent<StackScript>();    
        }
    }
}
