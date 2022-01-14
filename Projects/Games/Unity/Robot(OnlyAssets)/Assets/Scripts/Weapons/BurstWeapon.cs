using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurstWeapon : MonoBehaviour, GunInterface
{

    [Header("Shooting")]
    public GameObject bullet;
    public Transform spawnPoint;
    public int burstCount;
    public float waitBeetweenShoot;
    public int waitBeetweenBurst;
    public int damag;
   
    public float timer;

    [Header("LineRenderer + Gradient")]
    public LineRenderer lr;
    public GameObject laserend;
    public GameObject laserstart;

    [Header("UI")]
    public Image chargeBar;

    void Start()
    {
        chargeBar = GameObject.Find("charge").GetComponent<Image>();
        timer = 0;
        lr.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        chargeBar.fillAmount = timer / waitBeetweenBurst;

        if (lr.enabled)
        {
            lr.SetPosition(0, laserstart.transform.position);
            lr.SetPosition(1, laserend.transform.position);
        }
    }

    IEnumerator Shoot()
    {
        for(int i = 0; i <= burstCount; i++)
        {
            GameObject go = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            Destroy(go, 2);
            yield return new WaitForSeconds(waitBeetweenShoot);
        }
        timer = 0;
    }

    public void NotifyShowLine()
    {
        lr.enabled = true;
    }

    public void NotifyHideLine()
    {
        lr.enabled = false;
        if (timer >= waitBeetweenBurst)
        {
            Debug.Log("Shoot");
            StartCoroutine("Shoot");
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
