using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowersLive : MonoBehaviour
{
    Projectile projectile;
    Towers towers;
    float startHealth;
    float health = 0;
    int kosten;
    int towerLevel;
    public GameObject deathEffect;
    public string[] enemyBulletNames;

    public Image healthBar;

    bool inBuildZone = false;
    bool isFirstPlace = true;

    void Start()
    {
        towers = GetComponent<Towers>();
        health = towers.health;
        kosten = towers.kosten;
        startHealth = towers.health;
        /*towerLevel = PlayerPrefs.GetInt(tower);
        for (int i = 0; i < towerLevel; i++)
        {
            health = health + 100;
        }*/

        //health = 200;
    }


    void Die()
    {
        //GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);

        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(inBuildZone == false)
        {
            if(isFirstPlace == true)
            {
                isFirstPlace = false;
                GameObject.Find("Panel_SwitchMode").GetComponent<BuildManager>().BuildError(kosten);
            }
           
            Destroy(gameObject);
        }
        for(int i = 0; i < enemyBulletNames.Length; i++)
        {
            if(col.gameObject.name == enemyBulletNames[i] + "(Clone)")
            {
                projectile = GameObject.Find(enemyBulletNames[i] + "(Clone)").GetComponent<Projectile>();
                TakeDamage(projectile.damag);
            }
        }     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BuildZone")
        {
            inBuildZone = true;
        }
    }
}
