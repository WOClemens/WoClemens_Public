using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotImage : MonoBehaviour
{
    public int towerSlot1 = 1;
    public int towerSlot2 = 2;
    public int towerSlot3 = 3;
    public int towerSlot4 = 4;

    public Image towerSlotOne;
    public Image towerSlotTwo;
    public Image towerSlotThree;
    public Image towerSlotFour;

    public GameObject SlotOne;
    public GameObject SlotTwo;
    public GameObject SlotThree;
    public GameObject SlotFour;

    public Sprite[] towerImages; 

    void Start()
    {
        towerSlot1 = PlayerPrefs.GetInt("TowerInvent1");
        towerSlot2 = PlayerPrefs.GetInt("TowerInvent2");
        towerSlot3 = PlayerPrefs.GetInt("TowerInvent3");
        towerSlot4 = PlayerPrefs.GetInt("TowerInvent4");

        if(towerSlot1 < 0)
        {
            SlotOne.SetActive(false);
        }
        if (towerSlot2 < 0)
        {
            SlotTwo.SetActive(false);
        }
        if (towerSlot3 < 0)
        {
            SlotThree.SetActive(false);
        }
        if (towerSlot4 < 0)
        {
            SlotFour.SetActive(false);
        }
    }

    void Update()
    {

        Towers(towerSlot1, towerSlotOne);
        Towers(towerSlot2, towerSlotTwo);
        Towers(towerSlot3, towerSlotThree);
        Towers(towerSlot4, towerSlotFour);

    }

    void Towers(int tower, Image towerSlot)
    {

        for (int i = 0; i < towerImages.Length; i++)
        {
            if(i == tower)
            {
                towerSlot.sprite = towerImages[i];
            }
        }

    }
}


