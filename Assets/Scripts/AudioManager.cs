using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public Button audioButton;

    public AudioClip backgroundMusic;

    [Header("----Music Sprites ----")]
    public Sprite musicOffSprite;
    public Sprite musicOnSprite;

    /*
    [SerializeField] AudioSource audioSource;


    [Header("----Audio Clip ----")]
    public AudioClip background;
    public AudioClip death;
    */


    private void Awake()
    {
        
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
      
        }
    }

    private void Start()
    {
        audioButton = GetComponent<Button>();
        musicOffSprite = GetComponent<Sprite>(); 
        musicOnSprite = GetComponent<Sprite>();
    }

  
    


}

   



