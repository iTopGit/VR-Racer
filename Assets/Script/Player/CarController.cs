using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    public Stopwatch Stopwatch;
    public GameObject StartBarrier;
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking, isAccelerator, isReversing, isStopwatchStarted, triggerSlowReversing, triggerSlowAccelerating;
    private float targetSteerAngle = 0f; // the desired steering angle

    // Text Speed
    public float currentSpeed;
    public TextMeshProUGUI textSpeed;
    private readonly int decimalPlaces;
    Rigidbody rb;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle, maxSpeed, steerTime, currentSteerVelocity, retardationForce;
    [SerializeField] private bool isMaxSpeed;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    // Steering Wheel
    [SerializeField] private GameObject steeringWheel;

    //Sound
    public AudioClip carEngineRevSound;
    public AudioClip carBreakSoundShort;
    public AudioClip carBreakSoundLong;
    public AudioClip carStart;
    private AudioSource carAudio;
    private float downTime = 0f;
    private bool breakActivateSound = false;

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            if (!isStopwatchStarted)
            {
                Stopwatch.startStopwatch();
                isStopwatchStarted = true;
            }
            else
            {
                Stopwatch.endStopwatch();
                StartBarrier.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            StartBarrier.SetActive(true);
        }
    }

    public void acceleratorPressed()
    {
        isAccelerator = true;
        carAudio.PlayOneShot(carEngineRevSound, 1.0f);
    }
    public void acceleratorReleased()
    {
        isAccelerator = false;
        triggerSlowAccelerating = true;
    }
    public void breakPressed()
    {
        downTime = Time.time;
        isBreaking = true;
        breakActivateSound = true;
        if (currentSpeed < 15 && currentSpeed > 0)
        {
            carAudio.PlayOneShot(carBreakSoundShort, 1.0f);
        }
    }
    public void breakReleased()
    {
        isBreaking = false;
        downTime = Time.time - downTime;
        // Debug.Log("release=" + downTime);
    }
    public void reversePressed()
    {
        isReversing = true;
        carAudio.PlayOneShot(carEngineRevSound, 1.0f);
    }
    public void reverseReleased()
    {
        isReversing = false;
        triggerSlowReversing = true;
    }
    public void resetRotation()
    {
        bool isCarFlipping = (transform.rotation.eulerAngles.x > 45 && transform.rotation.eulerAngles.x < 315) || (transform.rotation.eulerAngles.z > 45 && transform.rotation.eulerAngles.z < 315);
        if(isCarFlipping)
        {
            Debug.Log("reseting..");
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Stopwatch.addPenalty();
        }
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Acceleration Input
        if (isAccelerator && !isReversing)
            verticalInput = 1;
        else if (isReversing && !isAccelerator)
            verticalInput = -1;
        else
            verticalInput = 0;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carAudio = GetComponent<AudioSource>();
        carAudio.PlayOneShot(carStart, 1.0f);
    }
    public void Update()
    {
        currentSpeed = (float)Math.Round((rb.velocity.magnitude)*3.6f, decimalPlaces);
        textSpeed.text = currentSpeed.ToString();
        float holdTime = Time.time - downTime;
        if(holdTime > 0.7f && isBreaking && breakActivateSound)
        {
            //Debug.Log("hold" + holdTime);
            if (currentSpeed > 0)
            {
                carAudio.PlayOneShot(carBreakSoundLong, 1.0f);
                breakActivateSound = false;
            }
        }
        
    }
    private void HandleMotor() {
        if (!isMaxSpeed)
        {
            if (!isBreaking && verticalInput == 0 )
            {
                frontLeftWheelCollider.motorTorque = 0;
                frontRightWheelCollider.motorTorque = 0;
                if (triggerSlowAccelerating)
                {
                    frontLeftWheelCollider.motorTorque = -retardationForce;
                    frontRightWheelCollider.motorTorque = -retardationForce;
                }
                else if (triggerSlowReversing)
                {
                    frontLeftWheelCollider.motorTorque = retardationForce;
                    frontRightWheelCollider.motorTorque = retardationForce;
                }
                if(currentSpeed == 0)
                {
                    triggerSlowAccelerating = false;
                    triggerSlowReversing = false;
                }
            } 
            else
            {
                frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
                frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            }
        }
        else
        {
            if(currentSpeed < maxSpeed)
            {
                frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
                frontRightWheelCollider.motorTorque = verticalInput * motorForce;
            }
            else
            {
                frontLeftWheelCollider.motorTorque = 0;
                frontRightWheelCollider.motorTorque = 0;
            }
        }

        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        targetSteerAngle = maxSteerAngle * horizontalInput;
        if(targetSteerAngle == 0)
        {
            currentSteerAngle = Mathf.SmoothDamp(currentSteerAngle, targetSteerAngle, ref currentSteerVelocity, steerTime/2);
        }
        else
        {
            currentSteerAngle = Mathf.SmoothDamp(currentSteerAngle, targetSteerAngle, ref currentSteerVelocity, steerTime);
        }
        
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
        steeringWheel.transform.rotation = Quaternion.Euler(steeringWheel.transform.eulerAngles.x, steeringWheel.transform.eulerAngles.y, currentSteerAngle*5);
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}