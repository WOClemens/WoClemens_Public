using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShop : MonoBehaviour
{
    const string key = "player";
    public string isBought = "false";
    bool isFalse = false;
    bool isPrevious = false;

    [Header("Ausgewählter Spieler")]
    public int usePlayer = 0;
    int seePlayer = 0;

    [Header("Texte & Image")]
    public string[] playerText;
    public Text playerinfoText;
    public Text buyText;
    public Text kostenText;
    public Image image_start;
    public Sprite[] sprite_players;

    [Header("Kosten")]
    public int[] playerKosten;

    [Header("Anderes")]
    public Text[] text_selcted;
    public GameObject image_Juwel;

    MoneyManager moneyManager;

    
    WeaponSaving players;

    void Start()
    {
        usePlayer = PlayerPrefs.GetInt("usePlayer");
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        players = GameObject.Find("WeaponSaving").GetComponent<WeaponSaving>();
        seePlayer = 0;
        playerinfoText.text = playerText[seePlayer];
        kostenText.text = playerKosten[seePlayer].ToString();
        SelectPlayer(0);
        SeeSelected();
    }

    private void Awake()
    {
        image_start.sprite = sprite_players[usePlayer];
    }

    void Update()
    {
        if(players.players[seePlayer] == "true")
        {
            buyText.text = "Select";
            kostenText.text = " ";
        }

        else
        {
            buyText.text = "Buy";
        }
    }

    public void SelectPlayer(int player)
    {
        seePlayer = player;
        playerinfoText.text = playerText[seePlayer];
        kostenText.text = playerKosten[seePlayer].ToString();
        if (players.players[seePlayer] == "true")
        {
            image_Juwel.SetActive(false);
        }
        else
        {
            image_Juwel.SetActive(true);
        }
    }

    public void SwitchOffPlayerhop ()
    {
        isFalse = false;
    }
    

    public void BuyOrSelctPlayer()
    {
        if(players.players[seePlayer] == "false")
        {
            Buy();
        }

        else
        {
            usePlayer = seePlayer;
            PlayerPrefs.SetInt("usePlayer", usePlayer);
            SeeSelected();
        }
    }

    void Buy()
    {
        if (moneyManager.money >= playerKosten[seePlayer])
        {
            GameObject.Find("OverManager(Clone)").GetComponent<OverManager>().Q2(playerKosten[seePlayer]);
            moneyManager.money -= playerKosten[seePlayer];
            players.players[seePlayer] = "true";
            PlayerPrefs.SetInt("usePlayer", usePlayer);
        }
        players.SetStringArray(key ,players.players);
    }

    void SeeSelected ()
    {
        for(int i = 0; i < text_selcted.Length; i++)
        {
            if(i == usePlayer)
            {
                text_selcted[i].text = "Selected";
            }
            else
            {
                text_selcted[i].text = "";
            }
        }
        image_start.sprite = sprite_players[usePlayer];
    }
}
