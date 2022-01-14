using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [Header("General")]
    public Transform partToRotate;
    private Transform target;
    public float turnSpeed = 10f;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public int shootingSpeed;
    public int waitBetwenLaser;
    public float bulletTime;

    [Header("Timer")]
    public float timer = 0f;
    public float range = 15f;
    public string towerTag = "Tower";

    GameObject nearestEnemy;
    private Transform targetTower;

    OverManager _overManager;
    public LineRenderer lineRenderer;

    public Gradient lineGradient;
    public Gradient lockedGradient;

    follow foll;

    bool isShooting = false;
    bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
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

        if (_overManager.mode == "Survive")
        {
            foll = GetComponent<follow>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);

        timer += Time.deltaTime;

        NewLaser();
        if (timer > shootingSpeed + bulletTime + waitBetwenLaser) // laser wird wieder eingeschalten
        {
            timer = 0;
            lineRenderer.enabled = true;
            isLocked = false;
            if (_overManager.mode == "Survive")
            {
                foll.Speed();
            }
        }
        else if (timer > shootingSpeed + bulletTime) // Laser wurde ausgeschaltet // es wird geschossen
        {
            if (isShooting == false)
            {
                isShooting = true;
                lineRenderer.enabled = false;
                Shoot();
            }
        }
        else if (timer >= shootingSpeed) // Ziel wurde geLocket gegner dreht sich nicht mehr 
        {
            if (isLocked == false)
            {
                isLocked = true;
                Vector3 targetPosition = target.position;
                LockOnTarget(targetPosition);
            }
            if (_overManager.mode == "Survive")
            {
                foll.Stop();
            }
        }
        else // Ziel wird verfolgt
        {
            //lineRenderer.material.color = lineColor;
            lineRenderer.colorGradient = lineGradient;
            isShooting = false;
            //ShowLaser();
            LookOnTarget();
        }

        /*if (_overManager != null && distanceToEnemy <= range)// schießt nur auf ein ziel indiesem fall den Zug
        {
            LockOnTarget();
            ShowLaser();
            if (timer >= shootingSpeed)
            {
                timer = 0;
                Shoot();
            }
        }*/
    }

    void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void NewLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, firePoint.forward*1000);
    }


    void LockOnTarget(Vector3 targetPos)
    {
        if (target != null)
        {

            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                //impactEffect.Play();
                //impactLight.enabled = true;
            }

            if (target != null)
            {
                //targetPosition.y -= 4f;
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, targetPos);
                //lineRenderer.material.color = lockedColor;
                lineRenderer.colorGradient = lockedGradient;

                Vector3 dir = firePoint.position - target.position;

                //impactEffect.transform.position = target.position + dir.normalized;

                //impactEffect.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    void Shoot()
    {
        GameObject go = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);

        Destroy(go, bulletTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
