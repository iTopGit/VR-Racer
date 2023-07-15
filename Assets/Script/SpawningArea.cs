using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningArea : MonoBehaviour
{
    private BoxCollider detectionArea;
    public LayerMask carLayer;

    public bool carDetected { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        detectionArea = gameObject.GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapBox(detectionArea.transform.position + detectionArea.center, detectionArea.size / 2, detectionArea.transform.rotation, carLayer);
       
        if (colliders.Length > 0)
        {
            carDetected = true;
        }
        else
        {
            carDetected = false;
        }
    }

}
