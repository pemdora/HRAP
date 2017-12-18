using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CandidateMovment))]
public class CandidateController : MonoBehaviour {

    public LayerMask movmentMask;
    private CandidateMovment motor;
    private Animator animator;
    public Camera camera;
    private RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        // Find a reference to the Animator component.
        animator = GetComponent<Animator>();
        motor = GetComponent<CandidateMovment>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction , out hit, 100, movmentMask))
            {
                animator.SetBool("isWalking", true);
                motor.MoveTopoint(hit.point);
            }
        }

        if (animator.GetBool("isWalking") && Vector3.Distance(hit.point, this.transform.position) <= 1.5f)
        {
            animator.SetBool("isWalking", false);
        }
    }
}
