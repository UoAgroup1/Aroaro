using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWin : MonoBehaviour
{

    //public GameObject rampTop; //Cube for the top of the ramp
    //public GameObject rampBottom; // Cube for bottom of ramp
    public bool condition1 = false;
    public bool condition2 = false;



    void Update()
    {
        //The below allows movement of the sphere
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
        */

        //The below checks position of sphere with rampTop and rampBottom
        /*
        if((transform.position.x > rampTop.transform.position.x && transform.position.x < rampTop.transform.position.x - rampTop.transform.localScale.x) &&
            (transform.position.y > rampTop.transform.position.y && transform.position.y < rampTop.transform.position.y + rampTop.transform.localScale.y) &&
            (transform.position.z > rampTop.transform.position.z && transform.position.z < rampTop.transform.position.z + rampTop.transform.localScale.z)
            )
        {
            //GetComponent<Renderer>().material.color = Color.green;
            Debug.Log(transform.position.x);
            Debug.Log(transform.position.y);
            Debug.Log(transform.position.z);
        }
        */

        if ((transform.position.x > 0f && transform.position.x < 4f) &&
            (transform.position.y > 6f && transform.position.y < 12f) &&
            (transform.position.z > -26f && transform.position.z < -19f)
            )
        {
            condition1 = true;
        }

        if ((transform.position.x > 16f && transform.position.x < 20f) &&
            (transform.position.y > -2f && transform.position.y < 3f) &&
            (transform.position.z > -18f && transform.position.z < -13f)
            )
        {
            condition2 = true;
        }






        if (condition1 == true && condition2 == true)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
