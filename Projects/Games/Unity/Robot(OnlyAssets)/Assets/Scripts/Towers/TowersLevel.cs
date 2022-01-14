using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersLevel : MonoBehaviour
{
    public string tower;
    int towerLevel;

    public GameObject Tower1;
    public GameObject Tower2;
    public GameObject Tower3;
    public GameObject Tower4;
    public GameObject Tower5;

    void Start()
    {
        towerLevel = PlayerPrefs.GetInt(tower);
    }

    void Update()
    {
        towerLevel = PlayerPrefs.GetInt(tower);

        if (towerLevel == 1 || towerLevel == 2)
        {
            Tower1.SetActive(true);
            Tower2.SetActive(false);
            Tower3.SetActive(false);
            Tower4.SetActive(false);
            Tower5.SetActive(false);

        }

        if (towerLevel == 3 || towerLevel == 4)
        {
            Tower1.SetActive(false);
            Tower2.SetActive(true);
            Tower3.SetActive(false);
            Tower4.SetActive(false);
            Tower5.SetActive(false);
        }

        if (towerLevel == 5 || towerLevel == 6)
        {
            Tower1.SetActive(false);
            Tower2.SetActive(false);
            Tower3.SetActive(true);
            Tower4.SetActive(false);
            Tower5.SetActive(false);
        }

        if (towerLevel == 7 || towerLevel == 8)
        {
            Tower1.SetActive(false);
            Tower2.SetActive(false);
            Tower3.SetActive(false);
            Tower4.SetActive(true);
            Tower5.SetActive(false);
        }

        if (towerLevel == 9 || towerLevel == 10)
        {
            Tower1.SetActive(false);
            Tower2.SetActive(false);
            Tower3.SetActive(false);
            Tower4.SetActive(false);
            Tower5.SetActive(true);
        }
    }
}
