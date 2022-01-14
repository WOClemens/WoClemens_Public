using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quests : MonoBehaviour
{
    [Header("Quest 1")]//Bestimmte anzahl an gegner töten
    public Text _q1Text;
    public int kills;
    public int AllKills;//500
    public int q1Be;//4 000
    public GameObject sliderQ1;
    public GameObject doneQ1;

    [Header("Quest 2")]//Bestimmte zahl an Geld ausgeben
    public Text _q2Text;
    public int spentMoney;
    public int AllspentMoney;//2 000
    public int q2Be;//10 000
    public GameObject sliderQ2;
    public GameObject doneQ2;

    [Header("Quest 3")]//Gewinne so und so viele Spiele in einer bestimmten Arena
    public Text _q3Text;
    public int winsInArena;
    public int AllwinsIn;//10
    public int q3Be;//1 000
    public GameObject sliderQ3;
    public GameObject doneQ3;

    [Header("Quest 4")]//Setzte deie specialfähiglkeit ein
    public Text _q4Text;
    public int useSpecials;
    public int AlluseSpecials;//100
    public int q4Be;//2 000
    public GameObject sliderQ4;
    public GameObject doneQ4;

    [Header("Quest 5")]//spiele soviele spiele
    public Text _q5Text;
    public int playedGames;
    public int AllplayedGames;//100
    public int q5Be;//15 000
    public GameObject sliderQ5;
    public GameObject doneQ5;

    [Header("Quest 6")]//gewinne soviele spiele
    public Text _q6Text;
    public int wins;
    public int AllWins;//100
    public int q6Be;//40 000
    public GameObject sliderQ6;
    public GameObject doneQ6;

    int firstTime;
    MoneyManager _moneyManager;

    void Start()
    {
        firstTime = PlayerPrefs.GetInt("firstTime");
        if(firstTime == 0)
        {
            PlayerPrefs.SetInt("Q1", 0);
            PlayerPrefs.SetInt("Q2", 0);
            PlayerPrefs.SetInt("Q3", 0);
            PlayerPrefs.SetInt("Q4", 0);
            PlayerPrefs.SetInt("Q5", 0);
            PlayerPrefs.SetInt("Q6", 0);
        }

        _moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        kills = PlayerPrefs.GetInt("Q1");
        spentMoney = PlayerPrefs.GetInt("Q2");
        winsInArena = PlayerPrefs.GetInt("Q3");
        useSpecials = PlayerPrefs.GetInt("Q4");
        playedGames = PlayerPrefs.GetInt("Q5");
        wins = PlayerPrefs.GetInt("Q6");

        QOne();
        QTwo();
        QThree();
        QFour();
        QFive();
        QSix();

        _q1Text.text = kills.ToString() + " / " + AllKills.ToString();
        _q2Text.text = spentMoney.ToString() + " / " + AllspentMoney.ToString();
        _q3Text.text = winsInArena.ToString() + " / " + AllwinsIn.ToString();
        _q4Text.text = useSpecials.ToString() + " / " + AlluseSpecials.ToString();
        _q5Text.text = playedGames.ToString() + " / " + AllplayedGames.ToString();
        _q6Text.text = wins.ToString() + " / " + AllWins.ToString();
    }

    void QOne() // Wird in Enemy bei der Methode Die erhöht
    {
        sliderQ1.SetActive(true);
        doneQ1.SetActive(false);
        if (kills >= AllKills)
        {
            _moneyManager.money += q1Be;
            kills = -100000;
        }

        if(kills < 0)
        {
            sliderQ1.SetActive(false);
            doneQ1.SetActive(true);
            kills = -10000;
        }

        PlayerPrefs.SetInt("Q1", kills);
    }

    void QTwo () // Wird öffters gesetzt darum auch der zugriff auf overmanager
    {
        sliderQ2.SetActive(true);
        doneQ2.SetActive(false);
        if (spentMoney >= AllspentMoney)
        {
            _moneyManager.money += q2Be;
            spentMoney = -100000;
        }

        if (spentMoney < 0)
        {
            sliderQ2.SetActive(false);
            doneQ2.SetActive(true);
            spentMoney = -10000;
        }

        PlayerPrefs.SetInt("Q2", spentMoney);
    }

    void QThree() // Wird in GameManager in der Mehtode GameEnds erhöht
    {
        sliderQ3.SetActive(true);
        doneQ3.SetActive(false);
        if (winsInArena >= AllwinsIn)
        {
            _moneyManager.money += q3Be;
            winsInArena = -100000;
        }

        if (winsInArena < 0)
        {
            sliderQ3.SetActive(false);
            doneQ3.SetActive(true);
            winsInArena = -10000;
        }

        PlayerPrefs.SetInt("Q3", winsInArena);
    }

    void QFour()// Wird in FaehigkeitenIngame Methode Special erhöht
    {
        sliderQ4.SetActive(true);
        doneQ4.SetActive(false);
        if (useSpecials >= AlluseSpecials)
        {
            _moneyManager.money += q4Be;
            useSpecials = -100000;
        }

        if (useSpecials < 0)
        {
            sliderQ4.SetActive(false);
            doneQ4.SetActive(true);
            useSpecials = -10000;
        }

        PlayerPrefs.SetInt("Q4", useSpecials);
    }

    void QFive() // Wird in GameManager LoadArena erhöht
    {
        sliderQ5.SetActive(true);
        doneQ5.SetActive(false);
        if (playedGames >= AllplayedGames)
        {
            _moneyManager.money += q5Be;
            playedGames = -100000;
        }

        if (playedGames < 0)
        {
            sliderQ5.SetActive(false);
            doneQ5.SetActive(true);
            playedGames = -10000;
        }

        PlayerPrefs.SetInt("Q5", playedGames);
    }

    void QSix() // Wird bei GameManager in der Methode  GameEnds erhöht
    {
        sliderQ6.SetActive(true);
        doneQ6.SetActive(false);
        if (wins >= AllWins)
        {
            _moneyManager.money += q6Be;
            wins = -100000;
        }

        if (wins < 0)
        {
            sliderQ6.SetActive(false);
            doneQ6.SetActive(true);
            wins = -10000;
        }

        PlayerPrefs.SetInt("Q6", wins);
    }


    void Update()
    {
        
    }
}
