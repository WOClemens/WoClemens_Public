using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feuerball : MonoBehaviour
{
    public GameObject feuerball;

    void Start()
    {
        
    }

    void Update()
    {
        

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Instantiate(feuerball, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
