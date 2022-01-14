using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int weaponSlots;

    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;
    public GameObject Weapon4;
    public GameObject Weapon5;
    public GameObject Weapon6;
    public GameObject Weapon7;
    public GameObject Weapon8;
    public GameObject Weapon9;
    public GameObject Weapon10;
    public GameObject Weapon11;
    public GameObject Weapon12;
    public GameObject Weapon13;
    public GameObject Weapon14;
    public GameObject Weapon15;
    public GameObject Weapon16;
    public GameObject Weapon17;
    public GameObject Weapon18;
    public GameObject Weapon19;
    public GameObject Weapon20;
    public GameObject Weapon21;
    public GameObject Weapon22;
    public GameObject Weapon23;
    public GameObject Weapon24;
    public GameObject Weapon25;

    void Start()
    {
        weaponSlots = PlayerPrefs.GetInt("WeaponSelection");

        PlayerPrefs.SetInt("WeaponSelection", weaponSlots);
    }

    
    void Update()
    {
        PlayerPrefs.SetInt("WeaponSelection", weaponSlots);
    }

    void GameStart ()
    {
        switch (weaponSlots)
        {
            case 1:
                Instantiate(Weapon1, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(Weapon2, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(Weapon3, transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(Weapon4, transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(Weapon5, transform.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(Weapon6, transform.position, Quaternion.identity);
                break;
            case 7:
                Instantiate(Weapon7, transform.position, Quaternion.identity);
                break;
            case 8:
                Instantiate(Weapon8, transform.position, Quaternion.identity);
                break;
            case 9:
                Instantiate(Weapon9, transform.position, Quaternion.identity);
                break;
            case 10:
                Instantiate(Weapon10, transform.position, Quaternion.identity);
                break;
            case 11:
                Instantiate(Weapon11, transform.position, Quaternion.identity);
                break;
            case 12:
                Instantiate(Weapon12, transform.position, Quaternion.identity);
                break;
            case 13:
                Instantiate(Weapon13, transform.position, Quaternion.identity);
                break;
            case 14:
                Instantiate(Weapon14, transform.position, Quaternion.identity);
                break;
            case 15:
                Instantiate(Weapon15, transform.position, Quaternion.identity);
                break;
            case 16:
                Instantiate(Weapon16, transform.position, Quaternion.identity);
                break;
            case 17:
                Instantiate(Weapon17, transform.position, Quaternion.identity);
                break;
            case 18:
                Instantiate(Weapon18, transform.position, Quaternion.identity);
                break;
            case 19:
                Instantiate(Weapon19, transform.position, Quaternion.identity);
                break;
            case 20:
                Instantiate(Weapon20, transform.position, Quaternion.identity);
                break;
            case 21:
                Instantiate(Weapon21, transform.position, Quaternion.identity);
                break;
            case 22:
                Instantiate(Weapon22, transform.position, Quaternion.identity);
                break;
            case 23:
                Instantiate(Weapon23, transform.position, Quaternion.identity);
                break;
            case 24:
                Instantiate(Weapon24, transform.position, Quaternion.identity);
                break;
            case 25:
                Instantiate(Weapon25, transform.position, Quaternion.identity);
                break;
        }
        
    }
}
