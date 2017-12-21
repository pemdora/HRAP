using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CandidateMovment))]
public class CandidateController : MonoBehaviour
{
    public LayerMask movmentMask;  // Create a layer "Ground" where only ground object are affected
    private CandidateMovment motor; // Script for candidate movment
    private Animator animator;
    public new Camera camera; // Main camera for player controller
    private RaycastHit hit; // Raycast for mouse hit
    public bool canMove; // boolean that choose if the player can move or not

    public GameObject particle;
    public GameObject pointer;

    // Goal points variables
    public Transform goalPointsList;
    private Transform[] goalPoints;
    public int currentGP = 0;

    public static CandidateController candidateControllerInstance;

    //SINGLETON
    void Awake()
    {
        if (candidateControllerInstance != null)
        {
            Debug.LogError("More than one Candidate Controller in scene");
            return;
        }
        else
        {
            candidateControllerInstance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        // Find a reference to the Animator component.
        animator = GetComponent<Animator>();
        motor = GetComponent<CandidateMovment>();
        // Get child elements from GoalPoints object
        goalPoints = new Transform[goalPointsList.childCount];
        for (int i = 0; i < goalPoints.Length; i++)
        {
            goalPoints[i] = goalPointsList.GetChild(i);
        }
        particle.transform.position = goalPoints[0].position;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove && Input.GetMouseButtonDown(0))
        {
            IHMInterview.MaskAllNguiComponents(false);
            // Create a ray for position to click
            Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 5f, movmentMask))
            {
                animator.SetBool("isWalking", true);
                motor.MoveTopoint(hit.point);
                pointer.transform.position = hit.point;
            }
            /*
            NavMeshPath path = new NavMeshPath();
            motor.Agent.CalculatePath(hit.point, path);
            NavMeshHit hithit;
            if (!NavMesh.SamplePosition(hit.point, out hithit, 0.01f, NavMesh.GetAreaFromName("Walkable")))
            {
                Debug.Log("ok");
            }
            */
        }

        // if the candidate is close to hit point target (1.5f), then he stop "walking" animation
        if (animator.GetBool("isWalking") && Vector3.Distance(hit.point, this.transform.position) <= 1f)
        {
            animator.SetBool("isWalking", false);
        }

        // if the candidate is close to reached target (1.5f), then he stop "walking" animation
        if (Vector3.Distance(particle.transform.position, this.transform.position) <= 1f)
        {
            currentGP++;
            if(currentGP< goalPoints.Length) // if we a next waypoint in the list
            {
                particle.transform.position = goalPoints[currentGP].position;
            }
            this.particle.SetActive(false);
            canMove = false;
            IHMInterview.MaskAllNguiComponents(true);
        }

    }
}
