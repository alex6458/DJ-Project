using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnDistance = 1.0f; // Distance ahead of the tower to spawn the bullet
    public string targetTag = "Enemy"; // Tag of the target object
    public float attackSpeed = 2.0f; // Time interval between attacks in seconds
    public float attackRange = 5.0f; // Range within which the tower can attack
    private float lastAttackTime = 0f; // Time when the last attack occurred
    public float attackDamage = 1f; // Tower damage
    public string towerType = "None";
    private Transform target; // Reference to the current target

    void Update()
    {
        var healthScript = GetComponent<Health>();
        if (healthScript == null)
        {
            Debug.Log("No healthScript found");
            return;
        }

        if (healthScript.stunned)
        {
            return;
        }

        // Find the closest target within range
        FindTarget();

        // If a target is found, check attack cooldown and perform attack
        if (target != null)
        {
            // Check attack cooldown
            if (Time.time >= lastAttackTime + 1f/attackSpeed)
            {
                PerformAttack();
            }
        }
    }

    public void Die()
    {

        var script = GameObject.Find("_Scripts");

        var spawner = script.GetComponent<Spawner>();

        var cellPosDefault = spawner.spawnTilemap.WorldToCell(transform.position);
        spawner.spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.Sprite);
        Destroy(gameObject);
    }

    void PerformAttack()
    {
        // Check if the target is within attack range
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            // Calculate spawn position slightly ahead of the tower
            Vector3 spawnPosition = transform.position + transform.forward * bulletSpawnDistance;
            // Spawn a bullet at the calculated position
            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, bulletPrefab.transform.rotation, transform.parent);
                RangeAttackTower rangeAttack = bullet.GetComponent<RangeAttackTower>();

                if (rangeAttack != null)
                {
                    rangeAttack.SetDamage(attackDamage);
                }
            }

            // Update the last attack time to the current time
            lastAttackTime = Time.time;
        }
        else
        {
            // Reset the target if it moves out of range
            target = null;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = attackRange;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy;
    }
}
