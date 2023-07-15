using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPathCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}