using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using HRAP;

[RequireComponent(typeof(CandidateMovment))]
public class CandidateController : MonoBehaviour
{
    public LayerMask movmentMask;  // Create a layer "Ground" where only ground object are affected
    private CandidateMovment motor; // Script for candidate movment
    private Animator animator;
    public Camera Candidatecamera; // Main camera for player controller
    private RaycastHit hit; // Raycast for mouse hit
    public bool canMove; // boolean that choose if the player can move or not

    public GameObject particle;
    public GameObject pointer;

    // Goal points variables
    public Transform goalPointsList;
    private Transform[] goalPoints;
    private int currentGP = 0;
    public Transform handshakingPoint;
    public Transform chair;

    public Transform eva;
    private Vector3 direction;
    public static CandidateController candidateControllerInstance;
    public bool animationTrigger;

    // Timing
    private float hideInterfaceTime = 0;
    private IEnumerator coroutine;

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
        IHMInterview.MaskAllNguiComponents(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Input.GetMouseButtonDown(0))
        {
            // Create a ray for position to click
            Ray ray = this.Candidatecamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 5f, movmentMask))
            {
                animator.SetBool("Walking", true);
                motor.MoveToPoint(hit.point);
                pointer.transform.position = hit.point;
            }
        }

        // if the candidate is close to hit point target (1f), then he stop "walking" animation
        if (canMove && animator.GetBool("Walking") && Vector3.Distance(hit.point, this.transform.position) <= 1.2f)
        {
            animator.SetBool("Walking", false);
        }
        // if the candidate is close to animation point target (0.75f), then he stop "walking" animation
        if (animator.GetBool("Walking") && Vector3.Distance(handshakingPoint.position, this.transform.position) <= 0.75f)
        {
            animator.SetBool("Walking", false);
            PlayAnimation(M_Animation.ANIM_GESTE_MAIN, 0, Vector3.one); // Vector is not importat for a simple animation
            handshakingPoint.position = Vector3.one*-1;
        }

        // if the candidate is close to reached target (0.75f), then he stop "walking" animation
        if ((canMove || animationTrigger) && Vector3.Distance(particle.transform.position, this.transform.position) <= 0.75f)
        {
            currentGP++;
            animator.SetBool("Walking", false);
            if (currentGP < goalPoints.Length) // if we a next waypoint in the list
            {
                particle.transform.position = goalPoints[currentGP].position;
                this.particle.SetActive(true);
            }
            if (currentGP == 1) // if we are at 1st waypoint
            {
                PlayAnimation(M_Animation.ANIM_MARCHE, 0f, handshakingPoint.position);
            }
            if (currentGP == 2) // if we are at 2d waypoint
            {
                this.particle.SetActive(false);
                animator.SetBool("Sitting", true);
                motor.enabled = false;
                this.transform.position = chair.position;
            }
            LookAt(eva.position);
            canMove = false;
            animationTrigger = false;
            DiplayCandidateInterface(hideInterfaceTime);
        }
        if (!animator.GetBool("Walking")&& !canMove)
        {
            LookAt(eva.position);
        }

    }

    // Display or Hide GUI Interface
    public void DiplayCandidateInterface(float hideTime)
    {
        hideInterfaceTime = hideTime;
        // If the candidate is not moving
        if (!canMove)
        {
            IHMInterview.MaskAllNguiComponents(false); // Hide components (MaskAllNguiComponents should actually be DisplayAllNguiComponents)
            coroutine = WaitAndDisplay(hideTime);
            StartCoroutine(coroutine);
        }
    }

    public void LookAt(Vector3 positionToLookAt)
    {
        this.direction = positionToLookAt - this.transform.position;
        this.direction.y = 0;

        if (this.direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction); // mathematical way to deal with rotation
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f); // will just aim in the (x,z) plan
        }
    }

    private IEnumerator WaitAndDisplay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        IHMInterview.MaskAllNguiComponents(true); // Display UI 
    }

    #region P_interview call
    // Play animation with a wait time from P_interview
    public void PlayAnimation(M_Animation animation, float waitingTime) // Display current camera
    {
        switch (animation)
        {
            case M_Animation.ANIM_MARCHE:
                animator.SetBool("HandShaking", false);
                coroutine = WaitAndPlay(waitingTime, "Walking", goalPoints[currentGP].position);
                StartCoroutine(coroutine);
                break;
        }
    }
    #endregion

    // Overloaded PlayAnimation method
    // Play animation with a wait time
    public void PlayAnimation(M_Animation animation, float waitingTime, Vector3 position) // Display current camera
    {
        switch (animation)
        {
            case M_Animation.ANIM_GESTE_MAIN:
                coroutine = WaitAndPlay(waitingTime, "HandShaking", position);
                StartCoroutine(coroutine);
                break;
            case M_Animation.ANIM_MARCHE:
                coroutine = WaitAndPlay(waitingTime, "Walking", position);
                StartCoroutine(coroutine);
                break;
        }
    }

    private IEnumerator WaitAndPlay(float waitTime, string animName, Vector3 position)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool(animName, true);
        if(position!=Vector3.one)
            motor.MoveToPoint(position);
        animationTrigger = true;
    }


}
