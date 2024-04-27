using UnityEngine;

public class RangedBehaviour : MonoBehaviour
{
    public string targetTag = "Base"; // Tag of the target object
    public string targetTag2 = "Friendly"; // Tag of the target object
    private string attackTag = "";
    public float moveSpeed = 3f; // Speed at which the enemy moves
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnDistance = 1.0f; // Distance ahead of the enemy to spawn the bullet
    private Transform target; // Reference to the target's transform
    private bool isMoving = true; // Flag to control enemy movement
    public float attackSpeed = 2.0f; // Time interval between attacks in seconds
    private float lastAttackTime = 0f; // Time when the last attack occurred
    private bool collisionStay = false;


    void Start()
    {
        // Find the GameObject with the specified tag
        GameObject targetObject2 = GameObject.FindGameObjectWithTag(targetTag2);
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        // Get the transform component of the target GameObject
        if (targetObject != null)
        {
            target = targetObject.transform;
            attackTag = targetTag;
        }
        else
        {
            if (targetObject2 != null)
            {
                target = targetObject2.transform;
                attackTag = targetTag2;
            }
            else
            {
                Debug.LogError("Target with tag '" + targetTag + "' not found!");
            }
        }
    }


    void Update()
    {

        if(collisionStay)
        {
            // Check attack cooldown
            if (Time.time >= lastAttackTime + attackSpeed)
            {
                // If cooldown is over, perform an attack
                PerformAttack();
            }
        }

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


    void OnCollisionExit(Collision collision)
    {
        isMoving = true;
        collisionStay = false;
    }


    void PerformAttack()
    {
        // Calculate spawn position slightly ahead of the enemy
        Vector3 spawnPosition = transform.position + transform.forward * bulletSpawnDistance;

        // Spawn a bullet at the calculated position
        if (bulletPrefab != null)
        {
            Instantiate(bulletPrefab, spawnPosition, transform.rotation, transform.parent);
        }

        // Update the last attack time to the current time
        lastAttackTime = Time.time;
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected!");
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(attackTag))
        {
            isMoving = false;
            collisionStay = true;
        }
    }
}
