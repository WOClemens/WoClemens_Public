using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzProjectil : MonoBehaviour
{
    public Transform[] targets;
    GunInterface test;
    public float speed = 5f;
    int count;

    void Start()
    {
        count = 0;
        //target = GameObject.FindWithTag("Enemy").transform;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);

        LookOnTarget();
    }
    void LookOnTarget()
    {
        if(targets[count] != null)
        {
            Vector3 dir = targets[count].position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 800).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(count >= 2)
        {
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Enemy")
        {
            count++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
