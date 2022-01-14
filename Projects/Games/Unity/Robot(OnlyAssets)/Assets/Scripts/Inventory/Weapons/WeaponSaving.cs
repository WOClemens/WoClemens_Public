using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSaving : MonoBehaviour // Auch türme werden hier gespeichert
{
    public string[] weapons;
    public string[] towers;
    public string[] players;
    public string[] specials;
    const string _key = "weapons";
    const string towersKey = "towers";
    const string playerKey = "player";
    const string specialKey = "special";
    public Image[] gesperrt;
    public bool isNull = false;
    public int firstTime = 0;

    void Awake()
    {
        towers = new string[26] { "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false", "false" };
        weapons = new string[6] { "false", "false", "false", "false", "false", "false"};
        players = new string[9] { "false", "false", "false", "false", "false", "false", "false", "false", "false" };
        specials = new string[6] { "false", "false", "false", "false", "false", "false" };

        firstTime = PlayerPrefs.GetInt("firstTime");
        if(firstTime == 0)
        {
            SetStringArray(towersKey, towers);
            SetStringArray(_key, weapons);
            SetStringArray(playerKey, players);
            SetStringArray(specialKey, specials);
            firstTime = 1;
        }
        weapons = GetStringArray(_key);
        towers = GetStringArray(towersKey);
        players = GetStringArray(playerKey);
        specials = GetStringArray(specialKey);
        SetStringArray(_key, weapons);
        SetStringArray(towersKey, towers);
        SetStringArray(playerKey, players);
        SetStringArray(specialKey, specials);
    }

    void Update()
    {
        PlayerPrefs.SetInt("firstTime", firstTime);
        SetStringArray(towersKey, towers);
    }

    public void SetStringArray(string key, string[] values)
    {
        PlayerPrefs.SetInt(key + ".length", values.Length);
        int i = 0;
        for (; i < values.Length; i++)
        {
            PlayerPrefs.SetString(key + "[" + i + "]", values[i]);
        }       
    }

    public string[] GetStringArray(string key)
    {
        int length = 0;
        if(key == _key)
        {
            length = weapons.Length;
        }

        else if(key == towersKey)
        {
            length = towers.Length;
        }

        else if(key == playerKey)
        {
            length = players.Length;
        }

        else
        {
            length = specials.Length;
        }
        string[] result = new string[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = PlayerPrefs.GetString(key + "[" + i + "]");
        }
        return result;
    }

    public string GetStringAt(string key, int position)
    {
        string result = " ";

        result = PlayerPrefs.GetString(key + "[" + position + "]");

        return result;
    }

    public void SetToTrue(string key, int position)
    {
        if(key == _key)
        {
            weapons[position] = "true";
            PlayerPrefs.SetString(key + "[" + position + "]", weapons[position]);
            SetStringArray(key, weapons);
        }
        if (key == towersKey)
        {
            towers[position] = "true";
            PlayerPrefs.SetString(key + "[" + position + "]", towers[position]);
            SetStringArray(key, towers);
        }
        if (key == specialKey)
        {
            specials[position] = "true";
            PlayerPrefs.SetString(key + "[" + position + "]", specials[position]);
            SetStringArray(key, specials);
        }
    }  
}
