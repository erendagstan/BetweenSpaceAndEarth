using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

    public int level;
   
    public Image[] levels = new Image[4];
    public Image[] levels_start = new Image[4];

    public Color completedLevelColor;
    public Color complatedStartColor;
    public Color defaultSilverColor;

    string concatSilver = "SilverLev";
    string concatCoin = "CoinLev";
    public Color black;

    public Image[] SilveCoins = new Image[4];
    public Image[] Coins = new Image[4];
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("TopLevel");

        Debug.Log("LEVEL SCRIPT - LEVEL : "+ level);
        completedLevelColor = levels[0].GetComponent<Image>().color;
        completedLevelColor.a = 200f;
        complatedStartColor = completedLevelColor;

        /*
        PlayerPrefs.SetInt(concat + "1", 1);

        if (PlayerPrefs.GetInt(concat + "1") == 1)
        {
            SilveCoins[0].GetComponent<Image>().color = black;

        }
        */

        for (int i = 0; i < SilveCoins.Length; i++)
        {
            int k = i+1;
            if (PlayerPrefs.GetInt(concatSilver +k) == 1)
            {
                SilveCoins[i].GetComponent<Image>().color = black;
            }
            else
            {
                SilveCoins[i].GetComponent<Image>().color = defaultSilverColor;
            }
        }

        for (int i = 0; i < Coins.Length; i++)
        {
            int k = i + 1;
            if (PlayerPrefs.GetInt(concatCoin+ k) >1)
            {
                Coins[i].GetComponent<Image>().color = complatedStartColor;
                Coins[i].GetComponentInChildren<TextMeshProUGUI>().text = "X" + PlayerPrefs.GetInt(concatCoin + k).ToString();
            }
        }




        if (level > 0)
        {
            for(int i =0; i<level; i++)
            {
                levels[i].GetComponent<Image>().color = completedLevelColor;
                levels_start[i].GetComponent <Image>().color = complatedStartColor;

                /*
                if (PlayerPrefs.GetInt(concatSilver + "i") == 1)
                {
                    SilveCoins[i].GetComponent<Image>().color = black;
                }
                */
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
