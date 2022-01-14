using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float start_Speed;
    float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    Animator animator;

    [Header("Effects")]
    public GameObject PS_IceEffect;
    public Transform Spawn_PS;

    bool isAnima = true;

    bool isFrocen = false;
    float iceTimer;




    public Joystick lookJoystick;
    public float lookSpeed;
    Quaternion lookTo;

    //Gun gun;
    GunInterface gun;

    public string weaponName;
    bool canShoot;

    private void Start()
    {
        if (GameObject.Find("Spieler8(Clone)") == true)
        {
            isAnima = false;
        }
        else
        {
            animator = GameObject.FindWithTag("PlayerKörper").GetComponent<Animator>();
        }
        gun = GameObject.FindWithTag("Gun").GetComponent<GunInterface>();
        speed = start_Speed;
        weaponName = gun.GetGun();
    }

    public void FixedUpdate()
    {
        if(variableJoystick != null && lookJoystick != null)
        {
            animator.SetFloat("y", variableJoystick.Vertical);
            animator.SetFloat("x", variableJoystick.Horizontal);

            if (variableJoystick.Horizontal >= 0.1 || variableJoystick.Horizontal <= -0.1 || variableJoystick.Vertical >= 0.1 || variableJoystick.Vertical <= -0.1)
            {
                if (isAnima)
                {
                    animator.SetBool("isWalking", true);
                }
                Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
                rb.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
            }

            else if (isAnima)
            {
                animator.SetBool("isWalking", false);
            }




            switch (weaponName)
            {
                case "Lock":
                    Lock();
                    break;
                case "Blitz":
                    GunNotify();
                    break;
                case "flame":
                    Lock();
                    break;
                default:
                    GunNotify();
                    break;


            }

            Vector3 moveVector = (Vector3.right * lookJoystick.Horizontal + Vector3.forward * lookJoystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                lookTo = Quaternion.LookRotation(moveVector);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookTo, Time.deltaTime * lookSpeed);
            }
        }   
    }

    public void FlitzF3()
    {
        rb.AddForce(transform.forward * 20000);
        Debug.Log("null");
    }

    public void OnIce(float time)
    {
         StartCoroutine(EnumOnIce(time));
    }

    private IEnumerator EnumOnIce(float time)
    {
        iceTimer += time;
        if (isFrocen == false)
        {
            isFrocen = true;
            GameObject iceEffect = (GameObject)Instantiate(PS_IceEffect, Spawn_PS);
            float currentSpeed = speed;
            for (int i = 0; i < iceTimer; i++)
            {
                speed = 0;
                yield return new WaitForSeconds(1f);
            }
            speed = currentSpeed;
            Destroy(iceEffect);
            isFrocen = false;
            iceTimer = 0;
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

    void GunNotify()
    {
        if (lookJoystick.Horizontal >= 0.1 || lookJoystick.Horizontal <= -0.1 || lookJoystick.Vertical >= 0.1 || lookJoystick.Vertical <= -0.1)
        {
            canShoot = true;
            gun.NotifyShowLine();
        }
        if (lookJoystick.Horizontal == 0 || lookJoystick.Vertical == 0)
        {
            if (canShoot == true)
            {
                gun.NotifyHideLine();
            }
            canShoot = false;
        }
    }
}