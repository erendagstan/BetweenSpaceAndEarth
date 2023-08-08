using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float runningSpeed;
    public Transform playerTransform;
    private float xSpeed = 300f;

    public Animator playerAnim;
    public GameObject arrowObject;

    public Transform arrowPoint;
    
    private static float limitX=5.88f;

    public CapsuleCollider playerCollider;

    public GameObject restartPanel;

    public Animator cameraAnim;
    private bool isFiring = false;
    
    private Rigidbody playerRb;

    public GameManager gameManager;

    public TextMeshProUGUI coinText;
    public Image silverCoinImage;
    private Color tempSilverColor;
    int coin;
    public bool silverCoin = false;


    float touchXdelta;
    float newX;
    bool endGame = false;
    private bool died = false;

    private Vector2 startTouchPosition, endTouchPosition, currentPosition;

    [Header("--AudioSource--")]
    public AudioSource audioSource;

    [Header("--AudioClips--")]
    public AudioClip deathMusic;
    public AudioClip hyperMusic;
    public AudioClip bowAudio;
    public AudioClip jumpAudio;
    public AudioClip coinAudio;
    public AudioClip silverCoinAudio;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
        
        
        playerAnim = GetComponentInChildren<Animator>();
        arrowPoint = GetComponent<Transform>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerRb = GetComponent<Rigidbody>();



        //gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame == false)
        {
            SwipeCheck();

        }



    }

    private void SwipeCheck()
    {
        //denenmesi lazým
        /*
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
        }
        
        if(endTouchPosition.y >  startTouchPosition.y+30f && playerAnim.GetBool("isJumping") == false )
        {
            setJumping();  // doðru çalýþmýyor!
        }
        */


        //  TOUCH PASE BEFORE
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXdelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
            playerAnim.SetBool("isFire", false);
         
            if (endGame == false)
            {
                SetRunningSpeed();
            }
            StopFiring();
        }
        /*
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition=Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;
        }*/
        
        else if (Input.GetMouseButton(0))
        {
            touchXdelta = Input.GetAxis("Mouse X");
            playerAnim.SetBool("isFire", false);

            if (endGame ==false) {
                SetRunningSpeed();
            }
            StopFiring();
        }
        else
        {
            touchXdelta = 0;
        }
        newX = transform.position.x + xSpeed * touchXdelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);


        Vector3 newPos = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPos;

    }


    public void setFiring()
    {
        if(isFiring == true)
        {
            isFiring = false;
        }
      

        else if (playerAnim.GetBool("isFire") == false && playerAnim.GetBool("isJumping") ==false && playerAnim.GetBool("isRoll") == false && isFiring==false)
        {
            playerAnim.SetBool("isFire", true);
            runningSpeed = 0;

            StartCoroutine(Shoot());
        }
    
    }

    public void setJumping()
    {
        if(playerAnim.GetBool("isJumping") == false)
        {
            audioSource.clip = jumpAudio;
            audioSource.Play();
            StartCoroutine(Jumping());
        }
    }
    IEnumerator Jumping()
    {

        playerCollider.center = new Vector3(0, 1.87f, 0);
        playerAnim.SetBool("isJumping", true);
        //playerAnim.Play("Jump");
        yield return new WaitForSeconds(.5f); //.5f
        playerAnim.SetBool("isJumping", false);
        playerCollider.center = new Vector3(0, 0.8871949f, 0);


    }

    public void setRoll()
    {
        if(playerAnim.GetBool("isRoll") == false)
        {
            StartCoroutine(Rolling());
        }
    }

    IEnumerator Rolling()
    {
        playerCollider.center = new Vector3(0, 0.51f, 0);
        playerAnim.SetBool("isRoll", true);
        yield return new WaitForSeconds(.5f);
        playerAnim.SetBool("isRoll", false);
        playerCollider.center = new Vector3(0, 0.8871949f, 0);
    }
        
    
    IEnumerator Shoot()
    {

        isFiring = true;
        GameObject arrow = Instantiate(arrowObject, new Vector3(gameObject.transform.position.x+0.1f, gameObject.transform.position.y + 0.4f, gameObject.transform.position.z + 5f), transform.rotation);

        audioSource.clip = bowAudio;
        audioSource.Play();
        arrow.GetComponent<Rigidbody>().AddForce(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 25f), ForceMode.Impulse);

        yield return new WaitForSeconds(.5f);
        if (playerAnim.GetBool("isFire") == true && isFiring==true)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            isFiring = false;
            playerAnim.SetBool("isFire", false);
        }
   



    }

    private void StopFiring()
    {
     

            playerAnim.SetBool("isFire", false);
            if (endGame==false)
            {
                SetRunningSpeed();

            }

    }

    private void SetRunningSpeed()
    {
        runningSpeed = 10f;
    }

    private void OnCollisionEnter(Collision collision)
    {
     
        if (collision.collider.CompareTag("Obstacles"))
        {
            Debug.Log("Touched OBSTACLE");
            Debug.Log(collision.gameObject.name);

            if (died == false)
            {
                PlayerDied();
            }
            /* for after jump of the tree...
            if (playerAnim.GetBool("isJumping"))
            {
                
                Debug.Log("Touched OBSTACLE BUT Jumping");
            }
            else if (playerAnim.GetBool("isJumping")==false)
            {
                if (died == false)
                {
                    PlayerDied();
                }


            }*/
        }

        if (collision.collider.CompareTag("Asteroid"))
        {
            Debug.Log("Touched ASTEROID");
            if(died == false)
            {
                PlayerDied();
            }

         

        }
    }

    public bool PlayerDied()
    {

        audioSource.clip = deathMusic;
        audioSource.Play();
        
        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        playerAnim.SetBool("isDefeat", true);
        playerAnim.SetBool("isFire", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isRoll", false);
        playerAnim.SetBool("isVictory", false);
        endGame = true;
        runningSpeed = 0;
        restartPanel.SetActive(true);
        cameraAnim.Play("DeadCamAnim");
        died = true;
        return true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "End")
        {
            EndLevel();
        }

        if (other.CompareTag("Coin"))
        {
            coin++;
            coinText.text = coin.ToString();
            audioSource.clip = coinAudio;
            audioSource.Play();
        }

        if (other.CompareTag("SilverCoin"))
        {
            audioSource.clip = silverCoinAudio;
            audioSource.Play();
            tempSilverColor = silverCoinImage.color;
            tempSilverColor.a = 200f;
            silverCoinImage.color = tempSilverColor;
            silverCoin = true;
  

        }
    }

    private void EndLevel()
    {
        audioSource.clip = hyperMusic; audioSource.Play();
        endGame = true;
        runningSpeed = 0;
        playerAnim.SetBool("isVictory", true);
        Debug.Log("Completed");
        transform.Rotate(transform.rotation.x, 180, transform.rotation.z, Space.Self);
        cameraAnim.Play("DeadCamAnim");
        gameManager.EndLevel(coin, silverCoinImage.color, SceneManager.GetActiveScene().buildIndex-1, silverCoin);

    }


}
