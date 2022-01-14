using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject finishPage;

    public bool isEnd;

    void Start()
    {
        finishPage.SetActive(false);
    }

    void Update()
    {
        if (isEnd == true)
        {
            finishPage.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "End")
        {
            isEnd = true;
        }
    }
}
