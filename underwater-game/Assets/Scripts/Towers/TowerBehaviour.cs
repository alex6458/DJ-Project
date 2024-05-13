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
    private bool attacking = false; // Flag to indicate if currently attacking
    private Transform target; // Reference to the current target

    void Update()
    {
        if (attacking && target != null)
        {
            // Check attack cooldown
            if (Time.time >= lastAttackTime + attackSpeed)
            {
                PerformAttack();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Enemy detected!");
            target = collision.transform;
            attacking = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // If the target exits the collision, stop attacking
        if (collision.transform == target)
        {
            Debug.Log("Target lost!");
            attacking = false;
            target = null;
        }
    }
}
