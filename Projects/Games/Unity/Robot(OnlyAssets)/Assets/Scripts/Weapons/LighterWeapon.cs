using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LighterWeapon : MonoBehaviour, GunInterface
{
    public GameObject lineRadius;
    public GameObject bullet;
    public Transform spawnPoint;
    public int shootSpeed;
    public int damag;
    float timer;
    bool isAimed;

    [Header("UI")]
    public Image chargeBar;

    void Start()
    {
        chargeBar = GameObject.Find("charge").GetComponent<Image>();
        isAimed = false;
        timer = 0;
        lineRadius.SetActive(false);
    }

    void Update()
    {
        if(isAimed == true)
        {
            timer += Time.deltaTime;
        }
        chargeBar.fillAmount = timer / shootSpeed;
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, spawnPoint);
        Destroy(go, 0.5f);
        timer = 0;
    }

    public void NotifyShowLine()
    {
        isAimed = true;
        lineRadius.SetActive(true);
    }

    public void NotifyHideLine()
    {
        lineRadius.SetActive(false);
        if(timer >= shootSpeed)
        {
            Shoot();
        }
        timer = 0;
        isAimed = false;
    }

    public int GetDamag()
    {
        return damag;
    } 

    public string GetGun()
    {
        return null;
    }

    public bool GetAim()
    {
        return false;
    }
}
