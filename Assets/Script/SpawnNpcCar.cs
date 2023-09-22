using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpcCar : MonoBehaviour
{
    public int checkpointNumber;
    private SpawnNPCCarManager spawnManager;
    public bool isCrashed = false;
    //private RestArea restArea;

    public Transform carPos;
    public GameObject NPCcarPrefab;  // Prefab of the NPCcar GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Hello");
        spawnManager = gameObject.GetComponentInParent<SpawnNPCCarManager>();
        //restArea = spawnManager.GetComponentInChildren<RestArea>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Use CompareTag for better performance
        {
            Debug.Log("Player entered the checkpoint");
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
            // test function where car could only destroy by Colliding only.
            // KeepExist(dollyCart, newNPCcar);
        }
        else
        {
            Debug.Log("car is null");
            Debug.LogWarning("CinemachineDollyCart component not found on the NPCcar.");
        }
    }

    IEnumerator DestroyNPCcarAtEndOfPath(Cinemachine.CinemachineDollyCart dollyCart, GameObject npcCar)
    {
        float pathLength = dollyCart.m_Path.PathLength; // Get the length of the path

        while (dollyCart.m_Position < pathLength)
        {   
            if(isCrashed)
            {
                isCrashed = false;
                dollyCart.m_Speed = 0f;
                StartCoroutine(AfterCrashed(npcCar));
            }
            // Debug.Log("Cart's position: " + dollyCart.m_Position + ", pathLength is: " + pathLength);
            yield return null;
        }

        // Debug.Log("this script has disabled the loop( dollyCart's position is higher than pathLength)");
        Destroy(npcCar);  // Destroy the NPCcar when it reaches the end of the path
        Destroy(gameObject);
    }

    IEnumerator AfterCrashed(GameObject npcCar)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(npcCar);
        Destroy(gameObject);
    }

}