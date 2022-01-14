using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsGame : MonoBehaviour
{
    [Header("Quest 1")]
    int kills;

    [Header("Quest 2")]
    int spentMoney;

    [Header("Quest 3")]
    int winsInArena;

    [Header("Quest 4")]
    int useSpecials;

    [Header("Quest 5")]
    int playedGames;

    [Header("Quest 6")]
    int wins;

    MoneyManager _moneyManager;

    void Start()
    {
        _moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        kills = PlayerPrefs.GetInt("Q1");
        spentMoney = PlayerPrefs.GetInt("Q2");
        winsInArena = PlayerPrefs.GetInt("Q3");
        useSpecials = PlayerPrefs.GetInt("Q4");
        playedGames = PlayerPrefs.GetInt("Q5");
        wins = PlayerPrefs.GetInt("Q6");
    }

    void Update()
    {
        PlayerPrefs.SetInt("Q1", kills);
        PlayerPrefs.SetInt("Q2", spentMoney);
        PlayerPrefs.SetInt("Q3", winsInArena);
        PlayerPrefs.SetInt("Q4", useSpecials);
        PlayerPrefs.SetInt("Q5", playedGames);
        PlayerPrefs.SetInt("Q6", wins);

    }
}
