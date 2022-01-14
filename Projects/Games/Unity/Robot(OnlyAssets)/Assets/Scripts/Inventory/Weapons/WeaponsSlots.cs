using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsSlots : MonoBehaviour
{
    const string key = "weapons";
    string boughtWeapon = " ";

    [Header("Waffe im gebrauch")]
    public int useWeapon = 0;

    [Header("Panel & Anime")]
    public GameObject Panel;
    public Animator anim;
    string seltenheid = " ";

    [Header("WeaponPages")]
    public GameObject WeaponsNormale;

    [Header("Buttons & Images")]
    public Button[] Weaponbuttons;
    public Sprite[] ImagesNormal;
    public Sprite[] ImagesSelten;
    public Sprite[] ImagesEpisch;
    public Sprite[] ImagesLegänder;

    [Header("Buttons & Images")]
    public Image InfoImage;

    public bool isFalse = false;

    public string savedSeltenheit = " ";

    public int weaponSaved = 0;

    public int weaponsSelected = 0;//Waffe die gerade ausgewählt ist

    WeaponSaving weapons;

    void Start()
    {
        weapons = GameObject.Find("WeaponSaving").GetComponent<WeaponSaving>();
        savedSeltenheit = PlayerPrefs.GetString("savedS");
        weaponSaved = PlayerPrefs.GetInt("WSeleted");
        switch (savedSeltenheit)
        {
            case "normale":
                seltenheid = "normale";
                ChangesImages(ImagesNormal);
                InfoImage.sprite = Weaponbuttons[weaponSaved - 1].image.sprite;
                break;
            case "selten":
                seltenheid = "selten";
                ChangesImages(ImagesSelten);
                InfoImage.sprite = Weaponbuttons[weaponSaved - 1].image.sprite;
                break;
            case "episch":
                seltenheid = "episch";
                ChangesImages(ImagesEpisch);
                InfoImage.sprite = Weaponbuttons[weaponSaved - 1].image.sprite;
                break;
            case "legend":
                seltenheid = "legend";
                ChangesImages(ImagesLegänder);
                InfoImage.sprite = Weaponbuttons[weaponSaved - 1].image.sprite;
                break;
            default:
                seltenheid = "normale";
                ChangesImages(ImagesNormal);
                InfoImage.sprite = Weaponbuttons[weaponSaved - 1].image.sprite;
                break;
        }

        isFalse = false;
        WeaponsNormale.SetActive(true);
        if(useWeapon <= 0)
        {
            useWeapon = 1;
            PlayerPrefs.SetInt("UseWeapon", useWeapon);
        }
    }

    public void SwitchToNormale()
    {
        isFalse = true;
        seltenheid = "normale";
    }

    public void SwitchToRare()
    {
        isFalse = true;
        seltenheid = "selten";
    }

    public void SwitchToEpic()
    {
        isFalse = true;
        seltenheid = "episch";
    }

    public void SwitchToLegend()
    {
        isFalse = true;
        seltenheid = "legend";
    }

    void SwitchOff()
    {
        isFalse = false;
    }

    void Update()
    {
        useWeapon = PlayerPrefs.GetInt("UseWeapon");
        anim.SetBool("isSwitch", isFalse);
    }

    void SwitchWeapons()//endert die waffen nach seltenheid
    {
        switch (seltenheid)
        {
            case "normale":
                ChangesImages(ImagesNormal);
                break;
            case "selten":
                ChangesImages(ImagesSelten);
                break;
            case "episch":
                ChangesImages(ImagesEpisch);
                break;
            case "legend":
                ChangesImages(ImagesLegänder);
                break;
        }

    }

    void ChangesImages(Sprite[] Images)
    {
        for (int i = 0; i < Weaponbuttons.Length; i++)
        {
            Weaponbuttons[i].image.sprite = Images[i];
        }
    }

    public void Weapon(int number)
    {
        switch(number)
        {
            case 1:
                InfoImage.sprite = Weaponbuttons[0].image.sprite;
                weaponsSelected = 1;
                break;
            case 2:
                InfoImage.sprite = Weaponbuttons[1].image.sprite;
                weaponsSelected = 2;
                break;
            case 3:
                InfoImage.sprite = Weaponbuttons[2].image.sprite;
                weaponsSelected = 3;
                break;
            case 4:
                InfoImage.sprite = Weaponbuttons[3].image.sprite;
                weaponsSelected = 4;
                break;

            case 5:
                InfoImage.sprite = Weaponbuttons[4].image.sprite;
                weaponsSelected = 5;
                break;
        }
    }

    public void SelectWeapon ()
    {
        weaponSaved = weaponsSelected;
        PlayerPrefs.SetInt("WSeleted", weaponSaved);
        savedSeltenheit = seltenheid;
        PlayerPrefs.SetString("savedS", savedSeltenheit);
        switch (seltenheid)
        {
            case "normale":
                switch (weaponsSelected)
                {
                    case 1:
                        boughtWeapon = weapons.GetStringAt(key, 15);
                        if(boughtWeapon == "true")
                        {
                            useWeapon = 23;
                        }
                        break;
                    case 2:
                        boughtWeapon = weapons.GetStringAt(key, 3);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 3;
                        }
                        break;
                    case 3:
                        boughtWeapon = weapons.GetStringAt(key, 7);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 10;
                        }
                        break;
                    case 4:
                        boughtWeapon = weapons.GetStringAt(key, 8);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 12;
                        }
                        break;
                    case 5:
                        boughtWeapon = weapons.GetStringAt(key, 16);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 24;
                        }
                        break;
                }
                break;
            case "selten":
                switch (weaponsSelected)
                {
                    case 1:
                        boughtWeapon = weapons.GetStringAt(key, 18);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 26;
                        }
                        break;
                    case 2:
                        boughtWeapon = weapons.GetStringAt(key, 20);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 29;
                        }
                        break;
                    case 3:
                        boughtWeapon = weapons.GetStringAt(key, 1);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 1;
                        }
                        break;
                    case 4:
                        boughtWeapon = weapons.GetStringAt(key, 13);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 20;
                        }
                        break;
                    case 5:
                        boughtWeapon = weapons.GetStringAt(key, 6);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 7;
                        }
                        break;
                }
                break;
            case "episch":
                switch (weaponsSelected)
                {
                    case 1:
                        boughtWeapon = weapons.GetStringAt(key, 9);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 14;
                        }
                        break;
                    case 2:
                        boughtWeapon = weapons.GetStringAt(key, 12);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 19;
                        }
                        break;
                    case 3:
                        boughtWeapon = weapons.GetStringAt(key, 14);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 21;
                        }
                        break;
                    case 4:
                        boughtWeapon = weapons.GetStringAt(key, 11);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 16;
                        }
                        break;
                    case 5:
                        boughtWeapon = weapons.GetStringAt(key, 2);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 2;
                        }
                        break;
                }
                break;
            case "legend":
                switch (weaponsSelected)
                {
                    case 1:
                        boughtWeapon = weapons.GetStringAt(key, 5);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 6;
                        }
                        break;
                    case 2:
                        boughtWeapon = weapons.GetStringAt(key, 4);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 5;
                        }
                        break;
                    case 3:
                        boughtWeapon = weapons.GetStringAt(key, 10);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 15;
                        }
                        break;
                    case 4:
                        boughtWeapon = weapons.GetStringAt(key, 19);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 27;
                        }
                        break;
                    case 5:
                        boughtWeapon = weapons.GetStringAt(key, 17);
                        if (boughtWeapon == "true")
                        {
                            useWeapon = 25;
                        }
                        break;
                }
                break;
        }


        PlayerPrefs.SetInt("UseWeapon", useWeapon);
    }
}
