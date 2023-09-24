using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    float speed = 0;
    Animator humanAnimation;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        humanAnimation = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void setSpeed(float newSpeed)
    {
        Debug.Log("Function Called");
        speed = newSpeed;
        if (speed > 0) 
        {
            humanAnimation.SetFloat("Speed_f", 0.5f);
        }
    }

}
