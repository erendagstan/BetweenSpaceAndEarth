using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //public Transform camera;
    //public float sSpeed = 10f;
    //public Vector3 dist;
    //public Transform lookTarget;

    private Vector3 offset;
    public GameObject player;

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset; 
    }
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
