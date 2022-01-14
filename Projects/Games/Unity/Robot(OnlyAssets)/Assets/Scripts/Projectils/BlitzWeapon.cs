using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzWeapon : MonoBehaviour, GunInterface
{
    GameObject[] enemys;

    [Header("LineRenderer + Gradient")]
    private LineRenderer lr;
    public Transform laserend;
    public Transform laserstart;
    public Gradient normalGradient;
    public Gradient aimGradient;

    public int range;
    public Transform spawnPoint;
    public GameObject bullet;
    public int damag;
    bool isAimOn;

    private void Start()
    {
        //chargeBar = GameObject.Find("charge").GetComponent<Image>();
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        isAimOn = false;
    }

    void FindEnemys(Vector3 point, GameObject enemyHit)
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject temp = null;

        for (int write = 0; write < enemys.Length; write++)
        {
            for (int sort = 0; sort < enemys.Length - 1; sort++)
            {
                float distanceToEnemy1 = Vector3.Distance(point, enemys[sort].transform.position);
                float distanceToEnemy2 = Vector3.Distance(point, enemys[sort + 1].transform.position);
                if(enemys[sort + 1] == enemyHit)
                {
                    distanceToEnemy2 = Mathf.Infinity;
                }
                else if(enemys[sort] == enemyHit)
                {
                    distanceToEnemy1 = Mathf.Infinity;
                }
                if (distanceToEnemy1 > distanceToEnemy2)
                {
                    temp = enemys[sort + 1];
                    enemys[sort + 1] = enemys[sort];
                    enemys[sort] = temp;
                }
            }
        }
        for(int i = 0; i < enemys.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemys[i].transform.position);
            Debug.Log(distanceToEnemy);
        }
    }

    void Update()
    {
        lr.SetPosition(0, laserstart.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.collider.tag == "Enemy")
            {
                isAimOn = true;
                lr.colorGradient = aimGradient;
                FindEnemys(hit.point, hit.transform.gameObject);
                lr.positionCount = 4;
                lr.SetPosition(1, hit.point);
                if(enemys[0] != null){
                    lr.SetPosition(2, new Vector3(enemys[0].transform.position.x, laserstart.transform.position.y, enemys[0].transform.position.z));
                }
                if(enemys[1] != null)
                {
                    lr.SetPosition(3, new Vector3(enemys[1].transform.position.x, laserstart.transform.position.y, enemys[1].transform.position.z));
                }              
            }
            else
            {
                isAimOn = false;
                lr.colorGradient = normalGradient;
                lr.positionCount = 2;
                lr.SetPosition(1, laserend.position);
            }
        }
        else
        {
            isAimOn = false;
            lr.colorGradient = normalGradient;
            lr.positionCount = 2;
            lr.SetPosition(1, laserend.position);
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        go.GetComponent<BlitzProjectil>().targets[0] = enemys[enemys.Length - 1].transform;
        go.GetComponent<BlitzProjectil>().targets[1] = enemys[0].transform;
        go.GetComponent<BlitzProjectil>().targets[2] = enemys[1].transform;
    }

    public void NotifyShowLine()
    {
        lr.enabled = true;
    }

    public void NotifyHideLine()
    {
        lr.enabled = false;
        if (isAimOn == true)
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
