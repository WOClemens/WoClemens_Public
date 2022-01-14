using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 800.0f;
    public bool isBullte = false;

    void Update()
    {
        if(isBullte == true)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }

        else
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
