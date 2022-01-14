using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuildCamera : MonoBehaviour
{
    private Vector3 touchStart;
    public Camera cam;
    public float groundZ = 0;
    public int maxPosFront;
    public int maxPosEnd;

    bool isFirstTouch = true;
    bool isBuildTouch = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Debug.Log(Input.GetTouch(0).position.x);
            if(Input.GetTouch(0).position.x > 2500 && isFirstTouch == true)
            {
                isBuildTouch = true;
            }

            if(isFirstTouch == true)
            {
                isFirstTouch = false;
                touchStart= cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else if(isBuildTouch == false)
            {
                Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                direction.x = 0;
                cam.transform.position += direction;
            }
        }
        else
        {
            isBuildTouch = false;
            isFirstTouch = true;
        }

        if (cam.transform.position.z > maxPosFront + 1)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, maxPosFront);
        }

        if (cam.transform.position.z < maxPosEnd - 1)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, maxPosEnd);
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            direction.x = 0;
            cam.transform.position += direction;
        }*/
    }
}
