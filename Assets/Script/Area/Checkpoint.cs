using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
    private SpawnNPCCarManager spawnManager;
    private RestArea restArea;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = gameObject.GetComponentInParent<SpawnNPCCarManager>();
        restArea = spawnManager.GetComponentInChildren<RestArea>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" )
        {
            PlayerDetected();
        }
    }

    public void PlayerDetected()
    {
        switch (checkpointNumber)
        {
            case 1:
                if (spawnManager.GetIsActivateSpawnNPCCar(0))
                {
                    spawnManager.SpawnPathIsOn(0, false);
                }
                break;
            case 2:
                if (spawnManager.GetIsActivateSpawnNPCCar(1))
                {
                    spawnManager.SpawnPathIsOn(0);
                }
                
                break;
            case 3:
                if (spawnManager.GetIsActivateSpawnNPCCar(2))
                {
                    spawnManager.SpawnPathIsOn(1);
                }
                break;
            case 4:
                if (spawnManager.GetIsActivateSpawnNPCCar(3))
                {
                    spawnManager.SpawnPathIsOn(2);
                }
                break;
            case 5:
                if (spawnManager.GetIsActivateSpawnNPCCar(0))
                {
                    spawnManager.SpawnPathIsOff(0);
                }
                break;
            case 6:
                if (spawnManager.GetIsActivateSpawnNPCCar(2))
                {
                    restArea.ReleaseCar();
                }
                break;
            default:
                break;
        }
        
    }
}
