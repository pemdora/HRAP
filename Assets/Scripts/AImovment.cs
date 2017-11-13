using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImovment : MonoBehaviour
{
    private float speed = 1f;
    private Vector3 direction;
    public Transform directionCandidate;
    private Animator animator;


    void Awake()
    {
        // Find a reference to the Animator component in Awake since it exists in the scene.
        animator = GetComponent<Animator>();
        
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

    public void Move()
    {
        this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            LookAt(directionCandidate.position);
            Move();
        }
    }
    
}
