using UnityEngine;

public class RangeAttackTower : MonoBehaviour
{
    public string targetTag = "Enemy"; // Tag of the target object
    public float moveSpeed = 3f; // Speed at which the enemy moves
    private bool collisionDetected = false;
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
           Debug.Log("Target with tag '" + targetTag + "' not found!");  
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
        else
        {
            // If target is no longer valid, destroy this enemy GameObject
            Destroy(gameObject);
        }
    }


    void OnCollisionExit(Collision collision)
    {
        collisionDetected = false;
    }


    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collision detected!");
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag) && collision.gameObject.transform == target && !collisionDetected)
        {
            collisionDetected = true;
            Health healthBar = collision.gameObject.GetComponent<Health>();
            if (healthBar != null)
            {
                healthBar.TakeDamage(1f);
            }
            // Enemy has collided with the target, stop moving
            isMoving = false;
            Destroy(gameObject);
        }
    }
}
