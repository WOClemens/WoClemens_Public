using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    //[HideInInspector]
    public float speed;

    public float health = 100;

    public int worth = 50;

    public GameObject deathEffect;

    //public AudioClip hitAudio;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;
    public bool isTrain;

    [Header("Tower Names")]
    public string[] towerNames;

    [Header("Enemy Names")]
    public string[] enemiesNames;

    [Header("Build Points")]
    public int enemyPoints;

    [Header("Effects")]
    public GameObject PS_FireEffect;
    public GameObject PS_IceEffect;
    public Transform Spawn_PS;

    Survive survive; 
    PlayerLive playerLive; 

    GunInterface gun;
    Projectile towerProjectile;
    BuildManager points; 
    Towers tow;
    OverManager _overMan; 


    public bool isOnFire = false;
    int firedamag;
    float onFireSeconds;
    float startHealth;
    bool isFire = false;

    void Start()
    {
        speed = startSpeed;
        gun = GameObject.FindWithTag("Gun").GetComponent<GunInterface>();
        _overMan = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
        health = health * _overMan.difficulty;
        health = 100; // nur für video
        startHealth = health;
        playerLive = GameObject.FindWithTag("Player").GetComponent<PlayerLive>(); 
        points = GameObject.Find("Panel_SwitchMode").GetComponent<BuildManager>();  // Neure BuildManager 
        survive = GameObject.Find("WaveManager").GetComponent<Survive>(); 
        
        //speed = startSpeed;
    }

    public void Update()
    {
        if(playerLive.isGameLost == true)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator fireCoruntain;

    void OnCollisionEnter(Collision col)
    {   
        for (int i = 0; i < towerNames.Length; i++)
        {
            if (col.gameObject.name == towerNames[i] + "(Clone)")
            {
                towerProjectile = GameObject.Find(towerNames[i] + "(Clone)").GetComponent<Projectile>();
                TakeDamage(towerProjectile.damag);
                if (towerProjectile.isFire == true)
                {
                    OnFire(3, towerProjectile.damag / 6);
                }
                else if(towerProjectile.isIce == true)
                {
                    this.GetComponent<EnemyMovement>().OnIce(2);
                }
            }
        }

        for(int b = 0; b < enemiesNames.Length; b++)
        {
            if (col.gameObject.name == enemiesNames[b])
            {
                towerProjectile = GameObject.Find(enemiesNames[b]).GetComponent<Projectile>();
                TakeDamage(towerProjectile.damag);
            }
        }

        if (col.gameObject.tag == "Projectile" && this.tag != "Target")
        {
            TakeDamage(gun.GetDamag()); //gun.damag
            //AudioManager.instance.PlaySound(hitAudio, transform.position);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Speer")
        {
            tow = GameObject.Find("8(Clone)").GetComponent<Towers>();
            TakeDamage(tow.damag);
        }

        if (col.gameObject.name == "Schwert")
        {
            tow = GameObject.Find("15(Clone)").GetComponent<Towers>();
            TakeDamage(tow.damag);
        }

        if (col.gameObject.name == "Flamefrower")
        {
            tow = GameObject.Find("26(Clone)").GetComponent<Towers>();
            isOnFire = true;
            firedamag = tow.damag;
            fireCoruntain = FireFrower(firedamag, 1);
            StartCoroutine(fireCoruntain);
        }

        if (col.gameObject.name == "Flame")
        {          
            if(gun.GetGun() == "flame" && gun.GetAim())
            {
                isOnFire = true;
                fireCoruntain = FireFrower(gun.GetDamag(), 0.2f);
                StartCoroutine(fireCoruntain);
            }
        }

        if (col.gameObject.name == "F4(Clone)")
        {
            //AudioManager.instance.PlaySound(hitAudio, transform.position);
            TakeDamage(100);
        }
        if (col.gameObject.tag == "Projectile")
        {
            //AudioManager.instance.PlaySound(hitAudio, transform.position);
            TakeDamage(gun.GetDamag());
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Flamefrower")
        {
            isOnFire = false;
        }
        if (col.gameObject.name == "Flamefrower")
        {
            isOnFire = false;
        }
        if (col.gameObject.name == "Flame")
        {
            isOnFire = false;
        }
    }

    private IEnumerator FireFrower(int nowDamag, float time)
    {
        while (isOnFire)
        {
            yield return new WaitForSeconds(time);
            TakeDamage(nowDamag);
        }
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
       speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;

        //PlayerStats.Money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        //points.AddPoints(enemyTowerPoints); Alt
        points.AddPoints(enemyPoints); // Geht zum neuen BuildManager 
        survive.enemysAlive--;
        survive.allEnemys--; 
        _overMan.Kill = 1;
        int kills = PlayerPrefs.GetInt("Q1");
        kills++;
        PlayerPrefs.SetInt("Q1", kills);
        Destroy(gameObject);      
    }
    public void OnFire(float time, int damag)
    {
        if(this.tag == "Target")
        {
            TakeDamage(time * damag);
        }
        else
        {
            if (isFire == false)
            {
                isFire = true;
                StartCoroutine(EnumOnFire(time, damag));
            }
            else
            {
                onFireSeconds += time;
            }
        }          
    }
    private IEnumerator EnumOnFire(float seconds, float fireDamgePS)
    {
        onFireSeconds += seconds;
        GameObject fireEffect = (GameObject)Instantiate(PS_FireEffect, Spawn_PS);
        for (int i = 0; i < onFireSeconds; i++)
        {
            TakeDamage(fireDamgePS);
            yield return new WaitForSeconds(1f);
        }
        Destroy(fireEffect);
        isFire = false;     
    }
}