using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField]
    float throwforce = 600f;
    [SerializeField]
    float maxDistance = 3f;
    float distance;

    TempParent tempParent;
    Rigidbody rb;

    Vector3 objectPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;


    }

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            Hold();
        }
    }

    private void OnMouseOver()
    {
        //pickup
        if (tempParent != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isHolding)
            {
                distance = Vector3.Distance(this.transform.position, tempParent.transform.position);
                if (distance <= maxDistance)
                {
                    isHolding = true;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    rb.detectCollisions = true;
                    this.transform.SetParent(tempParent.transform);
                }
            }
        }
        else
        {
            Debug.Log("Tempparent finns inte i scenen");
        }
    }

    private void OnMouseUp()
    {
        Drop();
    }

    private void OnMouseExit()
    {
        Drop();
    }

    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, tempParent.transform.position);
        if(distance > maxDistance)
        {
            Drop();
        }
       
        if(Input.GetMouseButtonDown(1))
        {
            rb.AddForce(tempParent.transform.forward * throwforce);
            Drop();
        }
        
    }

    private void Drop()
    {
        if(isHolding)
        {
            isHolding = false;
            this.transform.SetParent(null);

            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
}
