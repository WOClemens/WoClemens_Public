using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Damag")]
    public int damag;

    private Transform target;
    public int wavepointIndex = 0;

    private Enemy enemy;
    public int rotSpeed = 6;

    PlayerLive playerLive;
    OverManager _overManager;

    bool isPathEnd = false;

    Survive survive;
    Train train;
    Scene scene;

    public bool isTrain = false;
    public int spawn = -1;

    [Header("Effects")]
    public GameObject PS_IceEffect;
    public Transform Spawn_PS;

    [Header("Train")]
    public bool _isTrainStart;

    bool isFrocen = false;


    void Start()
    {
        scene = SceneManager.GetActiveScene();
        playerLive = GameObject.FindWithTag("Player").GetComponent<PlayerLive>();
        _overManager = GameObject.Find("OverManager(Clone)").GetComponent<OverManager>();
        enemy = GetComponent<Enemy>();

        //wavepointIndex = spawn;

        if(_overManager.mode == "Path")
        {
            survive = GameObject.Find("WaveManager").GetComponent<Survive>();
        }

        if (_overManager.mode == "Train" && isTrain == true)
        {
            survive = GameObject.Find("WaveManager").GetComponent<Survive>();
            spawn = survive.spawn;
            target = Waypoints.points[0];
        }

        if((_overManager._arena == 3 || _overManager._arena == 1) && _overManager.mode == "Path") // Winter oder Lava
        {
            if(spawn == 0)
            {
                wavepointIndex = 3;
                target = Waypoints.points[wavepointIndex];
            }
            else
            {
                wavepointIndex = 0;
                target = Waypoints.points[wavepointIndex];
            }
        }

        else if(_overManager.mode == "Path")
        {
            wavepointIndex = spawn;
            target = Waypoints.points[wavepointIndex];
        }
    }

    public void Slow(float pct)
    {
        enemy.speed = enemy.startSpeed * (1f - pct);
    }

    void Update()
    {
        if ((isTrain == true && _overManager.mode != "Defend" && _isTrainStart == true) || _overManager.mode == "Path")
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
            enemy.speed = enemy.startSpeed;

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                if(target.tag == "CheckPoint" && isTrain == true)
                {
                    CheckPoint();
                }
                GetNextWaypoint();
            }
        
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    void GetNextWaypoint()
    {
        if(isTrain == true)
        {
            if (wavepointIndex >= Waypoints.points.Length - 1)
            {
                EndPath();
                return;
            }
            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
            return;
        }

        if (_overManager._arena == 3 && _overManager.mode == "Path") // lava und Path
        {
            if (wavepointIndex == 6)
            {
                if (spawn == 1)
                {
                    wavepointIndex = 7;
                }
            }

            if (wavepointIndex == 2)
            {
                wavepointIndex = 3;
            }

            if (spawn == 0 && wavepointIndex == Waypoints.points.Length - 2)
            {
                EndPath();
                return;
            }

            if (spawn == 1 && wavepointIndex == Waypoints.points.Length - 1)
            {
                EndPath();
                return;
            }
        }

        else if (_overManager._arena == 1 && _overManager.mode == "Path") // Winter und Path
        {
            if (wavepointIndex == 4)
            {
                if (spawn == 0)
                {
                    wavepointIndex++;
                }
            }

            if (spawn == 0 && wavepointIndex == Waypoints.points.Length - 1)
            {
                EndPath();
                return;
            }

            if (spawn == 1 && wavepointIndex == Waypoints.points.Length - 2)
            {
                EndPath();
                return;
            }
        }

        else if(_overManager._arena == 0 && _overManager.mode == "Path") // Wüste und Path
        {
            if (spawn == 1)
            {
                if(wavepointIndex > 3)
                {
                    EndPath();
                    return;
                }

                if(wavepointIndex == 3)
                {
                    wavepointIndex = Waypoints.points.Length - 2;
                }            
            }
            else
            {
                if (wavepointIndex == 0)
                {
                    wavepointIndex++;
                }
            }

            if (spawn == 0 && wavepointIndex == Waypoints.points.Length - 2)
            {
                EndPath();
                return;
            }
        }

        else
        {
            if (wavepointIndex >= Waypoints.points.Length - 1)
            {
                EndPath();
                return;
            }

            if (wavepointIndex == 0)
            {
                wavepointIndex++;
            }

            if (wavepointIndex == 3 && _overManager._arena == 2)// Dschungel
            {
                if (spawn == 1)
                {
                    wavepointIndex = Waypoints.points.Length - 2;
                }
            }

            if (spawn == 0 && wavepointIndex == Waypoints.points.Length - 2)
            {
                EndPath();
                return;
            }
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void CheckPoint()
    {
        GameObject.FindWithTag("WaveManager").GetComponent<Survive>().TrainCheckPoint();
        enemy.speed = 0;

    }

    public void CheckPointOver()
    {
        enemy.speed = enemy.startSpeed;
    }

    void EndPath()
    {
        if(_overManager.mode == "Path")
        {
            playerLive.TakePathDamag();
            survive.enemysAlive--;
        }

        if(_overManager.mode == "Train")
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().GameEnds("win");
        }       
        Destroy(gameObject);
    }

    public void OnIce(float time)
    {
        if(isFrocen == false)
        {
            StartCoroutine(EnumOnIce(time));
        }
    }

    private IEnumerator EnumOnIce(float time)
    {
        isFrocen = true;
        GameObject iceEffect = (GameObject)Instantiate(PS_IceEffect, Spawn_PS);
        enemy.speed = 0;
        yield return new WaitForSeconds(time);
        enemy.speed = enemy.startSpeed;
        isFrocen = false;
        Destroy(iceEffect);
    }

}