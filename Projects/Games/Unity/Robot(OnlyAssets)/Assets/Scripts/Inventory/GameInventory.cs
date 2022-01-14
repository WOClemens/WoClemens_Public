using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInventory : MonoBehaviour
{
    public Animator GameInvent;

    public bool isShown = false;
    public bool isBuildAble = true;

    public int towerSlot1;
    public int towerSlot2;
    public int towerSlot3;
    public int towerSlot4;

    public GameObject buildPrefab;
    public GameObject invent;

    public Button towerSlotOne;
    public Button towerSlotTwo;
    public Button towerSlotThree;
    public Button towerSlotFour;

    public GameObject[] towers;

    public Transform SpawnPoint;

    public Material green;
    public Material red;

    [Header("Points")]
    public int towerPoints;
    public Slider slider;
    public Text Points;

    GameInventory points;
    Towers towersMain;
    FaehigkeitenIngame faehigkeiten;

    void Start()
    {
        points = GameObject.Find("TurmUnsichtbar").GetComponent<GameInventory>();
        faehigkeiten = GameObject.Find("Faehigkeiten").GetComponent<FaehigkeitenIngame>();
        //invent.SetActive(false);
        buildPrefab.SetActive(false);
        isShown = false;
        isBuildAble = true;
        towerSlot1 = PlayerPrefs.GetInt("TowerInvent1");
        towerSlot2 = PlayerPrefs.GetInt("TowerInvent2");
        towerSlot3 = PlayerPrefs.GetInt("TowerInvent3");
        towerSlot4 = PlayerPrefs.GetInt("TowerInvent4");
    }

    public void ShowBuild()
    {
        if (isShown == true)
        {
            GameInvent.SetTrigger("Off");
            buildPrefab.SetActive(false);
            isShown = false;
        }

        else
        {
            GameInvent.SetTrigger("On");
            buildPrefab.SetActive(true);
            buildPrefab.GetComponent<MeshRenderer>().material = green;
            isShown = true;
        }
    }

    void Update()
    {
        Points.text = towerPoints.ToString();

        towerSlot1 = PlayerPrefs.GetInt("TowerInvent1");
        towerSlot2 = PlayerPrefs.GetInt("TowerInvent2");
        towerSlot3 = PlayerPrefs.GetInt("TowerInvent3");
        towerSlot4 = PlayerPrefs.GetInt("TowerInvent4");

        slider.value = towerPoints;
    }

    public void InventSlotOne ()
    {
        Build(towerSlot1);
    }

    public void InventSlotTwo()
    {
        Build(towerSlot2);
    }

    public void InventSlotThree()
    {
        Build(towerSlot3);
    }

    public void InventSlotFour()
    {
        Build(towerSlot4);                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Umgebung")
        {
            isBuildAble = false;
            buildPrefab.GetComponent<MeshRenderer>().material = red;
        }

        else
        {
            buildPrefab.GetComponent<MeshRenderer>().material = green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Umgebung")
        {
            isBuildAble = true;
            buildPrefab.GetComponent<MeshRenderer>().material = green;
        }
    }

    void FindOut(int towerSlot) //schaut ob turm legänder oder feuer .. ist
    {
        Towers tower = towers[towerSlot].GetComponent<Towers>();
        faehigkeiten.HealPlus();
        if(tower.art == "fire")
        {
            faehigkeiten.FirePlus();
        }
        else if (tower.art == "ice")
        {
            faehigkeiten.IcePlus();
        }
        else if(tower.seltenheit == "legendär")
        {
            faehigkeiten.TitanPlus();
        }
        else if(tower.seltenheit == "normal")
        {
            faehigkeiten.PointsPlus();
        }
    }

    public void AddPoints(int ammount)
    {
        towerPoints += ammount;
        if(towerPoints > 100)
        {
            towerPoints = 100;
        }
    }

    void Build (int towerSlot)
    {
        towersMain = towers[towerSlot].GetComponent<Towers>();
        if (isShown == true && isBuildAble == true)
        {
            for (int i = 0; i < towers.Length; i++)
            {
                if (i == towerSlot)
                {
                    if (points.towerPoints >= towersMain.kosten)
                    {
                        Instantiate(towers[i], SpawnPoint.position, Quaternion.identity);
                        points.towerPoints -= towersMain.kosten;
                        FindOut(towerSlot);
                    }
                }
            }
        }
    }
}
