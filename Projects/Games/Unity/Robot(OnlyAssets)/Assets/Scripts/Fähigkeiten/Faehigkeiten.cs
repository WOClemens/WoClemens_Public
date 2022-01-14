using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Faehigkeiten : MonoBehaviour
{
    [Header("Faehigkeiten")]
    public int _fire;
    public int _ice;
    public int _titan;
    public int _heal;
    public int _points;

    [Header("Aufgabe 1")]//Baue so und soviele FeuerTürme // Fire
    public Text _a1Text;
    public int _a1Anzahl;
    public int _a1GesamtAnzahl;//500
    public Text text_fire;

    [Header("Aufgabe 2")]//Baue so und soviele Ice // Ice
    public Text _a2Text;
    public int _a2Anzahl;
    public int _a2GesamtAnzahl;//500
    public Text text_ice;

    [Header("Aufgabe 3")]//Baue so und soviele Legendäre // Titan
    public Text _a3Text;
    public int _a3Anzahl;
    public int _a3GesamtAnzahl;//500
    public Text text_titan;

    [Header("Aufgabe 4")]//Baue so und soviele Normale Türme // Heal
    public Text _a4Text;
    public int _a4Anzahl;
    public int _a4GesamtAnzahl;//500
    public Text text_heal;

    [Header("Aufgabe 5")]//Baue so und soviele Türme //points
    public Text _a5Text;
    public int _a5Anzahl;
    public int _a5GesamtAnzahl;//500
    public Text text_points;

    [Header("Anderes")]
    public GameObject InfoPage;
    public Text Text_Description;
    public Text Text_Name;
    public string[] _names;
    public string[] _descriptions;

    [Header("Pages")]
    public GameObject Page_Description;

    void Start()
    {
        Page_Description.SetActive(false);
        InfoPage.SetActive(false);
        _a1Anzahl = PlayerPrefs.GetInt("AOne");
        _a2Anzahl = PlayerPrefs.GetInt("ATwo");
        _a3Anzahl = PlayerPrefs.GetInt("AThree");
        _a4Anzahl = PlayerPrefs.GetInt("AFour");
        _a5Anzahl = PlayerPrefs.GetInt("AFive");

        _fire = PlayerPrefs.GetInt("fire");
        _ice = PlayerPrefs.GetInt("ice");
        _titan = PlayerPrefs.GetInt("titan");
        _heal = PlayerPrefs.GetInt("heal");
        _points = PlayerPrefs.GetInt("points");

        CheckAOne();
        CheckATwo();
        CheckAThree();
        CheckAFour();
        CheckAFive();

        _a1Text.text = _a1Anzahl.ToString() + "/10";
        _a2Text.text = _a2Anzahl.ToString() + "/10";
        _a3Text.text = _a3Anzahl.ToString() + "/10";
        _a4Text.text = _a4Anzahl.ToString() + "/10";
        //_a5Text.text = _a5Anzahl.ToString();

        text_fire.text = _fire.ToString();
        text_ice.text = _ice.ToString();
        text_titan.text = _titan.ToString();
        text_heal.text = _heal.ToString();
    }

    void CheckAOne()
    {
        if (_a1Anzahl >= _a1GesamtAnzahl)
        {
            _fire++;
            _a1Anzahl = 0;
        }
        PlayerPrefs.SetInt("AOne", _a1Anzahl);
        PlayerPrefs.SetInt("fire", _fire);
    }
    void CheckATwo()
    {
        if (_a2Anzahl >= _a2GesamtAnzahl)
        {
            _ice++;
            _a2Anzahl = 0;
        }
        PlayerPrefs.SetInt("ATwo", _a2Anzahl);
        PlayerPrefs.SetInt("ice", _ice);
    }
    void CheckAThree()
    {
        if (_a3Anzahl >= _a3GesamtAnzahl)
        {
            _titan++;
            _a3Anzahl = 0;
        }
        PlayerPrefs.SetInt("AThree", _a3Anzahl);
        PlayerPrefs.SetInt("titan", _titan);
    }
    void CheckAFour()
    {
        if (_a4Anzahl >= _a4GesamtAnzahl)
        {
            _heal++;
            _a4Anzahl = 0;
        }
        PlayerPrefs.SetInt("AFour", _a4Anzahl);
        PlayerPrefs.SetInt("heal", _heal);
    }
    void CheckAFive()
    {
        if (_a5Anzahl >= _a5GesamtAnzahl)
        {
            _points++;
            _a5Anzahl = 0;
        }
    }

    public void InfoPageOn(int number)
    {
        Text_Name.text = _names[number].ToString();
        Text_Description.text = _descriptions[number].ToString();
        InfoPage.SetActive(true);
    }

    public void InfoPageOff()
    {
        InfoPage.SetActive(false);
    }

    public void InfoPageForDescription ()
    {
        Page_Description.SetActive(true);
    }

    public void InfoPageForDescriptionOff ()
    {
        Page_Description.SetActive(false);
    }

    void Update()
    {

        //PlayerPrefs.SetInt("special1", _a1Anzahl);

    }


}
