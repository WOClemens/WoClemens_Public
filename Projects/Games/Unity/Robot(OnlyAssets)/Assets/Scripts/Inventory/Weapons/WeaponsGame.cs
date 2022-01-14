using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsGame : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] players;

    public int useWeapon = 0;
    public int usePlayer = 0;

    public Transform SpawnPoint;
    public Transform SpawnPointPlayer;

    void Start()
    {
        usePlayer = PlayerPrefs.GetInt("usePlayer");
        useWeapon = PlayerPrefs.GetInt("useWeapon");
        if (weapons[useWeapon] != null)
        {
            Instantiate(weapons[useWeapon], SpawnPoint);
            Debug.Log(useWeapon);
        }

        else
        {
            Instantiate(weapons[0], SpawnPoint);
        }

        Instantiate(players[usePlayer], SpawnPointPlayer);
    }
}
