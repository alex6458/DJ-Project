using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float playerSpeed;
    public float playerHealth;

    public float ArcherAttackSpeed;
    public float ArcherHealth;
    public float ArcherDamage;

    public float MageAttackSpeed;
    public float MageHealth;
    public float MageDamage;



    public void UpgradePlayerHealth(float amount)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerHealth += amount;

        foreach (GameObject player in players)
        {
            Health health = player.GetComponent<Health>();
            if (health != null)
            {
                health.maxHealth = playerHealth;
            }
        }
        Debug.Log("All players' health upgraded to: " + playerHealth);
    }

    public void UpgradePlayerSpeed(float amount)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playerSpeed += amount;

        foreach (GameObject player in players)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.speed = playerSpeed;
            }
        }
        Debug.Log("All players' speed upgraded to: " + playerSpeed);
    }

    public void UpgradeMageHealth(float amount)
    {
        GameObject[] friendlyTowers = GameObject.FindGameObjectsWithTag("Friendly");
        MageHealth += amount;

        foreach (GameObject tower in friendlyTowers)
        {
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();
            Health health = tower.GetComponent<Health>();

            if (towerBehaviour != null && towerBehaviour.towerType == "Mage")
            {
                if (health != null)
                {
                    Debug.Log("Mage Health upgraded to: " + MageHealth);
                    health.maxHealth = MageHealth;
                }
            }
        }
    }

    public void UpgradeMageAttackSpeed(float amount)
    {
        GameObject[] friendlyTowers = GameObject.FindGameObjectsWithTag("Friendly");
        MageAttackSpeed += amount;

        foreach (GameObject tower in friendlyTowers)
        {
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();

            if (towerBehaviour != null && towerBehaviour.towerType == "Mage")
            {
                Debug.Log("Mage Attack Speed upgraded to: " + MageAttackSpeed);
                towerBehaviour.attackSpeed = MageAttackSpeed;
            }
        }
    }

    public void UpgradeMageDamage(float amount)
    {
        GameObject[] friendlyTowers = GameObject.FindGameObjectsWithTag("Friendly");
        MageDamage += amount;

        foreach (GameObject tower in friendlyTowers)
        {
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();

            if (towerBehaviour != null && towerBehaviour.towerType == "Mage")
            {
                Debug.Log("Mage Damage upgraded to: " + MageDamage);
                towerBehaviour.attackDamage = MageDamage;
            }
        }
    }

    public void UpgradeArcherHealth(float amount)
    {
        GameObject[] friendlyTowers = GameObject.FindGameObjectsWithTag("Friendly");
        ArcherHealth += amount;

        foreach (GameObject tower in friendlyTowers)
        {
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();
            Health health = tower.GetComponent<Health>();

            if (towerBehaviour != null && towerBehaviour.towerType == "Archer")
            {
                if (health != null)
                {

                    Debug.Log("Archer Health upgraded to: " + ArcherHealth);
                    health.maxHealth = ArcherHealth;
                }
            }
        }
    }

    public void UpgradeArcherAttackSpeed(float amount)
    {
        GameObject[] friendlyTowers = GameObject.FindGameObjectsWithTag("Friendly");
        ArcherAttackSpeed += amount;

        foreach (GameObject tower in friendlyTowers)
        {
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();

            if (towerBehaviour != null && towerBehaviour.towerType == "Archer")
            {
                Debug.Log("Archer Attack Speed upgraded to: " + ArcherAttackSpeed);
                towerBehaviour.attackSpeed = ArcherAttackSpeed;
            }
        }
    }

    public void UpgradeArcherDamage(float amount)
    {
        GameObject[] friendlyTowers = GameObject.FindGameObjectsWithTag("Friendly");
        ArcherDamage += amount;

        foreach (GameObject tower in friendlyTowers)
        {
            TowerBehaviour towerBehaviour = tower.GetComponent<TowerBehaviour>();

            if (towerBehaviour != null && towerBehaviour.towerType == "Archer")
            {
                Debug.Log("Archer Damage upgraded to: " + ArcherDamage);
                towerBehaviour.attackDamage = ArcherDamage;
            }
        }
    }
}
