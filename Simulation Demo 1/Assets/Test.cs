using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject des;
    public GameObject start;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.Warp(start.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
       
        // transform.position=start.transform.position;//wrong
       
        // agent.isStopped = false;
       // agent.Move(transform.forward * Time.deltaTime);
        agent.SetDestination(des.transform.position);

       

    }
}
