using UnityEngine;

public class RangeEnemyBehaviour : MonoBehaviour
{
    public string targetTag = "Base"; // Tag of the target object
    public float moveSpeed = 3f; // Speed at which the enemy moves
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnDistance = 1.0f; // Distance ahead of the enemy to spawn the bullet
    private Transform target; // Reference to the target's transform
    private bool isMoving = true; // Flag to control enemy movement

    void Start()
    {
        // Find the GameObject with the specified tag
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        // Get the transform component of the target GameObject
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target with tag '" + targetTag + "' not found!");
        }
    }

    void Update()
    {
        // Check if we have a valid target and we are allowed to move
        if (target != null && isMoving)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Normalize the direction vector (make its length 1)
            direction.Normalize();

            // Move towards the target
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collision detected!");
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        { 
            // Calculate spawn position slightly ahead of the enemy
            Vector3 spawnPosition = transform.position + transform.forward * bulletSpawnDistance;

            // Spawn a bullet at the calculated position
            if (bulletPrefab != null)
            {
                Instantiate(bulletPrefab, transform.position, transform.rotation, transform.parent);
            }
            isMoving = false;
        }
    }
}
