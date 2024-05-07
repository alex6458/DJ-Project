using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public string targetTag = "Base"; // Tag of the target object
    public string targetTag2 = "Friendly"; // Tag of the target object
    private string attackTag = "";
    public float moveSpeed = 3f; // Speed at which the enemy moves
    private Transform target; // Reference to the target's transform
    private bool isMoving = true; // Flag to control enemy movement
    public float attackSpeed = 2.0f; // Time interval between attacks in seconds
    private float lastAttackTime = 0f; // Time when the last attack occurred
    private bool collisionStay = false; // If the collision is still going on
    Health healthBar = null;

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
        // check if the collision is still ongoing
        if (collisionStay)
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

    // if there is no more collision
    void OnCollisionExit2D(Collision2D collision)
    {
        isMoving = true;
        collisionStay = false;
    }

    //do the melee attack
    void PerformAttack()
    {

        if (healthBar != null)
        {
            healthBar.TakeDamage(1f);
        }

        // Update the last attack time to the current time
        lastAttackTime = Time.time;
    }

    // the collision started
    void OnCollisionEnter2D(Collision2D collision)
    {
        healthBar = collision.gameObject.GetComponent<Health>();

        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(attackTag))
        {
            isMoving = false;
            collisionStay = true;
        }
    }
}
