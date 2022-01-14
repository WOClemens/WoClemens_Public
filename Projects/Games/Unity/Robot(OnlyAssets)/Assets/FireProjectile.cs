using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public float speed;
    public float fireTime;
    public GameObject fireArea;
    public GameObject fireSpawn;
    public float turnSpeed;
    public Transform target;

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        if(target != null)
        {
            LockOnTarget();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Explode();
    }

    void Explode()
    {
        GameObject go = (GameObject)Instantiate(fireArea, fireSpawn.transform.position, fireSpawn.transform.rotation);
        Destroy(go, fireTime);
        Destroy(this.gameObject);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
