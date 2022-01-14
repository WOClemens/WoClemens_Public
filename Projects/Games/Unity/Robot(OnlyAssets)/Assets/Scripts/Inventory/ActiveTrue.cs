using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveTrue : MonoBehaviour
{
    string sceneName = " ";
    public bool sceneChange = false;

    public GameObject JoyStick;
    public GameObject LockJoyStich;
    public GameObject TowerInvent;

    void Start()
    {
        JoyStick.SetActive(false);
        LockJoyStich.SetActive(false);
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName != "StartMenu")
        {
            TowerInvent.SetActive(true);
            JoyStick.SetActive(true);
            LockJoyStich.SetActive(true);
            sceneChange = true;
        }

        if (sceneName == "StartMenu" && sceneChange == true)
        {
            Destroy(this.gameObject);
        }
    }

    
}
