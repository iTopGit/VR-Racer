using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    [SerializeField] private GameObject[] npcPrefabs;
    [SerializeField] private float timeToNextSpawn = 0.3f;
    [SerializeField] private int npcNumToSpawn;

    [SerializeField] private Transform startPoint;
    [SerializeField] private List<Transform> movePointList;

    [SerializeField] private Transform parent;

    private bool hasSpawned;

    [SerializeField] private Transform spawnTrigger;
    [SerializeField] private float spawnTriggerSize = 2;

    [SerializeField] private Transform walkSensorEarlier;
    [SerializeField] private Transform walkSensorLater;
    [SerializeField] private Vector3 walkSensorEarlierSize;
    [SerializeField] private Vector3 walkSensorLaterSize;
    [SerializeField] private bool isWalkSensorOnRight;

    private void Update()
    {
        List<Collider> colliderList = new List<Collider>();
        colliderList.AddRange(Physics.OverlapBox(
            spawnTrigger.position, Vector3.one * spawnTriggerSize / 2,
            spawnTrigger.rotation,
            LayerMask.GetMask("Car")
        ));

        // Check if player hit the overlap box, it will start to spawn the npc.
        if (colliderList.Count > 0 && colliderList.Find((collider) => collider.tag == "Player") && !hasSpawned)
        {
            hasSpawned = true;
            StartSpawn();
        }
        else if (parent.childCount == 0)
        {
            hasSpawned = false;
        }
    }

    // Draw wire cube (overlap box) for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnTrigger.position, Vector3.one * spawnTriggerSize);
    }

    // Spawn NPC and set all way points for NPC
    private void StartSpawn()
    {
        int randomIndex = Random.Range(0, npcPrefabs.Length);
        GameObject npcObject = Instantiate(npcPrefabs[randomIndex], startPoint.position, startPoint.rotation, parent);
        NPC npc = npcObject.GetComponent<NPC>();
        WalkSensor npcSensor = npcObject.GetComponent<WalkSensor>();

        foreach (Transform movePoint in movePointList)
        {
            npc.movePointList.Add(movePoint);
        }

        npcSensor.walkSensorEarlier = walkSensorEarlier;
        npcSensor.walkSensorLater = walkSensorLater;
        npcSensor.walkSensorEarlierSize = walkSensorEarlierSize;
        npcSensor.walkSensorLaterSize = walkSensorLaterSize;
        npcSensor.IsRight = isWalkSensorOnRight;

        if (parent.childCount < npcNumToSpawn)
        {
            StartCoroutine(WaitToNextSpawn());
        }
    }

    // Wait to spawn next NPC
    private IEnumerator WaitToNextSpawn()
    {
        yield return new WaitForSeconds(timeToNextSpawn);
        StartSpawn();
    }
}
