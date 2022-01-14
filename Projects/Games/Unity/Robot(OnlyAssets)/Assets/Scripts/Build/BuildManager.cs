using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject BuildCamera;
    public GameObject BuildPage;
    public GameObject InGamePage;
    public GameObject Player;
    public GameObject[] towers;
    public GameObject SwitchPage;
    GameObject[] _buildZone;

    [Header("Points")]
    public int _points;
    public Text text_points;

    [Header("Points")]
    public Animator transition;

    Survive _waveManager;

    void Start()
    {
        _buildZone = GameObject.FindGameObjectsWithTag("BuildZone");
        for(int i = 0; i < _buildZone.Length; i++)
        {
            _buildZone[i].SetActive(false);
        }
        SwitchPage.SetActive(true);
        text_points.text = _points.ToString();

        _waveManager = GameObject.Find("WaveManager").GetComponent<Survive>();
    }

    public void BuildTime()
    {
        text_points.text = _points.ToString();
        transition.SetBool("isBuildMode", true);
    }

    public void ShowBuildMode()
    {
        transition.SetBool("isBuildMode", false);
        BuildCamera.SetActive(true);
        BuildPage.SetActive(true);
        InGamePage.SetActive(false);
        Player.SetActive(false);
        for (int i = 0; i < _buildZone.Length; i++)
        {
            _buildZone[i].SetActive(true);
        }
    }

    public void EndBuildTime()
    {
        transition.SetBool("isFightMode", true);
    }

    public void ShowFightMode()
    {
        transition.SetBool("isFightMode", false);
        BuildCamera.SetActive(false);
        BuildPage.SetActive(false);
        InGamePage.SetActive(true);
        Player.SetActive(true);
        _waveManager.isBuilding = false;
        for (int i = 0; i < _buildZone.Length; i++)
        {
            _buildZone[i].SetActive(false);
        }
    }

    public void Build(Vector3 SpawnPoint, int towerSlot)
    {
        if (_points >= towers[towerSlot].GetComponent<Towers>().kosten)
        {
            _points -= towers[towerSlot].GetComponent<Towers>().kosten;
            Debug.Log("Build");
            for (int i = 0; i < towers.Length; i++)
            {
                if (i == towerSlot)
                {
                    Instantiate(towers[i], SpawnPoint, Quaternion.identity);
                }
            }
        }
        text_points.text = _points.ToString();
    }

    public void BuildError(int kost)
    {
        _points += kost;
        text_points.text = _points.ToString();
    }

    public void AddPoints(int ammount)
    {
        _points += ammount;
    }
}

