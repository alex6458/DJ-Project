using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseBehaviour : MonoBehaviour
{

    private Health healthScript;
    private AudioSource audioSource;
    public Mineral mineralScript;
    private TutorialManager tutorialManager;

    private void Start()
    {
        // Get the HealthScript component attached to this GameObject
        healthScript = GetComponent<Health>();

        // Check if the HealthScript component was found
        if (healthScript == null)
        {
            Debug.LogError("HealthScript component not found on this GameObject!");
        }

        // Get the existing AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Optional: Check if the AudioSource component exists and log a warning if it doesn't
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the game object.");
        }

        tutorialManager = FindObjectOfType<TutorialManager>();

        if (tutorialManager == null)
        {
            Debug.LogWarning("TutorialManager component not found.");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetResources();
        }

    }

    public void Die()
    {
        // Check if the healthScript reference is valid
        if (healthScript != null)
        {
            // Check if own health is less than or equal to zero
            if (healthScript.currentHealth <= 0)
            {
                // Perform actions when own health is zero or below
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            Debug.LogError("HealthScript reference is null!");
        }
    }

    private void GetResources()
    {
        // Find the GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Check if playerObject is found
        if (playerObject != null)
        {
            // Get the Mineral script attached to the playerObject
            Mineral playerResources = playerObject.GetComponent<Mineral>();

            // Check if the Mineral script was found
            if (playerResources != null)
            {

                if(playerResources.isPoor == false) // Player has resources to deposit
                {

                    if(audioSource != null)
                    {
                        audioSource.Play();
                    }

                    mineralScript.AddResources(playerResources.wood , playerResources.stone , playerResources.iron , playerResources.gold);
                    playerResources.ResetResources();
                }
                else
                {
                    Debug.Log("Player has no resources to deposit");
                }

                if(tutorialManager.baseTutorial)
                    tutorialManager.OnBaseCollision();

            }
            else
            {
                Debug.Log("No Mineral script found on the object with the 'Player' tag");
            }
        }
        else
        {
            Debug.Log("No object with the 'Player' tag found");
        }
    }
}
