using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Asteroid")){
            Destroy(gameObject);
        }
    }
}
