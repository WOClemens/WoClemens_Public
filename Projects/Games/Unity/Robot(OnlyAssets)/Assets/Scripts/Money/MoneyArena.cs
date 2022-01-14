 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyArena : MonoBehaviour
{
    MoneyManager myMoneyManager;

    [Header("Arena Kosten")]
    public int[] arenaKosten;

    [Header("Mode Page")]
    public GameObject ModePage;
    OverManager _overManager;


    void Start()
    {
        ModePage.SetActive(false);
        myMoneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        _overManager = GameObject.FindWithTag("OverManager").GetComponent<OverManager>();
    }

    
    void Update()
    {
        
    }

    public void PlayArena (int schwierigkeit)
    {
        if(myMoneyManager.money >= arenaKosten[schwierigkeit])
        {
            ModePage.SetActive(true);
            PlayerPrefs.SetInt("SetMoney", arenaKosten[schwierigkeit]);
            _overManager.difficulty = schwierigkeit + 1;
        }
        else
        {
            GameObject.Find("SceneLoader").GetComponent<GameManager>().Show_NoMoney_Error();
        }
    }

    public void ModePageOff()
    {
        ModePage.SetActive(false);
    }
}
