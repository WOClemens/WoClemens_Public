using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch : MonoBehaviour
{
    public Text touch;
    public Text genauTouch;
    public int count = 0;
    int move;

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
        if(Input.touchCount > 0)
        {

        }

        for(int i = 0; i < Input.touches.Length; i++)
        {

            if (Input.GetTouch(i).position.x > ScreenWidth / 2 && Input.GetTouch(i).position.y < ScreenHeight / 2) 
            {
                move++;
            }

        }

        genauTouch.text = move.ToString();
        touch.text = count.ToString();
    }
}
