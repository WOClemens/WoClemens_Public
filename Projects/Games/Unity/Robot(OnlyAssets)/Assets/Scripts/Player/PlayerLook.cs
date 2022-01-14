using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    public Joystick lookJoystick;
    public float lookSpeed;
    Quaternion lookTo;

    //Gun gun;
    GunInterface gun;

    public string weaponName;
    bool canShoot;

    private void Start()
    {
        canShoot = false;
        //gun = GameObject.FindWithTag("Gun").GetComponent<Gun>();
        gun = GameObject.FindWithTag("Gun").GetComponent<GunInterface>();
        /*switch (weaponName)
        {
            case "Lock":
                gun = GameObject.FindWithTag("Gun").GetComponent<LookWeapon>();
                break;
            case "Blitz":
                gun = GameObject.FindWithTag("Gun").GetComponent<GunInterface>();
                break;
            case "Lighter":
                gun = GameObject.FindWithTag("Gun").GetComponent<GunInterface>();
                break;
        }*/
    }

    void Update()
    {
        switch (weaponName)
        {
            case "Lock":
                Lock();
                break;
            case "Blitz":
                Blitz();
                break;
        }

        Vector3 moveVector = (Vector3.right * lookJoystick.Horizontal + Vector3.forward * lookJoystick.Vertical);

        if (moveVector != Vector3.zero)
         {
            if (lookJoystick.Horizontal >= 0.6 || lookJoystick.Horizontal <= -0.6 || lookJoystick.Vertical >= 0.6 || lookJoystick.Vertical <= -0.6)
            {
                //gun.Shoot();
            }

            lookTo = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookTo, Time.deltaTime * lookSpeed);
        }
    }

    void Lock()
    {
        if (lookJoystick.Horizontal >= 0.1 || lookJoystick.Horizontal <= -0.1 || lookJoystick.Vertical >= 0.1 || lookJoystick.Vertical <= -0.1)
        {
            gun.NotifyShowLine();
        }
        else
        {
            gun.NotifyHideLine();
        }
    }

    void Blitz()
    {
        if (lookJoystick.Horizontal >= 0.1 || lookJoystick.Horizontal <= -0.1 || lookJoystick.Vertical >= 0.1 || lookJoystick.Vertical <= -0.1)
        {
            canShoot = true;
            gun.NotifyShowLine();
        }
        if (lookJoystick.Horizontal == 0 || lookJoystick.Vertical == 0)
        {
            if(canShoot == true)
            {
                gun.NotifyHideLine();
            }
            canShoot = false;
        }
    }
}
