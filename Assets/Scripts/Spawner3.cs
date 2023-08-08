using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner3 : MonoBehaviour
{

    public Transform spawner;
    public Transform portal;
    public GameObject player;

    public GameObject asteroid;
    public Animator asteroidAnim;

    public GameObject spawnerObject;
    float distance;
    Vector3 playerPosition;
    bool isDistance = false;
    private IEnumerator spawnerAsteroid;
    public Animator playerAnim;
    public Vector3 portalPos;


    float force=15f;
    public float torque;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Transform>();
        portal = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        distance = Vector3.Distance(spawner.position, playerPosition);

        //Debug.Log(distance);

        if (spawner.transform.position.z >= player.transform.position.z && isDistance == false)
        {
            if (playerAnim.GetBool("isDefeat") == false)
            {
                spawnerAsteroid = SpawnAsteroid();
                StartCoroutine(spawnerAsteroid);
            }

        }
        else
        {
            isDistance = true;

        }

    }

    IEnumerator SpawnAsteroid()
    {
        portalPos = new Vector3(portal.position.x, portal.position.y, portal.position.z - 5f);
        isDistance = true;
        var asteroidBomb = Instantiate(asteroid, portalPos, Quaternion.identity);
        
        if (portalPos.z > 140f && portalPos.z <220f)
        {
            force = 14f;
        }

        else if(portalPos.z > 220f  && portalPos.z <250f)
        {
            force = 10f;
        }
        else if (portalPos.z > 250f && portalPos.z < 260f)
        {
            force = 13f;
        }
        else if (portalPos.z > 260f && portalPos.z < 300f)
        {
            force = 11f;
        }

        //Debug.Log(portalPos);
        asteroidBomb.GetComponent<Rigidbody>().AddForce(new Vector3(asteroidBomb.transform.position.x, asteroidBomb.transform.position.y, asteroidBomb.transform.position.z * -force), ForceMode.Force);

        // asteroidBomb.transform.position.z *-force
        asteroidBomb.GetComponent<Rigidbody>().AddTorque(new Vector3(torque,0f, 0f));
        // For Destroying assets is not permitted to avoid data loss.
        yield return new WaitForSeconds(2f);
        //Destroy(asteroidBomb,1f);
        isDistance = false;
        Destroy(asteroidBomb, 0f);
        //yield return new WaitForSeconds(1f);
    }


}
