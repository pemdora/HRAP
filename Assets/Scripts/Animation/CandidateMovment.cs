using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CandidateMovment : MonoBehaviour {
    private NavMeshAgent agent;

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
