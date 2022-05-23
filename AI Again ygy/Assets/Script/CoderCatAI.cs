using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoderCatAI : MonoBehaviour
{

    public Transform[] waypoints;
    int currentPatrolPointIndex;

    NavMeshAgent nav;

    public float currentWaitingTime;
    public float maxWaitingTime;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        currentPatrolPointIndex = -1;
        currentWaitingTime = 0;
        maxWaitingTime = 0;

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving()
    {

        if (nav.remainingDistance < 0.5f)
        {
            if (maxWaitingTime == 0)
            {
                maxWaitingTime = Random.Range(3, 8);
            }
            if (currentWaitingTime > -maxWaitingTime)
            {
                maxWaitingTime = 0;
                currentWaitingTime = 0;
                GoToNextPoint();
            }
            else
            {
                currentWaitingTime += Time.deltaTime;
            }
        }
    }

    void GoToNextPoint()
    {
        if (waypoints.Length != 0)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % waypoints.Length;
            nav.SetDestination(waypoints[currentPatrolPointIndex].position);
        }
    }
}
