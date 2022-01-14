using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    MoneyArena myMoneyArena;

    [Header("Geld")]
    public int money;
    public int firstMoney;

    [Header("UI Elemente")]
    public Text moneytext;

    int winOrLose = 0;
    public int firstTime;
    public int setMoney = 0;

    void Start()
    {
        firstTime = PlayerPrefs.GetInt("firstTime");
        if (firstTime == 0)
        {
            money = firstMoney;
            PlayerPrefs.SetInt("firstTime", 1);
            PlayerPrefs.SetInt("Money", money);
        }
        money = PlayerPrefs.GetInt("Money");
        winOrLose = PlayerPrefs.GetInt("WinOrLose");
        myMoneyArena = GameObject.Find("MoneyManager").GetComponent<MoneyArena>();
        setMoney = PlayerPrefs.GetInt("SetMoney");
        
        if(winOrLose == 1)
        {
            money = money + (2 * setMoney);
            winOrLose = -1;
            PlayerPrefs.SetInt("WinOrLose", winOrLose);
            PlayerPrefs.SetInt("SetMoney", 0);
        }

        else if(winOrLose == 0)
        {
            winOrLose = -1;
            PlayerPrefs.SetInt("WinOrLose", winOrLose);
        }
    }

    void Update()
    {
        PlayerPrefs.SetInt("Money", money);
        moneytext.text = money.ToString();
    }
}
