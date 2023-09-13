using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    float speed;
    private Animator humanAnimation;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        dead = false;
        humanAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
        if (speed > 0) 
        {
            humanAnimation.SetFloat("Speed_f", 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dead = true;
        }
    }
}
