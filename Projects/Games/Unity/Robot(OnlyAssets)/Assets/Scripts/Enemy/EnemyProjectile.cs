using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public LayerMask collisionMask;
    public Color trailColour;
    public float speed = 10;
    public int damage = 100;

    public GameObject deathEffect;   

    //Gun gun;

    float lifetime = 3;
    float skinWidth = .1f;

    void Start()
    {

        Destroy(gameObject, lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);

        GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColour);
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            Destroy(this.gameObject);
        }
    }
}
