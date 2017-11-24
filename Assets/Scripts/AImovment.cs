using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AImovment : MonoBehaviour
{
    public Transform directionCandidate; // waypoint 1
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        // Find a reference to the Animator component in Awake since it exists in the scene.
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }
    
    public void MoveTopoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            MoveTopoint(directionCandidate.position);
        }
    }
    
}
