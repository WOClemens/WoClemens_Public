using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Joystick joystick;
    public float speedHori = 100f;
    public float speedVerti = 100f;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();   
    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(joystick.Horizontal * speedHori, rigidbody.velocity.y * joystick.Vertical * speedVerti);
    }
}
