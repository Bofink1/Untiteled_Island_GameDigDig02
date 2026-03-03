using UnityEngine;

public class WalkingNPC : MonoBehaviour
{
    float speed = 1f;
    Vector3 dir;
    float timer;
    bool allowedToWalk = true;
    bool returningToCenter = false;

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
        timer += Time.deltaTime;

        if (returningToCenter && centerPoint != null)
        {
            // Move toward the center
            Vector3 directionToCenter = (centerPoint.position - transform.position).normalized;
            transform.forward = directionToCenter;
            transform.position += directionToCenter * speed * Time.deltaTime;

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
            transform.position += transform.forward * speed * Time.deltaTime;

            if (timer >= 2f)
            {
                timer = 0f;
                allowedToWalk = false;
            }
        }
        else
        {
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
    }
}