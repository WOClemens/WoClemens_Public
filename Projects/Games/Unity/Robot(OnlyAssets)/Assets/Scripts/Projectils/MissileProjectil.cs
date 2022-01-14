using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectil : MonoBehaviour
{
    public Transform target;
    GunInterface test;
    public float speed = 5f;
    public float rotateSpeed = 200f;

    void Start()
    {
        //target = GameObject.FindWithTag("Enemy").transform;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);

        LockOnTarget();
    }
    void LockOnTarget()
    {
        if(target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
