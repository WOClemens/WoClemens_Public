using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [Header("Typ")]
    public bool isPlayer;
    public bool isEnemy = false;
    public bool isTower = false;
    public bool isName;

    [Header("Blitz")]
    int blitzCount = 0;
    public int blitzCountMax = 5;
    private Enemy targetEnemy;
    private Transform target;

    [Header("General")]
    public LayerMask collisionMask;
    //public Color trailColour;
    public string enemyTag = "Enemy";
    public float speed = 10;
    public int damag = 0;
    public float turnSpeed = 10f;
    public GameObject deathEffect;
    public Transform partToRotate;
    public AudioClip deathClip;

    [Header("Turm Name")]
    public string towerName;

    [Header("Turm art")]
    public bool isFire;
    public bool isIce;

    Projectile projectile;
    Towers to;
    OverManager _overMan;

    public float lifetime = 3;
    float skinWidth = .1f;


    void Start()
    {
        if(isName == true)
        {
            towerName += "(Clone)";
            to = GameObject.Find(towerName).GetComponent<Towers>();
            damag = to.damag;
        }
        if ((isPlayer || isTower) && isEnemy == false)
            UpdateTarget();
        
        if(isEnemy)
        {
            _overMan = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
            damag = damag * _overMan.difficulty;
            if(isTower == true)
            {
                if(_overMan.mode == "Defend" || _overMan.mode == "Train")
                {
                    target = GameObject.FindWithTag("Target").transform;
                }
                else
                {
                    target = GameObject.FindWithTag("Player").transform;
                }
            }
        }

        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        
        //GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColour);

    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        if(target != null && (isPlayer || isTower))
        {
            LockOnTarget();
        }
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
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

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    void UpdateTargetNew()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        float distanceToEnemy;

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies == null)
            {
                Destroy(this.gameObject);
            }
            distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if(distanceToEnemy < shortestDistance && enemies[i].transform != target)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemies[i];
            }

        }
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnCollisionEnter(Collision col)
    {
        //AudioManager.instance.PlaySound(deathClip, transform.position);
        if (col.gameObject.tag == "Enemy" && isEnemy == false && gameObject.tag == "Blitz")
        {
            if (blitzCount >= blitzCountMax)
                Destroy(this.gameObject);

            else
                blitzCount++; UpdateTargetNew();
        }

        else
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            Destroy(this.gameObject);
        }
    }
}