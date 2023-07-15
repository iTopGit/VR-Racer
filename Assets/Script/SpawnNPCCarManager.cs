using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPCCarManager : MonoBehaviour
{


    [SerializeField] private bool[] isActivateSpawnsNPCCar = new bool[4];
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private GameObject[] pathPrefabs;
    [SerializeField] private SpawningArea[] spawningAreas;

    private int numPath = 3;
    private bool[] isSpawningPath = new bool[3];
    private float startDelay = 0.0f;
    private float repeatRate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPaths", startDelay, repeatRate);
        for(int i = 0; i < numPath; i++)
        {
            isSpawningPath[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPath(int pathIndex, Lane laneDefault)
    {
        int carIndex = Random.Range(0, carPrefabs.Length);
        Vector3 spawnPathPos = gameObject.transform.position;
        GameObject currentCar = carPrefabs[carIndex];
        currentCar.gameObject.transform.GetComponent<NPCCar>().lane = laneDefault;
        currentCar.gameObject.transform.GetComponent<NPCCar>().fromSpawnNumber = pathIndex;
        GameObject currentPath = Instantiate(pathPrefabs[pathIndex], spawnPathPos, gameObject.transform.rotation);

        currentPath.transform.SetParent(gameObject.transform);

        GameObject dollyCart = currentPath.transform.GetChild(1).gameObject;
        
        currentCar = Instantiate(currentCar, dollyCart.transform.position, dollyCart.transform.rotation);
        currentCar.transform.SetParent(dollyCart.transform);
        
    }
    public void SpawnPathIsOn(int pathIndex, bool isSingleSpawn = true)
    {
        if (isSingleSpawn)
        {
            SpawnPath(pathIndex, GetLane(pathIndex));

        } else
        {
            isSpawningPath[pathIndex] = true;
        }
    }
    public void SpawnPathIsOff(int pathIndex)
    {
        isSpawningPath[pathIndex] = false;
    }
    void SpawnPaths()
    {
        for(int i = 0;  i < numPath; i++)
        {
            if (isSpawningPath[i] && !spawningAreas[i].carDetected)
            {
                SpawnPath(i, GetLane(i));
            }
        }
    }

    private Lane GetLane(int pathIndex)
    {
        if (pathIndex == 2)
        {
            return Lane.Right;
        }
        return Lane.Left;
    }

    public bool GetIsActivateSpawnNPCCar(int numArea)
    {
        return isActivateSpawnsNPCCar[numArea];
    }
}
