using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool isSelection = false;

    public GameObject InfoPage;
    public GameObject StatsPage;

    public int towerSlot1 = 1;
    public int towerSlot2 = 2;
    public int towerSlot3 = 3;
    public int towerSlot4 = 4;

    public int usingTower = 0;

    public Image[] towers;

    public Image towerSlotOne;
    public Image towerSlotTwo;
    public Image towerSlotThree;
    public Image towerSlotFour;

    public Animator anim;

    public Sprite noneTower;

    public int firstTime = 0;

    void Awake()
    {
        firstTime = PlayerPrefs.GetInt("firstTime");
        //usingTower = PlayerPrefs.GetInt("usingTower");

        towerSlot1 = PlayerPrefs.GetInt("TowerInvent1");
        towerSlot2 = PlayerPrefs.GetInt("TowerInvent2");
        towerSlot3 = PlayerPrefs.GetInt("TowerInvent3");
        towerSlot4 = PlayerPrefs.GetInt("TowerInvent4");

        if(firstTime == 0)
        {
            
            towerSlot1 = -1;
            towerSlot2 = -1;
            towerSlot3 = -1;
            towerSlot4 = -1;

        }
    }

    void Update()
    {
        if(towerSlot1 == -1)
        {
            towerSlotOne.sprite = noneTower;
        }
        if (towerSlot2 == -1)
        {
            towerSlotTwo.sprite = noneTower;
        }
        if (towerSlot3 == -1)
        {
            towerSlotThree.sprite = noneTower;
        }
        if (towerSlot4 == -1)
        {
            towerSlotFour.sprite = noneTower;
        }
        PlayerPrefs.SetInt("TowerInvent1", towerSlot1);
        PlayerPrefs.SetInt("TowerInvent2", towerSlot2);
        PlayerPrefs.SetInt("TowerInvent3", towerSlot3);
        PlayerPrefs.SetInt("TowerInvent4", towerSlot4);

        //usingTower = PlayerPrefs.GetInt("usingTower");

        TowerSlot(towerSlot1, towers, towerSlotOne);
        TowerSlot(towerSlot2, towers, towerSlotTwo);
        TowerSlot(towerSlot3, towers, towerSlotThree);
        TowerSlot(towerSlot4, towers, towerSlotFour);

    }

    void TowerSlot (int towerSlotNumber/*die Number im inventar Slot*/, Image[] towerSlotButton /*die verschiedenen Button*/, Image towerSlot/*die 4 Buttons des Inventars*/)
    {
        for(int i = 0; i < towerSlotButton.Length; i++)
        {
            if(i == towerSlotNumber)
            {
                towerSlot.sprite = towerSlotButton[i].sprite;
            }
        }

    }

    public void TowerInvent1 ()
    {
        if (isSelection == true && usingTower != towerSlot2 && usingTower != towerSlot3 && usingTower != towerSlot4)
        {
            isSelection = false;
            anim.SetBool("isSelected", isSelection);
            towerSlot1 = usingTower;
        }
    }

    public void TowerInvent2 ()
    {
        if (isSelection == true && usingTower != towerSlot1 && usingTower != towerSlot2 && usingTower != towerSlot3)
        {
            isSelection = false;
            anim.SetBool("isSelected", isSelection);
            towerSlot2 = usingTower;
        }
    }

    public void TowerInvent3()
    {
        if (isSelection == true && usingTower != towerSlot1 && usingTower != towerSlot2 && usingTower != towerSlot4)
        {
            isSelection = false;
            anim.SetBool("isSelected", isSelection);
            towerSlot3 = usingTower;
        }
    }

    public void TowerInvent4()
    {
        if (isSelection == true && usingTower != towerSlot1 && usingTower != towerSlot2 && usingTower != towerSlot3)
        {
            isSelection = false;
            anim.SetBool("isSelected", isSelection);
            towerSlot4 = usingTower;
        }
    }

    public void TapOnImage(int tower)
    {
        usingTower = tower;
    }

    public void SelectTower()
    {       
        if(usingTower != towerSlot1 && usingTower != towerSlot2 && usingTower != towerSlot3 && usingTower != towerSlot4)
        {
            isSelection = true;
            anim.SetBool("isSelected", isSelection);
            InfoPage.SetActive(false);
            StatsPage.SetActive(false);
        }
    }

    public void Back()
    {
        isSelection = false;
        anim.SetBool("isSelected", isSelection);
    }

    public void InventoryOff()
    {
        anim.SetBool("isSelected", false);
        isSelection = false;
    }

}
