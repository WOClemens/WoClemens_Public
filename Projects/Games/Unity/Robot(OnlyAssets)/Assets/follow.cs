using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class follow : MonoBehaviour
{
    Scene scene;

    private GameObject target;
    private NavMeshAgent nav;

    public float distanceToTarget;
    public float rangeToTarget;
    public bool isStop = false;
    float speed;

    private NavMeshAgent agent;
    private follow foll;

    void Start()
    {
        if (GameObject.Find("OverManager(Clone)").GetComponent<OverManager>().mode == "Path")//versteh nicht was target ist
        {
            target = GameObject.FindWithTag("Target");
            agent = GetComponent<NavMeshAgent>();
            agent.enabled = !agent.enabled;
            foll = GetComponent<follow>();
            foll.enabled = !foll.enabled;
        }

        else
        {
            FindTarget();
        }

        nav = GetComponent<NavMeshAgent>();
        speed = nav.speed;
    }

    void FindTarget()
    {
        if (GameObject.Find("OverManager(Clone)").GetComponent<OverManager>().mode == "Train")
        {
            target = GameObject.FindWithTag("Target");
        }

        else if (GameObject.Find("OverManager(Clone)").GetComponent<OverManager>().mode == "Defend")
        {
            target = GameObject.FindWithTag("Target");
        }
        else
        {
            target = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if(target == null)
        {
            FindTarget();
        }
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget < rangeToTarget)
        {
            nav.speed = 0;
            isStop = true;
        }

        else
        {
            nav.speed = speed;
            nav.SetDestination(target.transform.position);
        }
    }

    public void Stop()
    {
        nav.speed = 0;
    }

    public void Speed()
    {
        nav.speed = speed;
        nav.SetDestination(target.transform.position);
    }
}
