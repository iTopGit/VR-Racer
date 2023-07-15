using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSensor : MonoBehaviour
{
	private NPC npcScript;
	
	[SerializeField] private float sensorForwardRange = 1.5f;
	[SerializeField] private float sensorSideRange = 3.0f;
	[SerializeField] private float sensorUpRange = 0.5f;
	
	[SerializeField] private float speedCheck = 3;
	
	public Transform walkSensorEarlier;
	public Transform walkSensorLater;
	public Vector3 walkSensorEarlierSize;
	public Vector3 walkSensorLaterSize;
	
	private float isRight;
	public bool IsRight
	{
		get { return isRight > 0; }
		set { isRight = value? 1 : -1; }
	}
	
	private void Start()
	{
		npcScript = GetComponent<NPC>();
	}
	
	// Cast a sensor to check a player car
	private void Update()
	{
		if (npcScript.isStartToCross == false) 
		{
			// For earlier sensor (stop to let npc walk cross crossing path)
			List<Collider> sensorEarlierColliders = new List<Collider>();
			sensorEarlierColliders.AddRange(Physics.OverlapBox(
				walkSensorEarlier.position,
				walkSensorEarlierSize / 2,
				walkSensorEarlier.rotation,
				LayerMask.GetMask("Car")
			));
			
			Collider playerEarlierCollider = sensorEarlierColliders.Find((collider) => collider.tag == "Player");
			
			// Check if player hit the overlap box, it will trigger the npc to walk.
			if (sensorEarlierColliders.Count > 0 && playerEarlierCollider != null)
			{
				CarController playerController = playerEarlierCollider.GetComponent<CarController>();
				
				if (playerController.currentSpeed < speedCheck) 
				{
					npcScript.isStartToCross = true;
				}
			}
		}
		
		// For later sensor (destroy npc when didn't stop for npc)
		List<Collider> sensorLaterColliders = new List<Collider>();
		sensorLaterColliders.AddRange(Physics.OverlapBox(
			walkSensorLater.position,
			walkSensorLaterSize / 2,
			walkSensorLater.rotation,
			LayerMask.GetMask("Car")
		));
		
		Collider playerLaterCollider = sensorLaterColliders.Find((collider) => collider.tag == "Player");
		
		// Check if player hit the overlap box, it will trigger the npc to destroy.
		if (sensorLaterColliders.Count > 0 && playerLaterCollider != null)
		{
			Destroy(gameObject);
		}
	}
	
	// Draw wire cube (overlap box) for debugging
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(walkSensorEarlier.position, walkSensorEarlierSize);
		Gizmos.DrawWireCube(walkSensorLater.position, walkSensorLaterSize);
	}
}
