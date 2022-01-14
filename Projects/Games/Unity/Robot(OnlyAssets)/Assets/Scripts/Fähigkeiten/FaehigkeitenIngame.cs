using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaehigkeitenIngame : MonoBehaviour
{
    [Header("Faehigkeiten")]
    public int _fire;
    public int _ice;
    public int _titan;
    public int _heal;
    public int _points;
    int _fireAmo;
    int _iceAmo;
    int _titanAmo;
    int _healAmo;
    int _pointsAmo;

    [Header("Effecte")]
    public GameObject PS_SpawnEffect;
    public GameObject PS_HealEffect;
    public Transform Spawn_PSHeal;

    [Header("Text")]
    public Text text_FireAmo;
    public Text text_IceAmo;
    public Text text_TitanAmo;
    public Text text_HealAmo;
    public Text text_PointsAmo;

    public GameObject _f0Fire;
    public GameObject _titanObject;
    public Transform _specialSpawn;
    PlayerLive _pLive;
    OverManager _ovMang;
    GameInventory _gameInv;
    JoystickPlayerExample _joyStick;
    Vector3 titanSpawn;


    void Start()
    {
        _joyStick = GameObject.FindWithTag("Player").GetComponent<JoystickPlayerExample>();
        _pLive = GameObject.FindWithTag("Player").GetComponent<PlayerLive>();
        _ovMang = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
        _gameInv = GameObject.Find("TurmUnsichtbar").GetComponent<GameInventory>();

        _fire = PlayerPrefs.GetInt("fire");
        _ice = PlayerPrefs.GetInt("ice");
        _titan = PlayerPrefs.GetInt("titan");
        _heal = PlayerPrefs.GetInt("heal");
        _points = PlayerPrefs.GetInt("points");

        _fireAmo = PlayerPrefs.GetInt("AOne");
        _iceAmo = PlayerPrefs.GetInt("ATwo");
        _titanAmo = PlayerPrefs.GetInt("AThree");
        _healAmo = PlayerPrefs.GetInt("AFour");
        _pointsAmo = PlayerPrefs.GetInt("AFive");

        text_FireAmo.text = _fire.ToString();
        text_IceAmo.text = _ice.ToString();
        text_TitanAmo.text = _titan.ToString();
        text_HealAmo.text = _points.ToString();
    }
    public void Special(int special)
    {
        int useSpecials = PlayerPrefs.GetInt("Q4");
        useSpecials++;
        PlayerPrefs.SetInt("Q4", useSpecials);
        switch (special)
        {
            case 0:
                if(_fire > 0)
                {
                    _fire--;
                    PlayerPrefs.SetInt("fire", _fire);
                    VoidOnFire();
                }
                break;
            case 1:
                if(_ice > 0)
                {
                    _ice--;
                    PlayerPrefs.SetInt("ice", _ice);
                    EnumIce();
                }
                break;
            case 2:
                if (_titan > 0)
                {
                    _titan--;
                    PlayerPrefs.SetInt("titan", _titan);
                    StartCoroutine(VoidSpawnTitan());
                }
                break;
            case 3:
                if (_heal > 0)
                {
                    _heal--;
                    PlayerPrefs.SetInt("heal", _heal);
                    StartCoroutine(EnumHeal());
                }
                break;
            case 4:
                if(_points > 0)
                {
                    _points--;
                    PlayerPrefs.SetInt("points", _points);
                    StartCoroutine(VoidPoints());
                }
                break;
        }
        text_FireAmo.text = _fire.ToString();
        text_IceAmo.text = _ice.ToString();
        text_TitanAmo.text = _titan.ToString();
        text_HealAmo.text = _points.ToString();
    }

    private void VoidOnFire()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<Enemy>().OnFire(8, 20);
        }
    }

    private void EnumIce()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyMovement>().OnIce(10);
        }
    }
    IEnumerator VoidSpawnTitan()
    {
        titanSpawn = new Vector3(_specialSpawn.position.x, _specialSpawn.position.y, _specialSpawn.position.z);
        GameObject spawnEffect = (GameObject)Instantiate(PS_SpawnEffect, new Vector3(titanSpawn.x, titanSpawn.y - 27f, titanSpawn.z), Quaternion.identity);
        yield return new WaitForSeconds(4f);
        GameObject titan = Instantiate(_titanObject, titanSpawn, Quaternion.identity);
        Destroy(spawnEffect, 3.5f);
        Destroy(titan, 8f);
    }
    IEnumerator EnumHeal()
    {
        GameObject healEffect = (GameObject)Instantiate(PS_HealEffect, Spawn_PSHeal);
        if (_pLive.pathLives < 10)
        {
            _pLive.pathLives += 2;
        }
        for (int i = 0; i < 5; i++)
        {
            if(_pLive.playerLives < 100)
            {
                _pLive.Heal(10);
            }
            yield return new WaitForSeconds(1f);
        }
        Destroy(healEffect);
    }
    IEnumerator VoidPoints()
    {
        for (int i = 0; i < 25; i++)
        {
            _gameInv.AddPoints(2);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator EnumFlitz()
    {
        _joyStick.FlitzF3();
        yield return new WaitForSeconds(0.1f);
    }


    public void FirePlus()
    {
        _fireAmo++;
        PlayerPrefs.SetInt("AOne", _fireAmo);
    }
    public void IcePlus()
    {
        _iceAmo++;
        PlayerPrefs.SetInt("ATwo", _iceAmo);
    }
    public void TitanPlus()
    {
        _titanAmo++;
        PlayerPrefs.SetInt("AThree", _titanAmo);
    }
    public void HealPlus()
    {
        _healAmo++;
        PlayerPrefs.SetInt("AFour", _healAmo);
    }

    public void PointsPlus()
    {
        _pointsAmo++;
        PlayerPrefs.SetInt("AFive", _pointsAmo);
    }



    void Update()
    {
        text_FireAmo.text = _fire.ToString();
        text_IceAmo.text = _ice.ToString();
        text_TitanAmo.text = _titan.ToString();
        text_HealAmo.text = _heal.ToString();
        //text_PointsAmo.text = _points.ToString();
    }
}
