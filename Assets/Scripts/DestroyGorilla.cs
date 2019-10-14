using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGorilla : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Gorilla")
        {
            Destroy(col.gameObject);
            explosion.SetActive(true);
        }

        
    }
}
