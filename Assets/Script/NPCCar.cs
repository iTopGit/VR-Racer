using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveStates
{
    Stop,
    // Speeding,
    Moving,
    // Braking
}

public enum Lane
{
    Left,
    Right,
}

public class NPCCar : MonoBehaviour
{

	public float currentSpeed { get; private set; } // Current speed of the car
	public float currentMaxSpeed { get; private set; }
	public float maxSpeed = 8;

	private bool isStop = false;
	private float sensorLength = 3f;
	private float sensorAngle = 20;
	private bool isCarDetected = false;

	private GameObject dollyCart;


	private float accelerationRate = 5f; // Rate at which the car accelerates
	private float brakeRate = 100f; // Rate at which the car brakes

	public Lane lane;
	public int fromSpawnNumber;
	public LayerMask layerMask;

	public MoveStates moveState;
	

	// Start is called before the first frame update
	void Start()
	{

		dollyCart = gameObject.transform.parent.gameObject;
		
	}

	// Update is called once per frame
	void Update()
	{

		if ( currentSpeed == 0 && moveState == MoveStates.Moving)
		{
			moveState = MoveStates.Stop;
		}
		else if (currentSpeed != 0 && moveState == MoveStates.Stop)
		{
			moveState = MoveStates.Moving;
		}


		if (currentMaxSpeed == 0 && currentSpeed != 0)
		{
			Stop();
		}

		// If car is moving
		else if (currentMaxSpeed != 0 && currentSpeed != currentMaxSpeed)
		{
			Moving();
		}

	
	}

	void FixedUpdate()
	{
        if (!isStop)
        {
			CarSensor();
		}
		
	}


	public void setDefaultSpeed()
	{
		currentMaxSpeed = maxSpeed;
		currentSpeed = currentMaxSpeed;
	}
	public void ReleaseCar()
	{
		
		currentMaxSpeed = maxSpeed;
		isStop = false;

	}

	public void StopCar()
	{
		currentMaxSpeed = 0;
		isStop = true;
	}


	// Set car to move for moveState and animation
	private void Moving()
	{
		// For speeding up and braking
		if (currentSpeed < currentMaxSpeed)
		{
			SpeedUp();
		}
		else if (currentSpeed > currentMaxSpeed)
		{
			Brake();
		}
	}

	// Set car to stop for moveState and animation
	private void Stop()
	{
		if (currentSpeed != 0)
		{
			Brake();
		}
	}

	private void SpeedUp()
	{
		currentSpeed = Mathf.Clamp(currentSpeed + accelerationRate * Time.deltaTime, 0, currentMaxSpeed);
		dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentSpeed;
	}

	private void Brake()
	{
		currentSpeed = Mathf.Clamp(currentSpeed - brakeRate * Time.deltaTime, 0, maxSpeed);
		dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = currentSpeed;
	}



	private void CarSensor()
	{
		RaycastHit hit = new RaycastHit();

		isCarDetected = false;

		Vector3 sensorStartPos = transform.position + transform.forward * 2f + new Vector3(0, 0.6f, 0);

		Vector3 sensorDirection = new Vector3();
		
		// Cast a ray in front of the car
		RaySensor(hit, sensorStartPos, sensorDirection, 0, true, -0.70f);
		RaySensor(hit, sensorStartPos, sensorDirection, 0, true, 0);
		RaySensor(hit, sensorStartPos, sensorDirection, 0, true, 0.70f);
		// Cast a ray to the right of the car
		RaySensor(hit, sensorStartPos, sensorDirection, sensorAngle);
		RaySensor(hit, sensorStartPos, sensorDirection, 2 * sensorAngle);
		// Cast a ray to the left of the car
		RaySensor(hit, sensorStartPos, sensorDirection,-sensorAngle);
		RaySensor(hit, sensorStartPos, sensorDirection, -2 * sensorAngle);
		
	}

	private void RaySensor(RaycastHit hit, Vector3 sensorStartPos, Vector3 sensorDirection, float sensorAngle, bool isCenterRay = false, float frontSideSensorPosition = 0f)
	{
		sensorStartPos.x += frontSideSensorPosition;
		sensorDirection = Quaternion.AngleAxis(sensorAngle, transform.up) * transform.forward;
		if (Physics.Raycast(sensorStartPos, sensorDirection, out hit, sensorLength, layerMask) && (hit.collider.gameObject.tag == "Car" || hit.collider.gameObject.tag == "Player"))
		{
			//Debug.Log(hit.collider.gameObject.tag+" "+hit.collider.gameObject.name);
			if (hit.collider.gameObject.tag == "Player" && (isCenterRay || (lane == Lane.Left && hit.collider.gameObject.GetComponent<CarController>().currentSpeed > 3)))
            {

				currentMaxSpeed = 0;
				isCarDetected = true;
				Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.yellow);
				
			}
			else if (hit.collider.gameObject.tag == "Car")
            {
				if (hit.collider.gameObject.GetComponent<NPCCar>().lane == lane)
				{
					if (hit.collider.gameObject.GetComponent<NPCCar>().fromSpawnNumber == fromSpawnNumber)
                    {
						isCarDetected = true;
						AdjustTheSpeed(hit, sensorStartPos);
					}
					else if (isCenterRay)
                    {
						isCarDetected = true;
						AdjustTheSpeed(hit, sensorStartPos);
					}
					
				}
				else if (isCenterRay)
				{
					isCarDetected = true;
					currentMaxSpeed = 0;
				}
				//Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.red);
			}


		}
		else
		{
			if (!isCarDetected)
			{
				currentMaxSpeed = maxSpeed;
			}
			Debug.DrawLine(sensorStartPos, sensorStartPos + sensorDirection * sensorLength, Color.green);
		}
	}

	private void AdjustTheSpeed(RaycastHit hit, Vector3 sensorStartPos)
	{
		float o_speed = hit.collider.gameObject.transform.GetComponent<NPCCar>().currentSpeed;
		
		if (o_speed < currentMaxSpeed)
		{
			currentMaxSpeed = o_speed;
			//Debug.Log(gameObject.name + " "+ hit.collider.gameObject.name+" Detect to Braking");
		}
		Debug.DrawLine(sensorStartPos, hit.point, Color.red);
		//Debug.Log("Detected car: " + hit.collider.gameObject.tag);
	}


}
