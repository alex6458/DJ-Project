using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnDistance = 1.0f; // Distance ahead of the enemy to spawn the bullet
    public string targetTag = "Enemy"; // Tag of the target object
    public float attackSpeed = 2.0f; // Time interval between attacks in seconds
    private float lastAttackTime = 0f; // Time when the last attack occurred
    private bool collisionStay = false;

    void Update()
    {

        if (collisionStay)
        {
            // Check attack cooldown
            if (Time.time >= lastAttackTime + attackSpeed)
            {
                // If cooldown is over, perform an attack
                PerformAttack();
            }
        }
    }


    void OnCollisionExit(Collision collision)
    {
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
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Enemy detected!");
            collisionStay = true;
        }
    }
}
