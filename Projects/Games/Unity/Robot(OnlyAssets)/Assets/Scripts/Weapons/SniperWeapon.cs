using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperWeapon : MonoBehaviour, GunInterface
{

    [Header("Shooting")]
    public GameObject bullet;
    public Transform spawnPoint;
    public float waitBeetweenShoot;
    public int damag;

    float timer;

    [Header("LineRenderer + Gradient")]
    private LineRenderer lr;
    public GameObject laserend;
    public GameObject laserstart;

    [Header("UI")]
    public Image chargeBar;

    void Start()
    {
        chargeBar = GameObject.Find("charge").GetComponent<Image>();
        lr = GetComponent<LineRenderer>();
        timer = 0;
        lr.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        chargeBar.fillAmount = timer / waitBeetweenShoot;

        if (lr.enabled)
        {
            lr.SetPosition(0, laserstart.transform.position);
            lr.SetPosition(1, laserstart.transform.forward * 1000);
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        Destroy(go, 3);
        timer = 0;
    }

    public void NotifyShowLine()
    {
        lr.enabled = true;
    }

    public void NotifyHideLine()
    {
        lr.enabled = false;
        if (timer >= waitBeetweenShoot)
        {
            Shoot();
        }
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
