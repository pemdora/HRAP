using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CandidateMovment : MonoBehaviour
{
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get { return agent; } }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    public void MoveTopoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    // Update is called once per frame
    void Update()
    {
    }

}
