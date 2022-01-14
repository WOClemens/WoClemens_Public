using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public int destroyAfter;

    void Start()
    {
        Destroy(this.gameObject, destroyAfter);
    }
}
