using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLive : MonoBehaviour
{
    [Header("Lives")]
    public int pathLives;
    public int playerLives;
    public float startHealt;

    Projectile projectile;
    public bool isGameLost;
    public bool isGameWin;

    public string[] enemyNames;
    public Image healthBar;

    public bool isTrain = false;

    [Header("Effects")]
    public GameObject PS_FireEffect;
    public Transform Spawn_PS;
    JoystickPlayerExample joyMove;
    EnemyMovement enemyMov;

    GameManager _gameManager;

    [Header("Fire")]
    public float timer = 0;
    public int fireDamag;
    public bool onFire;
    public float fireTimer;

    void Start()
    {
        /*if (SceneManager.GetActiveScene().name == "WPath")
        {
            playerLives = 1000;
        }*/
        _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        if(isTrain == false)
        {
            joyMove = this.GetComponent<JoystickPlayerExample>();
        }
        else
        {
            enemyMov = this.GetComponent<EnemyMovement>();
        }

        isGameLost = false;
        isGameWin = false;

    }

    void Update()
    {

    }

    public void TakePathDamag()
    {
        pathLives--;
        if (pathLives <= 0)
        {
            _gameManager.GameEnds("lost");
        }
    }


    void TakeDamag(int amount)
    {
        playerLives -= amount;
        startHealt = playerLives / 100f;

        healthBar.fillAmount = startHealt;

        if (playerLives <= 0)
        {
            _gameManager.GameEnds("lost");
        }
    }

    public void Heal(int amount)
    {
        playerLives += amount;
        startHealt = playerLives / 100f;

        healthBar.fillAmount = startHealt;
    }

    void OnCollisionEnter(Collision col)
    {
        for(int i = 0; i < enemyNames.Length; i++)
        {
            if (col.gameObject.name == enemyNames[i] + "(Clone)")
            {
                projectile = GameObject.Find(enemyNames[i] + "(Clone)").GetComponent<Projectile>();
                if(col.gameObject.name == "EnemyP4(Clone)" || col.gameObject.name == "EnemyP7(Clone)")
                {
                    OnFire(5, projectile.damag);
                }
                else if (col.gameObject.name == "EnemyP5(Clone)" || col.gameObject.name == "EnemyP6(Clone)")
                {
                    if(isTrain == false)
                    {
                        joyMove.OnIce(3);
                    }
                    else
                    {
                        enemyMov.OnIce(3);
                    }
                    TakeDamag(projectile.damag);
                }
                else
                {
                    TakeDamag(projectile.damag);
                }
            }
        }
    }

    void OnFire(float time, int damag)
    {
        StartCoroutine(EnumOnFire(time, damag));
    }
    private IEnumerator EnumOnFire(float seconds, int fireDamgePS)
    {
        fireTimer += seconds;
        if (onFire == false)
        {
            onFire = true;
            GameObject fireEffect = (GameObject)Instantiate(PS_FireEffect, Spawn_PS);
            for (int i = 0; i < fireTimer; i++)
            {
                TakeDamag(fireDamgePS);
                yield return new WaitForSeconds(1f);
            }
            Destroy(fireEffect);
            onFire = false;
            fireTimer = 0;
        }
    }

    void OnTriggerStay(Collider other)// Wenn er im Fire bullet stehen bleibt
    {
        if (other.tag == "Fire")
        {
            timer += Time.deltaTime;
            if(timer >= 0.2)
            {
                TakeDamag(fireDamag);
                timer = 0;
            }
        }
    }
}
