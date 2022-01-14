using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleShootTouch : MonoBehaviour
{
    private bool touched;
    private int pointerID;
    private bool canFire;

    public int test;

    void Awake ()
    {
        touched = false;
    }

    public void OnPointerDown (PointerEventData data)
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            canFire = true;
            test++;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            canFire = false;
            touched = false;
            test++;
        }
    }

    public bool CanFire()
    {
        return canFire;
    }
}
