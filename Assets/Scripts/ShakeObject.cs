using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShakeObject : MonoBehaviour
{
    public float Timer = 0f;

    [SerializeField]
    public float speed = 10f; //how fast it shakes

    [SerializeField]
    public float amount = 0.18f; //how much it shakes
    // Update is called once per frame
    void Update()
    {
        /*
        //transform.position.x = Mathf.Sin(Time.time * speed) * amount;
        transform.position = transform.position + new Vector3(Mathf.Sin(Time.time * speed) * amount, 0, 0);
        Timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.
        */

        if(Timer <= 1)

        {
            transform.position = transform.position + new Vector3(Mathf.Sin(Time.time * speed) * amount, 0, 0);
            
        }
        Timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second.


    }
}
