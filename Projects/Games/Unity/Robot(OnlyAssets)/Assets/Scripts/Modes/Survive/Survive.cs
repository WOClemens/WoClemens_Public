using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Survive : MonoBehaviour
{
    public Animator WaveAnimatior;
    public Text WaveInt;
    public Text WaveCount;
    public Text EnemysAlive;
    public int spD;
    public int spT;
    public int spP;
    public int spS;
    int sp;

    [Header("Spawn Points")]
    public int waveAmmount = 5;

    [Header("Modi(macht nichts aus)")]
    public bool isTrain;
    public bool isPath;
    public bool isDefend;

    [Header("Spawn Points")]
    private GameObject[] _spawnPoints;
    public GameObject _spawnpointsWüste;
    public GameObject _spawnpointsWinter;
    public GameObject _spawnpointsDschungel;
    public GameObject _spawnpointsLava;

    [Header("Gegener")]
    public GameObject[] enemys;
    public GameObject[] enemys2;

    [Header("Welle")]
    public int waveCount = 0;

    [Header("Wartezeit zwischen Wellen")]
    public float waitBetweenWaves = 5f;

    [Header("Wartezeit zwischen Gegner")]
    public float waitBetweenEnemys = 1;

    [Header("Anzahl der Spawnenden Gegener")]
    public int enemysAreSpawning = 5;
    //int enemySpawnPoint = 0;
    float timer = 0f;

    [Header("Übrige Gegner")]
    public int enemysAlive = 0;
    bool isStart = false;
    bool isWaveEnd = false;
    public bool isGameStart = false;
    int enemyIsSpawning = 0;

    bool isWin = false;
    int halfEnemysSpawn;

    [Header("Start Page")]
    public GameObject StartPage;
    public GameObject InGamePage;
    public int spawn;

    public int allEnemys;
    string _mode;

    [Header("Train")]
    public float _halfTimer;
    GameManager _gameManager;
    bool isTrainCheckPoint;

    [Header("Building")]
    public bool isBuilding = false;
    bool isInBuildManager = false;
    BuildManager _buildManager;

    void Start()
    {
        InGamePage.SetActive(true);
        isBuilding = true;
        enemysAreSpawning = Random.Range(enemysAreSpawning, enemysAreSpawning + 3);
        isWin = false;
        StartPage.SetActive(true);     
        _mode = GameObject.FindWithTag("OverManager").GetComponent<OverManager>().mode;
        _gameManager = GameObject.Find("SceneLoader").GetComponent<GameManager>();
        _buildManager = GameObject.Find("Panel_SwitchMode").GetComponent<BuildManager>();

        switch (GameObject.Find("OverManager(Clone)").GetComponent<OverManager>()._arena)
        {
            case 0:
                Instantiate(_spawnpointsWüste, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(_spawnpointsWinter, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(_spawnpointsDschungel, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(_spawnpointsLava, new Vector3(0, 0, 0), Quaternion.identity);
                break;
        }
        switch (_mode)
        {
            case "Train":
                _spawnPoints = GameObject.FindGameObjectsWithTag("ST");
                System.Array.Sort(_spawnPoints, CompareObNames);
                break;
            case "Path":
                _spawnPoints = GameObject.FindGameObjectsWithTag("SP");
                break;
            case "Defend":
                _spawnPoints = GameObject.FindGameObjectsWithTag("SD");
                break;
            case "Survive":
                _spawnPoints = GameObject.FindGameObjectsWithTag("SS");
                break;
        }
    }

    public void Isstart()
    {
        isStart = true;
        isGameStart = true;
        StartPage.SetActive(false);
        InGamePage.SetActive(false);

        if (GameObject.FindWithTag("OverManager").GetComponent<OverManager>().mode == "Train")
        {
            GameObject.FindWithTag("Target").GetComponent<EnemyMovement>()._isTrainStart = true;
        }
        if (_mode == "Train")
        {
            StartCoroutine("TrainWaves");
        }
    }

    void Update()
    {
        EnemysAlive.text = allEnemys.ToString();
        WaveCount.text = waveCount.ToString();
        if (waveCount <= waveAmmount && _mode != "Train" && _gameManager.isEnd == false)
        {
            if (isStart == true)
            {     
                if(isInBuildManager == false)
                {
                    isInBuildManager = true;
                    _buildManager.BuildTime();
                }
                if (isBuilding == false)
                {
                    isInBuildManager = false;
                    isBuilding = true;
                    isWaveEnd = false;
                    isStart = false;
                    waveCount++;
                    if (waveCount <= waveAmmount)
                    {
                        StartCoroutine("Wave");
                    }
                }
            }
            if (enemysAlive <= 0 && isWaveEnd == true)
            {
                isStart = true;
            }
        }
        else if(_gameManager.isEnd == false && _mode != "Train")
        {
            _gameManager.GameEnds("win");
        }

        if (isGameStart == true)
        {          
            timer += Time.deltaTime;
        }
    }

    void Spawn()
    {
        int ending = _spawnPoints.Length;
        int spawning = Random.Range(0, ending);
        spawn = spawning;
        int array = Random.Range(0, 2);
        if(_mode == "Train")
        {
            spawning = Random.Range(0, 2);
            if (timer > _halfTimer)
            {
                spawning += 2;
            }
            
        }
        if(array == 0)
        {
            GameObject go = (GameObject)Instantiate(enemys2[enemyIsSpawning], _spawnPoints[spawning].transform.position, Quaternion.identity);
            go.GetComponent<EnemyMovement>().spawn = spawning;
            enemysAlive++;
        }

        else 
        {
            GameObject go = (GameObject)Instantiate(enemys[enemyIsSpawning], _spawnPoints[spawning].transform.position, Quaternion.identity);
            go.GetComponent<EnemyMovement>().spawn = spawning;
            enemysAlive++;
        }
    }

    IEnumerator Wave()
    {
        WaveInt.text = waveCount.ToString();
        WaveAnimatior.SetTrigger("ShowWave");
        yield return new WaitForSeconds(waitBetweenWaves);

        WaveAnimatior.SetTrigger("HideWave");

        double ets = enemysAreSpawning/2;
        halfEnemysSpawn = (int)ets;
        allEnemys = enemysAreSpawning + 1;

        for (int b = 0; b < enemysAreSpawning + 1; b++)
        {
            Spawn();
            yield return new WaitForSeconds(waitBetweenEnemys);
        }

        enemyIsSpawning++;
        if(enemyIsSpawning >= enemys.Length)
        {
            enemyIsSpawning--;
        }
        enemysAreSpawning = enemysAreSpawning + 3;
        yield return new WaitForSeconds(waitBetweenWaves);

        isWaveEnd = true;
    }

    IEnumerator TrainWaves() // Wird benutzt
    {
        isStart = false;
        while (_gameManager.isEnd == false)
        {
            for (int b = 0; b < 6 + 1; b++)
            {
                Spawn();
                yield return new WaitForSeconds(2);
            }
            while(isBuilding == true)
            {
                if(GameObject.FindWithTag("Enemy") == null)
                {
                    if (isInBuildManager == false)
                    {
                        _buildManager.BuildTime();
                        isInBuildManager = true;
                    }
                }
                yield return new WaitForSeconds(0.2f);
            }
            if(isInBuildManager == true)
            {
                GameObject.FindWithTag("Target").GetComponent<EnemyMovement>().CheckPointOver();
            }
            isInBuildManager = false;
            yield return new WaitForSeconds(3);
            enemyIsSpawning++;
            if (enemyIsSpawning >= enemys.Length)
            {
                enemyIsSpawning = enemys.Length;
            }
        }
    }

    public void TrainCheckPoint()
    {
        isBuilding = true;       
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

}
