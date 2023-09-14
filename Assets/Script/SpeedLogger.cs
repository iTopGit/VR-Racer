using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SpeedLogger : MonoBehaviour
{

    //public TextMeshProUGUI textSpeed;
    private float currentSpeed;
    private readonly int decimalPlaces;
    private Collision collision;

    // OnCollisionEnter is called when the player enters the trigger box.
    private void OnCollisionEnter(Collision collision)
    {
        this.collision = collision;
        if (collision.gameObject.tag == "Player")
        {
            currentSpeed = (float)Math.Round((collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude) * 3.6f, 2);
            Debug.Log("Current Speed: " + currentSpeed);
        }
    }

    private void Update()
    {
        if (collision != null)
        {
            Debug.Log("Its collision");
        }
    }

}
