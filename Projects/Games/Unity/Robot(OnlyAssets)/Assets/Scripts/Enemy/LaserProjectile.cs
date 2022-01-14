using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(this.gameObject);
    }
}
