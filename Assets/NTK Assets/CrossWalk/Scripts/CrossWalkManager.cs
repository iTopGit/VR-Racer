using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossWalkManager : MonoBehaviour
{
    public GameObject[] humanPrefabs;
    [SerializeField] public GameObject spawnPoint;

    GameObject newHuman;
    HumanController humanController;
    bool move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnHuman()
    {
        int humanIndex = Random.Range(0, humanPrefabs.Length);
        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion spawnRotation = spawnPoint.transform.rotation;
        newHuman = Instantiate(humanPrefabs[1], spawnPosition, spawnRotation);
        humanController = newHuman.GetComponent<HumanController>();
    }

    private void OnTriggerStay(Collider other)
    {
        float carSpeed = other.GetComponent<CarController>().currentSpeed;

        // ========== Spawn NPC อยู่นี่นะน้องบ่าว ========== //
        if (carSpeed < 2 && !move && (humanController != null)) 
        {
            move = true;
            humanController.setSpeed(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnHuman();
    }
}
