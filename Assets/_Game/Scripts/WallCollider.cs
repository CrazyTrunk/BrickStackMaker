using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
