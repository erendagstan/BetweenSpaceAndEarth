using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public int level=0;
    [SerializeField]
    private TextMeshProUGUI levelText;
    public GameObject nextLevelPanel;
    public GameObject canvas;
    public Image silverCoinImage;
    public TextMeshProUGUI coinText;
    private Color unlockedColor;
    public Image[] levels;

    public Image sound;
    public Image soundOn;
    public Image soundOff;


    public GameObject infoPanel;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI loadingTextLevelScene;


    /*Example1
      public class RunCodeOnce : MonoBehaviour
{
    public static RunCodeOnce Instance;

    void Awake()
    {
        if (Instance!=null) { Destroy(gameObject); return; } // stops dups running
        DontDestroyOnLoad(gameObject); // keep me forever
        Instance = this; // set the reference to it

        ... code to run only once ...
    }
}*/

    /*Example2
     *   void Awake()
    {
        Init();
    }

    void Init()
    {
        if (!PlayerPrefs.HasKey("isInit"))
        {
            DoYourStuffHere();
            PlayerPrefs.SetInt("isInit", 1);
        }
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        /*
        PlayerPrefs.SetInt("TopLevel", 0);
        PlayerPrefs.SetInt("Level", 0);

        for(int i =1; i<5; i++)
        {
            PlayerPrefs.SetInt("CoinLev"+i, 0);
            PlayerPrefs.SetInt("SilverLev"+i, 0);
        }
        */

    }

    void Update()
    {
    
    }

    public void LevelScene()
    {
        SceneManager.LoadScene("LevelMenu");

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartLevel(int lvl)
    {
        if (PlayerPrefs.GetInt("Level") >= lvl)
        {
            SceneManager.LoadScene("Level" + lvl.ToString());
            loadingTextLevelScene.gameObject.SetActive(true);

        }
    }

    public void StartGame()
    {

        loadingText.gameObject.SetActive(true);

        int startNextLevel = PlayerPrefs.GetInt("TopLevel")+1;

        if(startNextLevel > 5 || startNextLevel<=0)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("Level" + startNextLevel.ToString());

        }
    }

    public void HomeAction()
    {
        SceneManager.LoadScene("Entry");
    }

   

        public void EndLevel(int coin, Color silverCoin, int levelFromPlayer, bool isSilver)
    {
        canvas.SetActive(false);
        nextLevelPanel.SetActive(true);
        silverCoinImage.color = silverCoin; 
        coinText.text = coin.ToString();

        
        if(levelFromPlayer==1 && PlayerPrefs.GetInt("TopLevel")==0)
        {
            PlayerPrefs.SetInt("TopLevel", levelFromPlayer);
        }

        
        if(levelFromPlayer> PlayerPrefs.GetInt("TopLevel"))
        {
            PlayerPrefs.SetInt("TopLevel", levelFromPlayer);
        }

        
        //if (levelFromPlayer > PlayerPrefs.GetInt("Level"))
        //{
            level = levelFromPlayer;
            PlayerPrefs.SetInt("Level", level);
         
        //}
        if (coin > 1)
        {
            Debug.Log("in coin case " + level);
            if (PlayerPrefs.GetInt("CoinLev" + level) < coin)
            {
                PlayerPrefs.SetInt("CoinLev" + level, coin);
                if (isSilver)
                {
                    PlayerPrefs.SetInt("SilverLev"+level, 1);
                } else
                {
                    PlayerPrefs.SetInt("SilverLev" + level, 0);
                }

            }

            /*
            switch (level)
            {
                case 4:
                    Debug.Log("in coin case 4");
                    if (PlayerPrefs.GetInt("CoinLev4") < coin)
                    {
                        PlayerPrefs.SetInt("CoinLev4", coin);
                        if (isSilver)
                        {

                            PlayerPrefs.SetInt("SilverLev4", 1);
                        }
                        else
                        {

                            PlayerPrefs.SetInt("SilverLev4", 0);
                        }
                    }
                    break;

                case 3:
                    Debug.Log("in coin case 3");
                    if (PlayerPrefs.GetInt("CoinLev3") < coin)
                    {
                        PlayerPrefs.SetInt("CoinLev3", coin);
                        if (isSilver)
                        {
                            PlayerPrefs.SetInt("SilverLev3", 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("SilverLev3", 0);
                        }
                    }
                    break;
                case 2:
                    Debug.Log("in coin case 2");
                    if (PlayerPrefs.GetInt("CoinLev2") < coin)
                    {
                        PlayerPrefs.SetInt("CoinLev2", coin);
                        if (isSilver)
                        {
                            PlayerPrefs.SetInt("SilverLev2", 1);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("SilverLev2", 0);
                        }
                    }
                    break;
                case 1:
                    Debug.Log("in coin case 1");
                    if (PlayerPrefs.GetInt("CoinLev1") < coin)
                    {
                        PlayerPrefs.SetInt("CoinLev1", coin);
                        if (isSilver)
                        {

                            Debug.Log("YesSilverrrrrr");
                            PlayerPrefs.SetInt("SilverLev1", 1);
                        }
                        else
                        {
                            Debug.Log("NoSilver");
                            PlayerPrefs.SetInt("SilverLev1", 0);
                        }
                    }
                    break;

                default:
                    Debug.Log("incorrect level");
                    break;

            }*/

}

        /*
            if (isSilver)
        {
            switch(level)
            {
                case 4:
                    Debug.Log("in silver case 4");
                    PlayerPrefs.SetInt("SilverLev4", 1);
                   
                    break;

                case 3:
                    Debug.Log("in silver case 3");
                    PlayerPrefs.SetInt("SilverLev3", 1);
                    break;  
                case 2:
                    Debug.Log("in silver case 2");
                    PlayerPrefs.SetInt("SilverLev2", 1);
                    break; 
                case 1:
                    Debug.Log("in silver case 1");
                    PlayerPrefs.SetInt("SilverLev1", 1);
                    break;

                default:
                    Debug.Log("incorrect level");
                    break;

            }
        } 
        */
    
        
        
    }

    public void CheckMute()
    {
        int x = PlayerPrefs.GetInt("audio");
        if(x == 1)
        {
            MuteAllSound();

        }
        else if(x == 0) {
            UnMuteAllSound();
        }
        else
        {
            PlayerPrefs.SetInt("audio", 1);
            MuteAllSound();
        }
    }

    public void MuteAllSound()
    {
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("audio", 0);
        sound.sprite = soundOn.sprite;
    }

    public void UnMuteAllSound()
    {
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("audio", 1);
        sound.sprite = soundOff.sprite;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnInfo()
    {
        infoPanel.SetActive(true);
    }
    public void OffInfo()
    {
        infoPanel.SetActive(false);
    }



}
