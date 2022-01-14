using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverManager : MonoBehaviour
{
    [Header("Arena Stuff")]
    public int difficulty;
    public string mode;
    public int _arena; //0 = wüste, 1 = Winter, 2 = Dschungel, 3 = lava

    [Header("Money")]
    public int setMoney;

    [Header("In Game")]
    private int _roundKills;

    [Header("Quest 1")]
    private int kills;

    [Header("Quest 2")]
    private int spentMoney;

    [Header("Quest 3")]
    private int winsInArena;

    [Header("Quest 4")]
    private int useSpecials;

    [Header("Quest 5")]
    private int playedGames;

    [Header("Quest 6")]
    private int wins;

    MoneyManager _moneyManager;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            mode = "";
            difficulty = 0;
            setMoney = 0;
        }

        _moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        kills = PlayerPrefs.GetInt("Q1");
        spentMoney = PlayerPrefs.GetInt("Q2");
        winsInArena = PlayerPrefs.GetInt("Q3");
        useSpecials = PlayerPrefs.GetInt("Q4");
        playedGames = PlayerPrefs.GetInt("Q5");
        wins = PlayerPrefs.GetInt("Q6");

        _roundKills = 0;
    }

    void Update()
    {
        PlayerPrefs.SetInt("Q6", wins);
    }

    public void Q2(int amount)
    {
        spentMoney += amount;
        PlayerPrefs.SetInt("Q2", spentMoney);
    }
    

    public int Kill
    {
        set
        {
            _roundKills += value;
        }
        get
        {
            return _roundKills;
        }
    }
}
