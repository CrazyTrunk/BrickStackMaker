using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeStackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUpBrick")
        {
            Player.Instance.RemoveBrick(other.gameObject);

        }
    }
}
