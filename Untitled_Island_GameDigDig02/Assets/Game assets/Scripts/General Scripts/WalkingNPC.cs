using UnityEngine;

public class WalkingNPC : MonoBehaviour
{
    public float MovementSpeed = 1f;
    float speed = 1f;
    Vector3 dir;
    float timer;
    bool allowedToWalk = true;
    bool returningToCenter = false;
    bool playerNearby = false;
    public Rigidbody rb;
    public Transform player;

    Transform centerPoint;
    public string centerPointName = "Enter Centerpoint"; // name of your empty GameObject
    public float returnDuration = 2f;
   


    void Start()
    {
        // Find the center point by name
        GameObject centerObj = GameObject.Find(centerPointName);
     
        if (centerObj != null)
        {
            centerPoint = centerObj.transform;
        }
        else
        {
            Debug.LogError("Center point not found: " + centerPointName);
        }

        PickRandomDirection();
    }

    void Update()
    {
       /* if (playerNearby)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0;
            transform.forward = directionToPlayer.normalized;
            

            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            return;
        }*/

        timer += Time.deltaTime;
        if (returningToCenter && centerPoint != null)
        {
            // Move toward the center
            Vector3 directionToCenter = (centerPoint.position - transform.position).normalized;
            transform.forward = directionToCenter;
            rb.velocity = new Vector3(directionToCenter.x * speed, rb.velocity.y, directionToCenter.z * speed);

            if (timer >= returnDuration)
            {
                returningToCenter = false;
                allowedToWalk = true;
                timer = 0f;
                PickRandomDirection();
            }
        }
        else if (allowedToWalk)
        {
            rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.z * speed);

            if (timer >= 2f)
            {
                timer = 0f;
                allowedToWalk = false;
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            if (timer >= 5f)
            {
               
                timer = 0f;
                allowedToWalk = true;
                PickRandomDirection();
            }
        }

     
    }

    void PickRandomDirection()
    {
        dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        transform.forward = dir;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            returningToCenter = true;
            timer = 0f;
            allowedToWalk = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {

            playerNearby = false;
            speed = MovementSpeed;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNearby = true;
            speed = 0f;

        }
    }


}