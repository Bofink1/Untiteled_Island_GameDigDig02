using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        transform.forward = dir;

        transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90, 0);
    }



}
