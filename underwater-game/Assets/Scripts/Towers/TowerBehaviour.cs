using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnDistance = 1.0f; // Distance ahead of the enemy to spawn the bullet
    public string targetTag = "Enemy"; // Tag of the target object

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collision detected!");
        Debug.Log(collision.gameObject.tag);
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(targetTag))
        {

            // Calculate spawn position slightly ahead of the enemy
            Vector3 spawnPosition = transform.position + transform.forward * bulletSpawnDistance;

            // Spawn a bullet at the calculated position
            if (bulletPrefab != null)
            {
                Debug.Log("SHOOTING");
                Instantiate(bulletPrefab, transform.position, transform.rotation, transform.parent);
            }
        }
    }
}
