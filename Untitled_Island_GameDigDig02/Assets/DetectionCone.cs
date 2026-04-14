using UnityEngine;

public class DetectionCone : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject screenCoverPrefab;
    public Transform canvas;
    public GameObject player;

    void Start()
    {
        respawnPoint = GameObject.Find("RespawnPoint").transform;
        player = GameObject.Find("PlayableCharacter"); // make sure tag matches too
        canvas = GameObject.Find("Canvas").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Spawn the death screen UI
            GameObject screen = Instantiate(screenCoverPrefab, canvas);

            // TELEPORT player safely
            CharacterController cc = player.GetComponent<CharacterController>();
            Rigidbody rb = player.GetComponent<Rigidbody>();

            if (cc != null)
            {
                // Disable controller before moving
                cc.enabled = false;
                player.transform.position = respawnPoint.position;
                cc.enabled = true;
            }
            else if (rb != null)
            {
                // Reset velocity and move rigidbody
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.position = respawnPoint.position;
            }
            else
            {
                // Just move the transform
                player.transform.position = respawnPoint.position;
            }

            // Destroy UI after 1 second (or change to 5s if you want)
            Destroy(screen, 1f);
        }
    }
}