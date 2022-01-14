using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public float shootspeed;
    public int count = 0;
    public Text countText;

    private float ScreenWidth;
    private float ScreenHeight;
    void Start()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = count.ToString();
        int i = 0;
        while (i < Input.touchCount)
        {
            count++;
        }
    }

    public void CountPlus()
    {
        count++;
    }
}
