using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahS : MonoBehaviour
{
    Animator anim;
    public bool isFire = false;
    public bool isReloadning = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Fire()
    {
        anim.SetBool("isFire", true);
        isFire = true;
    }

    public void Realode()
    {
        anim.SetBool("isReloading", true);
        anim.SetBool("isFire", false);
        isFire = false;
        isReloadning = true;
    }

    public void FinishRealode()
    {
        anim.SetBool("isReloading", false);
        isReloadning = false;
    }
}
