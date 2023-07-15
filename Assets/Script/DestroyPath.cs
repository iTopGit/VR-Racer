using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPath : MonoBehaviour
{
    public float destination;
    private GameObject dollyCart;
    private NPCCar car;
    private SpawnNPCCarManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        dollyCart = gameObject.transform.GetChild(1).gameObject;
        spawnManager = gameObject.GetComponentInParent<SpawnNPCCarManager>();
        car = gameObject.transform.GetComponentInChildren<NPCCar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position > destination)
        {
            Destroy(gameObject);
        }
    }
}
