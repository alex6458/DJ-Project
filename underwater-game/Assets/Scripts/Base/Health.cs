using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image HealthBarUi;
    private float fillamount = 0f;
    private float maxHealth = 10f;
    public float currentHealth = 10f;


    void UpdateHealthBar()
    {


        // Calculate the fill amount based on current health
        fillamount = currentHealth / maxHealth;
        HealthBarUi.fillAmount = fillamount;


    }

    // Method to take damage and update health bar
    public void TakeDamage(float damageAmount)
    {

        Debug.Log("TAKING DAMAGE");
        // Reduce current health
        currentHealth -= damageAmount;

        // Ensure current health does not go below zero
        currentHealth = Mathf.Max(currentHealth, 0f);

        // Update the health bar UI
        UpdateHealthBar();
    }
}
