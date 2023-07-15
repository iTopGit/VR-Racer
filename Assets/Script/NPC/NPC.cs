using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
	private NavMeshAgent agent;
	
	private Animator animator;
	
	[SerializeField] private float speed = 0;
	
	public List<Transform> movePointList; 
	
	public bool isStartToCross;
	
	private bool isCrossing;
	
	[SerializeField] private float lookAroundTime = 3.5f;
	
	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		
		agent.speed = speed;
	}

	// Update is called once per frame
	void Update()
	{	
		// Set speed for NPC to animate walking and running animation.
		animator.SetFloat("Speed_f", agent.velocity.magnitude);
	}

	// Change from Moving (Walking, Running) -> Idle
	private void Idle() 
	{
		// agent.SetDestination(transform.position);
		agent.isStopped = true;
	}
	
	// Change from Idle -> Moving (Walking, Running)
	private void Move(Transform destination) 
	{
		agent.SetDestination(destination.position);
		agent.isStopped = false;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "NpcMovePoint") 
		{	
			if (other.gameObject.name == "CrosswalkHead" && !isCrossing)
			{
				Idle();
				animator.SetInteger("LookAround", 1);
				StartCoroutine(WaitForLookAround());
			}
			
			else if (other.gameObject.name == "Destination")
			{
				Destroy(gameObject);
			}
			
			else if (other.gameObject.name != "CrosswalkHead")
			{
				Transform nextMovePoint = movePointList[0];
				
				Move(nextMovePoint);
				movePointList.Remove(nextMovePoint);
				
				isCrossing = false;
			}
		}
	}
	
	private void OnTriggerStay(Collider other)
	{
		// If npc stay in crosswalk head and start to cross.
		if (other.gameObject.name == "CrosswalkHead" && isStartToCross && !isCrossing && animator.GetInteger("LookAround") == 0)
		{
			isCrossing = true;
			Transform nextMovePoint = movePointList[0];
			
			Move(nextMovePoint);
			movePointList.Remove(nextMovePoint);
			
			isStartToCross = false;
		}
	}
	
	private IEnumerator WaitForLookAround() 
	{
		yield return new WaitForSeconds(lookAroundTime);
		animator.SetInteger("LookAround", 0);
	}
	
}
