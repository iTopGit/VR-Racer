using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControll : MonoBehaviour
{
    //Used Forces to move and stop the car
    public float MotorForce, SteerForce, BreakForce;

    //FL = Front_Left Wheel | FR = Front_Right Wheel | BL = Back Left Wheel | BR = Back Right Wheel
    public WheelCollider FL_Wheel, FR_Wheel, BL_Wheel, BR_Wheel;

    void Start()
    {
        
    }

    
    void Update()
    {

        //CAR CONTROLLER MOVEMENT
        float v = Input.GetAxis("Vertical") * MotorForce;
        float h = Input.GetAxis("Horizontal") * SteerForce;


        //Wheels Collider makes the car move (Unity inside feature)
        BR_Wheel.motorTorque = v;
        BL_Wheel.motorTorque = v;

        FL_Wheel.steerAngle = h;
        FR_Wheel.steerAngle = h;

        //If we Click space - make the car stop
        if (Input.GetKey(KeyCode.Space))
        {
            BL_Wheel.brakeTorque = BreakForce;
            BR_Wheel.brakeTorque = BreakForce;
        }

        //If we keep long click Space Button - values of speed will equal to stop the car (0)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            BL_Wheel.brakeTorque = 0;
            BR_Wheel.brakeTorque = 0;
        }

        
        if(Input.GetAxis("Vertical") == 0)
        {
            BL_Wheel.brakeTorque = BreakForce;
            BR_Wheel.brakeTorque = BreakForce;
        }
        else
        {
            BL_Wheel.brakeTorque = 0;
            BR_Wheel.brakeTorque = 0;
        }


    }
}
