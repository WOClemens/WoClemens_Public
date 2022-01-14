using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public int number;
    int randomEnd = 2;

    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public GameObject enemy;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public WaveSpawner gameManager;

    private int waveIndex = 0;


    void Start()
    {

    }

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            //gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        //PlayerStats.Rounds++;
        number = Random.Range(1, randomEnd);
        Wave wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            number = Random.Range(1, randomEnd);

            if (number == 1)
            {
                SpawnEnemy(wave.enemy);
            }
            
            if (number == 2)
            {
                SpawnEnemyTwo(wave.enemyTwo);
            }

            if (number == 3)
            {
                SpawnEnemyThree(wave.enemyThree);
            }

            if (number == 4)
            {
                SpawnEnemyFour(wave.enemyFour);
            }

            if (number == 5)
            {
                SpawnEnemyFive(wave.enemyFive);
            }

            if (number == 6)
            {
                SpawnEnemySix(wave.enemySix);
            }

            if (number == 7)
            {
                SpawnEnemySeven(wave.enemySeven);
            }

            if (number == 8)
            {
                SpawnEnemyEight(wave.enemyEight);
            }

            if (number == 9)
            {
                SpawnEnemyNine(wave.enemyNine);
            }

            if (number == 10)
            {
                SpawnEnemyTen(wave.enemyTen);
            }


            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
        randomEnd++;

    }

    void SpawnEnemy(GameObject enemy)
    {

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyTwo(GameObject enemyTwo)
    {

        Instantiate(enemyTwo, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyThree(GameObject enemyThree)
    {

        Instantiate(enemyThree, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyFour(GameObject enemyFour)
    {

        Instantiate(enemyFour, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyFive(GameObject enemyFive)
    {

        Instantiate(enemyFive, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemySix(GameObject enemySix)
    {

        Instantiate(enemySix, spawnPoint.position, spawnPoint.rotation);
    }
    void SpawnEnemySeven(GameObject enemySeven)
    {

        Instantiate(enemySeven, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyEight(GameObject enemyEight)
    {

        Instantiate(enemyEight, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyNine(GameObject enemyNine)
    {

        Instantiate(enemyNine, spawnPoint.position, spawnPoint.rotation);
    }

    void SpawnEnemyTen(GameObject enemyTen)
    {

        Instantiate(enemyTen, spawnPoint.position, spawnPoint.rotation);
    }
}

