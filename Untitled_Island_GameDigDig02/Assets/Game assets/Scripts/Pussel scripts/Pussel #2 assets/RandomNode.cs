using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : MonoBehaviour
{
    public Transform[] nodes;
    public float speed = 5f;
    public Transform targetNode;
    void Start()
    {

        ChooseRandomNode();

    }

  
    void Update()
    {
        
        if(targetNode != null)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetNode.position, speed * Time.deltaTime);



            if (Vector3.Distance(transform.position, targetNode.position) < 0.1f)
            {

                ChooseRandomNode();

            }

        }

      

    }

    void ChooseRandomNode()
    {

        targetNode = nodes[Random.Range(0, nodes.Length)];

    }


}


