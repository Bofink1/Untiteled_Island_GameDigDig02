using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFollow : MonoBehaviour
{
    public Transform player;      // Player to follow
    public float speed = 5f;     
    public float rotationSpeed = 2f; // How fast the bird rotates to player
    public float distanceOffset = 2f; // Keeps the distance from player 

    void Update()
    {
        if (player == null) return; // Safety check

        // Calculate direction to player
        Vector3 direction = player.position - transform.position;

        // Move towards player 
        Vector3 move = direction.normalized * speed * Time.deltaTime;
        if (direction.magnitude > distanceOffset) // Keeps distance
        {
            transform.position += move;
        }

        // Rotate to player
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}






