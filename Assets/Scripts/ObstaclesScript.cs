using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ObstaclesScript : MonoBehaviour
{
    public GameObject obstacle;
    //public TextMeshProUGUI healthText;
    //int health = 100;
   
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<GameObject>();
        //healthText = GetComponentInChildren<TextMeshProUGUI>();
        //healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if (health <= 0)
        //{
        //    health = 0;
        //    Destroy(obstacle);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Uf");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("You died!");
            

        }
    }
}
