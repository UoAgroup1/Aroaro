using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFall : MonoBehaviour
{

    public float fallSpeed = 10.0f;      //How fast should the object fall

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
    }
}