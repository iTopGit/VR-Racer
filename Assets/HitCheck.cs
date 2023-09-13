using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    SpawnNpcCar checker;
    // Start is called before the first frame update
    private void Start()
    {
        checker = GameObject.FindGameObjectWithTag("TriggerBox01").GetComponent<SpawnNpcCar>();
    }
            
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checker.isCrashed = true;
            Debug.Log("You Hit the car mother fucker");
        }
    }
}
