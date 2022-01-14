using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [Header("Train")]
    public GameObject train;
    public Transform trainSpawn;
    public bool isSpawn;
    Survive survive;
    EnemyMovement enemyMove;

    void Start()
    {
        isSpawn = false;
        survive = this.GetComponent<Survive>();
        enemyMove = train.GetComponent<EnemyMovement>();
        enemyMove.rotSpeed = 0;
    }

    void Update()
    {     
        if(survive.isGameStart == true && isSpawn == false)
        {
            enemyMove.rotSpeed = 6;
        }
    }

   
}
