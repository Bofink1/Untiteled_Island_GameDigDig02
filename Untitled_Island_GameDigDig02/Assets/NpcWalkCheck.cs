using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    public Animator animator; 
    private Vector3 lastPosition;
    public float movementThreshold = 0.01f; 

    void Start()
    {
        lastPosition = transform.position;

       
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, lastPosition);

        if (distance > movementThreshold)
        {

            animator.SetFloat("speed", 1f);
        }
        else
        {

            animator.SetFloat("speed", 0f);
        }

        lastPosition = transform.position; 
    }
}