using UnityEngine;
using System.Collections;

public class Towers : MonoBehaviour
{
    [Header("Werte")]
    public int damag = 100;
    public int health = 100;
    public int kosten;

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public string art;
    public string seltenheit;
    public float range = 15f;
    public bool isNah = false;
    public bool isNone = false;
    public bool isFlamefrower = false;
    public bool isPlaying = false;
    public bool isLoader = false;
    public ParticleSystem fire;


    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Laser Towers")]
    public bool useLaser = false;
    public bool iceLaser = false;
    public bool fireLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform[] firePoint;

    // Use this for initialization
    void Start()
    {     
        if (isNone == false)
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }     
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isNone == false)
        {
            if (target == null)
            {
                if(isFlamefrower == true && !fire.isStopped)
                {
                    fire.Stop();
                }

                if (useLaser)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                        impactEffect.Stop();
                        impactLight.enabled = false;
                    }
                }

                return;
            }

            LockOnTarget();

            if (useLaser)
            {
                UpdateTarget();
                Laser();
            }

            else if (isFlamefrower && !fire.isPlaying)
            {
                fire.Play();
            }

            else
            {
                if (fireCountdown <= 0f)
                {
                    if (isNah == true)
                    {
                        GetComponent<NahS>().Fire();
                    }
                    else
                    {
                        Shoot();
                    }
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
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

    void Laser()
    {
        if(target != null)
        {
            Enemy targetEnem = target.GetComponent<Enemy>();
            targetEnem.TakeDamage(damageOverTime * Time.deltaTime);

            if(iceLaser)
            {
                targetEnem.Slow(slowAmount);
            }
            else if (fireLaser)
            {
                targetEnem.OnFire(0.2f, 2);
            }
           

            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                impactLight.enabled = true;
            }

            if(target != null)
            {
                Vector3 targetPosition = target.position;
                targetPosition.y += 4f;

                lineRenderer.SetPosition(0, firePoint[0].position);
                lineRenderer.SetPosition(1, targetPosition);

                Vector3 dir = firePoint[0].position - target.position;

                impactEffect.transform.position = target.position + dir.normalized;

                impactEffect.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }

    void Shoot()
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            if(isLoader == true)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint[i]);
                bullet.transform.parent = firePoint[i].transform;
            }
            else
            {
                GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint[i].position, firePoint[i].rotation);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
