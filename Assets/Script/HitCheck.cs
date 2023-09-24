using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    SpawnNpcCar checker;
    public string triggerName;
    public Transform Barrier;
    public bool isCrashed = false;
    // Start is called before the first frame update
    private void Start()
    {
        checkParent();
        if(Barrier != null) { Physics.IgnoreCollision(Barrier.GetComponent<Collider>(), GetComponent<Collider>()); }
    }
            
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkParent();
            checker.isCrashed = true;
            isCrashed = true;
        }
    }

    void checkParent()
    {
        if (triggerName != null) {
            checker = GameObject.FindGameObjectWithTag(triggerName).GetComponent<SpawnNpcCar>();
        } else {
            checker = GameObject.FindGameObjectWithTag("TriggerBox01").GetComponent<SpawnNpcCar>();
        }
    }
}
