using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject buildPrefab;
    public GameObject towerOne;
    bool isShown = false;
    public bool isBuildAble = true;

    void Start()
    {
        buildPrefab.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ShowBuild()
    {
        if (isShown == true)
        {
            buildPrefab.SetActive(false);
            isShown = false;
        }

        else
        {
            buildPrefab.SetActive(true);
            isShown = true;
        }
    }

    public void BuildTowerOne ()
    {

        if (isShown == true && isBuildAble == true)
        {
            Instantiate(towerOne, transform.position, Quaternion.identity);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            isBuildAble = false; 
        }
    }
}
