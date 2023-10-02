using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnNpcCar : MonoBehaviour
{
    GameObject controller;

    public int checkpointNumber;
    private SpawnNPCCarManager spawnManager;
    public bool isCrashed = false;

    public Transform carPos;
    public GameObject NPCcarPrefab;  // Prefab of the NPCcar GameObject

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("distanceTrack");
        spawnManager = gameObject.GetComponentInParent<SpawnNPCCarManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use CompareTag for better performance
        {
            // Debug.Log("Player entered the checkpoint");
            SpawnNPCcar();  // Call the method to spawn the NPCcar
        }
    }

    void SpawnNPCcar()
    {
        //Spawn NPC car
        Debug.Log("Start NPC Car Spawn");
        GameObject newNPCcar = Instantiate(NPCcarPrefab, carPos.position, carPos.rotation);
        // get dollyCart as class
        Cinemachine.CinemachineDollyCart dollyCart = newNPCcar.GetComponentInChildren<Cinemachine.CinemachineDollyCart>();
        if (dollyCart != null)
        {
            StartCoroutine(DestroyNPCcarAtEndOfPath(dollyCart, newNPCcar));
        }
        /*
        else
        {
            Debug.Log("car is null");
            Debug.LogWarning("CinemachineDollyCart component not found on the NPCcar.");
        }
         */
    }

    IEnumerator DestroyNPCcarAtEndOfPath(Cinemachine.CinemachineDollyCart dollyCart, GameObject npcCar)
    {
        float pathLength = dollyCart.m_Path.PathLength; // Get the length of the path

        while (dollyCart.m_Position < pathLength)
        {
            if (isCrashed) {
                dollyCart.m_Speed = 0f;
                // Debug.Log(npcCar.name);
                isCrashed = false;
                controller.GetComponent<DistanceTrackController>().crashCounting();
                
                StartCoroutine(AfterCrashed(npcCar));
            }
            yield return new WaitForSeconds(0);
        }

        deleteEvent(npcCar);
    }

    IEnumerator AfterCrashed(GameObject npcCar)
    {
        yield return new WaitForSeconds(2.0f);
        deleteEvent(npcCar);
    }

    void deleteEvent(GameObject npcCar) {
        Destroy(npcCar.gameObject);
        Destroy(gameObject);
    }
}