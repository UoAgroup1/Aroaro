using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevator : MonoBehaviour
{
    public GameObject elevatorButton;
    public float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        /*while (transform.position.y > -2f)
        {
            transform.Translate(0, moveSpeed, 0);
        }

        while (transform.position.y < 8f)
        {
            transform.Translate(0, -moveSpeed, 0);
        }
        

        while (transform.position.y < 8f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }



        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        */

        while (transform.position.y < 8f)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }

    private void OnMouseOver()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
