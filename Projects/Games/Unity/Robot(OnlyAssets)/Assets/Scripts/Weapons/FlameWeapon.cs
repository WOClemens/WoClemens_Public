using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameWeapon : MonoBehaviour, GunInterface
{
    public ParticleSystem fire;
    public int _amount;
    public int damag;
    float timer;
    bool isAimed;
    bool canShoot;
    bool isPlaying;

    [Header("UI")]
    public Image chargeBar;

    void Start()
    {
        chargeBar = GameObject.Find("charge").GetComponent<Image>();
        canShoot = false;
        fire.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAimed == true && timer <= _amount)
        {
            timer += Time.deltaTime * 1.2f;
        }
        else if(timer > 0)
        {
            timer -= Time.deltaTime * 0.7f;
        }
        chargeBar.fillAmount = timer / _amount;
    }

    public void NotifyShowLine()
    {
        if(timer <= _amount - 0.2)
        {
            canShoot = true;
            isAimed = true;
            if (!isPlaying)
            {
                fire.Play();
                isPlaying = true;
            }
        }
        else
        {
            fire.Stop();
            canShoot = false;
        }
    }

    public void NotifyHideLine()
    {
        fire.Stop();
        isPlaying = false;
        canShoot = false;
        isAimed = false;
    }

    public int GetDamag()
    {
        return damag;
    }

    public string GetGun()
    {
        return "flame";
    }
    public bool GetAim()
    {
        return canShoot;
    }
}
