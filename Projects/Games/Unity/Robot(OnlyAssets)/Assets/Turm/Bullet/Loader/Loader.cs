using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject Projectile;
    public Transform BulletSpawn;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(Projectile, BulletSpawn.position, BulletSpawn.rotation);
        Destroy(this);
    }
}
