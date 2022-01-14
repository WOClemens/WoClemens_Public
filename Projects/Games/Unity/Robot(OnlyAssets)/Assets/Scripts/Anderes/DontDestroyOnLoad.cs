using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    ParticleSystem system;

    private void Start()
    {
        system = GetComponent<ParticleSystem>();
    }

    public void Fire()
    {
        system.Play();
    }
}
