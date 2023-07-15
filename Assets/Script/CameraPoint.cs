using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
	[SerializeField] private Transform nspCamera;
	
	[SerializeField] private Transform cameraPoint;
	
	[SerializeField] private Transform triggerPoint;
	[SerializeField] private float triggerSize;
	
	private bool hasMoved;
	
	private void Update()
	{
		List<Collider> colliderList = new List<Collider>();
		colliderList.AddRange(Physics.OverlapBox(
			triggerPoint.position,Vector3.one * triggerSize/2,
			triggerPoint.rotation,
			LayerMask.GetMask("Car")
		));
		
		hasMoved = nspCamera.position == cameraPoint.position;
			
		// Check if player hit the overlap box, it will start to spawn the npc.
		if (colliderList.Count > 0 && colliderList.Find((collider) => collider.tag == "Player") && !hasMoved)
		{
			nspCamera.position = cameraPoint.position;
			nspCamera.rotation = cameraPoint.rotation;
		}
	}
	
	// Draw wire cube (overlap box) for debugging
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(triggerPoint.position, Vector3.one * triggerSize);
	}
}
