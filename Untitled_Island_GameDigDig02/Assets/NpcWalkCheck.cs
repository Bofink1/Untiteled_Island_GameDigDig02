using UnityEngine;

public class MovementAnimator : MonoBehaviour
{
    public Animator animator;
    private Vector3 lastPosition;
    public float movementThreshold = 1f;

    void Start()
    {
        lastPosition = transform.position;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 movement = transform.position - lastPosition;
        float distance = movement.magnitude;

        if (distance > movementThreshold)
        {
            animator.SetBool("IsWalking", true);

            
            movement.y = 0;
            transform.forward = movement;
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        lastPosition = transform.position;
    }
}