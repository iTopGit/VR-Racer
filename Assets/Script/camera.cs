using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 20.0f; 
    private float turnSpeed = 90.0f;
    private float horizontalInput;
    private float forwardInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        //forwardInput = Input.GetAxis("Vertical"); 

        //// Move the vehicle forward
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //// We turn the vehicle
        //transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
