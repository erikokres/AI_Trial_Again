using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    public LayerMask playerMask;
    public float distance;
    public float range;
    int waypointindex;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < distance)
        {
            agent.SetDestination(player.position);

        }
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }

        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range, playerMask))
        { print("Attack Player"); }

    }

    void UpdateDestination()
    {
        int randomPointer = Random.Range(0, waypoints.Length);
        target = waypoints[randomPointer].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointindex++;
        if (waypointindex == waypoints.Length)
        {
            waypointindex = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
