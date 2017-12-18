using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CandidateMovment))]
public class CandidateController : MonoBehaviour
{
    public LayerMask movmentMask;  // Create a layer "Ground" where only ground object are affected
    private CandidateMovment motor; // Script for candidate movment
    private Animator animator;
    public new Camera camera; // Main camera for player controller
    private RaycastHit hit; // Raycast for mouse hit
    private bool canMove; // boolean that choose if the player can move or not

    public GameObject particle;
    public GameObject pointer;

    // Use this for initialization
    void Start()
    {
        // Find a reference to the Animator component.
        animator = GetComponent<Animator>();
        motor = GetComponent<CandidateMovment>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove && Input.GetMouseButtonDown(0))
        {
            IHMInterview.MaskAllNguiComponents(false);
            Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100, movmentMask))
            {
                animator.SetBool("isWalking", true);
                motor.MoveTopoint(hit.point);
                pointer.transform.position = hit.point;
            }
        }

        // if the candidate is close to hit point target (1.5f), then he stop "walking" animation
        if (animator.GetBool("isWalking") && Vector3.Distance(hit.point, this.transform.position) <= 0.75f)
        {
            animator.SetBool("isWalking", false);
        }

        // if the candidate is close to reached target (1.5f), then he stop "walking" animation
        if (Vector3.Distance(particle.transform.position, this.transform.position) <= 1f)
        {
            this.particle.SetActive(false);
            IHMInterview.MaskAllNguiComponents(true);
            canMove = false;
        }

    }
}
