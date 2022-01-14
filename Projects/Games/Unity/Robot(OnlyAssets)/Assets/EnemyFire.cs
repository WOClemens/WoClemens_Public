using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [Header("General")]
    public Transform partToRotate;
    private Transform target;
    public float turnSpeed = 10f;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public int shootingSpeed = 10;

    [Header("Timer")]
    public float timer = 0f;
    public float range = 15f;

    GameObject nearestEnemy;
    private Transform targetTower;

    OverManager _overManager;

    public float bulletTime;

    // Start is called before the first frame update
    void Start()
    {
        _overManager = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
        if (_overManager.mode == "Path" || _overManager.mode == "Survive")
        {
            nearestEnemy = GameObject.FindWithTag("Player");
        }
        else if (_overManager.mode == "Defend" || _overManager.mode == "Train")
        {
            nearestEnemy = GameObject.FindWithTag("Target");
        }
        target = nearestEnemy.transform;

        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

        timer += Time.deltaTime;
        LookOnTarget();
        if (distanceToEnemy <= range)
        {
            if (timer >= shootingSpeed)
            {
                Shoot(target);
                timer = 0;
            }
        }



    }

    void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot(Transform aimTarget)
    {
        GameObject go = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        go.GetComponent<FireProjectile>().target = aimTarget;
        Destroy(go, bulletTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
