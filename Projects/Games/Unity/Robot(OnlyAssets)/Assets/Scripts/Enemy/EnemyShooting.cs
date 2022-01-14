using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShooting : MonoBehaviour
{
    [Header("General")]
    public Transform partToRotate;
    private Transform target;
    public float turnSpeed = 10f;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform[] firePoint;
    public int shootingSpeed = 10;
    float timer = 0f;
    public float range = 15f;
    public string towerTag = "Tower";

    GameObject nearestEnemy;
    private Transform targetTower;

    OverManager _overManager;
    

    void Start()
    {
        _overManager = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
        if(_overManager.mode == "Path" || _overManager.mode == "Survive")
        {
            nearestEnemy = GameObject.FindWithTag("Player");
        }
        else if(_overManager.mode == "Defend" || _overManager.mode == "Train")
        {
            nearestEnemy = GameObject.FindWithTag("Target");
        }       
        target = nearestEnemy.transform;
              
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(towerTag);
        timer += Time.deltaTime;

        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

        if (_overManager != null && (_overManager.mode == "Train" || _overManager.mode == "Defend" || _overManager.mode == "Survive") && distanceToEnemy <= range)// schießt nur auf ein ziel indiesem fall den Zug
        {
            LockOnTarget();

            if (timer >= shootingSpeed)
            {
                timer = 0;
                Shoot();
            }
        }

        else if(_overManager.mode == "asfd") // schießt nur auf türme
        {
            UpdateTarget();

            if (targetTower != null)
            {
                LockOnTargetTower();
                if (timer >= shootingSpeed)
                {
                    timer = 0;
                    Shoot();
                }
            }
        }

        else if (_overManager.mode == "Path")//Schießt auf spieler und türme 
        {
            UpdateTarget();
            LockOnTarget();

            if (distanceToEnemy <= range && timer >= shootingSpeed)
            {
                timer = 0;
                Shoot();
            }

            else if (targetTower != null && timer >= shootingSpeed)
            {
                timer = 0;
                Shoot();
                LockOnTargetTower();
            }
        }


    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void LockOnTargetTower()
    {
        Vector3 dir = targetTower.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    void Shoot ()
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            GameObject bulletGO = (GameObject)Instantiate(bullet, firePoint[i].position, firePoint[i].rotation);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(towerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTower = null;
        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearestTower = tower;
            }
        }

        if (nearestTower != null && shortestDistance <= range)// hier könnte man regeln ob der gegner egal wo er ist auf einen turm schießen kann
        {
            targetTower = nearestTower.transform;
        }

        else
        {
            targetTower = null;
        }

    }
}
