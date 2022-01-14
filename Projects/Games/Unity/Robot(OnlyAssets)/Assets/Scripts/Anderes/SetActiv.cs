using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiv : MonoBehaviour
{
    string sceneName = " ";
    void Start()
    {

    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName != "StartMenu")
        {
            gameObject.SetActive(false);
        }
    }
}
