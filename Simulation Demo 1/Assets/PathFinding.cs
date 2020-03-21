using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinding : MonoBehaviour
{
    public Transform[] points;//points you will use to move
    private NavMeshAgent car;
    private int destPoint;

    // Start is called before the first frame update
    void Start()
    {
       car = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!car.pathPending && car.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        car.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;//goes through all of them
    }
}
