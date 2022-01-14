using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    NavMeshSurface surface;

    [Header("Ways")]
    public GameObject[] WayTrain;
    public GameObject[] WayPath;

    public bool Player;
    public bool PlayerAndTower;
    public bool Target;
    public bool Tower;
    Scene scene;

    public GameObject loadingScreen;
    public Slider slider;

    MoneyArena myMoneyArena;
    MoneyManager myMoneyManager;

    [Header("Arenas")]
    public int Wi;
    public int W;
    public int D;
    public int L;

    [Header("Lights")]
    public GameObject _light;

    [Header("Arenas")] // 0 = Wüste, 1 = Winter, 2 = Dschungel, 3 = Lava
    public GameObject[] _paths;

    [Header("Arenas Train")]
    public GameObject[] _train;

    [Header("Arenas Defend")]
    public GameObject[] _defend;

    [Header("Arenas Survive")]
    public GameObject[] _survive;

    Survive mySurvive;
    PlayerLive playerLive;

    int restlicheLeben;
    public Text ÜbrigeLeben;

    public bool isEnd = false;

    OverManager _overManager;
    public GameObject OManager;
    public GameObject ShowArena;
    public Sprite[] arenaImages;
    public Image loadSprite;
    public float spinSpeed = 0.5f;
    public GameObject text_error_noMoney;

    [Header("ResultPage")]
    public GameObject _resultPage;
    public Text text_Result;
    public Text text_Money;
    public Text text_Kills;
    public Text text_Time;
    float timer;
    bool isGameStart;

    void Start()
    {
        isGameStart = false;
        isEnd = false;
        scene = SceneManager.GetActiveScene();

        if (scene.name == "StartMenu")
        {
            if (GameObject.Find("OverManager(Clone)") == null)
            {
                Instantiate(OManager, new Vector3(0, 0, 0), Quaternion.identity);
            }
            myMoneyArena = GameObject.Find("MoneyManager").GetComponent<MoneyArena>();
            myMoneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        }


        else
        {
            _resultPage.SetActive(false);
            _overManager = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
            int arena = _overManager._arena;

            loadSprite.sprite = arenaImages[arena];

            switch (arena)
            {
                case 0:
                    Instantiate(_light, new Vector3(0, 0, 0), Quaternion.Euler(41,-30,0));
                    break;
                case 1:
                    Instantiate(_light, new Vector3(0, 0, 0), Quaternion.Euler(23, 34, 0));
                    break;
                case 2:
                    Instantiate(_light, new Vector3(0, 0, 0), Quaternion.Euler(30, -30, 0));
                    break;
                case 3:
                    Instantiate(_light, new Vector3(0, 0, 0), Quaternion.Euler(8, -30, 0));
                    break;
            }

            switch (_overManager.mode)
            {
                case "Path":
                    Instantiate(WayPath[arena], new Vector3(0, 0, 0), Quaternion.identity);
                    Instantiate(_paths[arena], new Vector3(0, 0, 0), Quaternion.identity);
                    playerLive = GameObject.FindWithTag("Player").GetComponent<PlayerLive>();
                    break;
                case "Train":
                    Instantiate(WayTrain[arena], new Vector3(0, 0, 0), Quaternion.identity);
                    Instantiate(_train[arena], new Vector3(0, 0, 0), Quaternion.identity);
                    playerLive = GameObject.FindWithTag("Target").GetComponent<PlayerLive>();
                    surface = GameObject.Find("BodenTrain").GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                    break;
                case "Defend":
                    Instantiate(_defend[arena], new Vector3(0, 0, 0), Quaternion.identity);
                    playerLive = GameObject.FindWithTag("Target").GetComponent<PlayerLive>();
                    surface = GameObject.Find("BodenDefend").GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                                   
                    break;
                case "Survive":                    
                    Instantiate(_survive[arena], new Vector3(0, 0, 0), Quaternion.identity);
                    playerLive = GameObject.FindWithTag("Player").GetComponent<PlayerLive>();
                    surface = GameObject.Find("BodenSurvive").GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                    
                    break;
            }
            mySurvive = GameObject.Find("WaveManager").GetComponent<Survive>();
        }
        _overManager = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
    }

    public void ArenaPath()
    {
        //LoadArena();
        ChooseArena();
        _overManager.mode = "Path";

    }

    public void ArenaTrain()
    {
        //LoadArena();
        ChooseArena();
        _overManager.mode = "Train";
    }

    public void ArenaDefend()
    {
        //LoadArena();
        ChooseArena();
        _overManager.mode = "Defend";
    }

    public void ArenaSurvive()
    {
        //LoadArena();
        ChooseArena();
        _overManager.mode = "Survive";
    }

    public void LoadTest()
    {
        StartCoroutine(LoadScene(1));
    }

    void ChooseArena()
    {
        int number = Random.Range(1, 5);
        loadSprite.sprite = arenaImages[number - 1];
        _overManager._arena = number - 1;
        LoadArena();// eingeben welche arena laden soll
    }

    void LoadArena() // Hir wird auch Quest 5 gesetzt
    {
        int playedGames = PlayerPrefs.GetInt("Q5");
        playedGames++;
        PlayerPrefs.SetInt("Q5", playedGames);

        myMoneyManager.money = myMoneyManager.money - myMoneyArena.arenaKosten[_overManager.difficulty - 1];
        _overManager.setMoney = myMoneyArena.arenaKosten[_overManager.difficulty - 1];
        StartCoroutine(LoadScene(1));    
    }

    IEnumerator LoadScene(int scene)
    {
        Debug.Log(" asdf");
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }


    }

    public void GameEnds(string result)
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            Destroy(enemys[i]);
        }

        isEnd = true;
        if (result == "win")
        {
            int winGames = PlayerPrefs.GetInt("Q6");
            winGames++;
            PlayerPrefs.SetInt("Q6", winGames);

            if(_overManager.difficulty == 1)
            {
                int winsInArena = PlayerPrefs.GetInt("Q3");
                winsInArena++;
                PlayerPrefs.SetInt("Q3", winsInArena);
            }

            PlayerPrefs.SetInt("WinOrLose", 1);
            ShowResult("Win");
        }
        else
        {
            PlayerPrefs.SetInt("WinOrLose", 0);
            ShowResult("Lose");
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            restlicheLeben = playerLive.pathLives;
            ÜbrigeLeben.text = restlicheLeben.ToString();
        }

        if (isGameStart == true)
        {
            timer += Time.deltaTime;
        }
    }

    void ShowResult(string result)
    {
        isGameStart = false;
        text_Result.text = result;
        if(result == "Win")
        {
            text_Money.text = "+ " + _overManager.setMoney.ToString();
        }
        else
        {
            text_Money.text = "- " + _overManager.setMoney.ToString();
        }
        text_Time.text = timer.ToString("F2");
        text_Kills.text = _overManager.Kill.ToString();
        _resultPage.SetActive(true);
    }

    public void End()
    {
        StartCoroutine(LoadScene(0));
    }

    public void StartMenu(int scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    public void Show_NoMoney_Error()
    {
        text_error_noMoney.GetComponent<Animator>().Play("ShowError_NoMoney");
    }

    public void StartTimer()
    {
        isGameStart = true;
    }


}