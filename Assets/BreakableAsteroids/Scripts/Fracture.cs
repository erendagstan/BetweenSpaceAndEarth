using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fracture : MonoBehaviour

{
   
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            FractureObject();
        }
    }


    public void FractureObject()
    {
        //transform.position
        Instantiate(fractured,transform.position, transform.rotation); //Spawn in the broken version
        Destroy(gameObject); //Destroy the object to stop it getting in the way
        StartCoroutine(DestroyDestroyedObject());
       
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            FractureObject();
        }
    }*/

    IEnumerator DestroyDestroyedObject()
    {
        yield return new WaitForSeconds(2f);
        //DestroyImmediate(fractured.gameObject,true);
        Destroy (fractured.gameObject);
    }
}
