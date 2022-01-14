using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Joystick joystick;
    public bool isIn = false;

    [Header("Effects")]
    public GameObject PS_IceEffect;
    public Transform Spawn_PS;

    bool isFrocen = false;
    float iceTimer;


    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            //transform.rotation = Quaternion.LookRotation(moveVector); //Nur testweise
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isIn = true;
    }

    public void OnIce(float time)
    {
        if(isFrocen == false)
        {
            isFrocen = true;
            StartCoroutine(EnumOnIce(time));
        }
    }

    private IEnumerator EnumOnIce(float time)
    {
        iceTimer += time;
        if(isFrocen == false)
        {
            isFrocen = true;
            GameObject iceEffect = (GameObject)Instantiate(PS_IceEffect, Spawn_PS);
            float currentSpeed = moveSpeed;
            for (int i = 0; i < iceTimer; i++)
            {
                moveSpeed = 0;
                yield return new WaitForSeconds(1f);
            }
            moveSpeed = currentSpeed;
            Destroy(iceEffect);
            isFrocen = false;
            iceTimer = 0;
        }
    }
}
