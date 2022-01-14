using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchShoot : MonoBehaviour
{
    public int count = 0;
    public int shoot = 0;

    private float ScreenWidth;
    private float ScreenHeight;
    void Start()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
    }

    void Update()
    {
        count = Input.touchCount;



        for (int i = 0; i < Input.touches.Length; i++)
        {

            if (Input.GetTouch(i).position.x > ScreenWidth / 2 && Input.GetTouch(i).position.y < ScreenHeight / 2)
            {
                shoot++;
            }

            else
            {
                shoot = 0;
            }

        }

        if (count == 0)
        {
            shoot = 0;
        }
    }
}
