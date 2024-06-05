using UnityEngine;
using System.Collections;


public class Secret : MonoBehaviour
{
    private bool collisionState = false;
    private bool found = false;
    private bool Dialog = false;
    private AudioSource audioSource;

    
    private void Start()
    {
        // Get the existing AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Optional: Check if the AudioSource component exists and log a warning if it doesn't
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the game object.");
        }

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collisionState = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionState = false;
    }

    private void OnMouseOver()
    {
        // right click clicked and collision detected
        if (Input.GetMouseButtonDown(1) && collisionState && !found) // Change 1 to 0 for left click, 2 for middle click
        {
            Debug.Log("Right clicked");
            found = true;
            FindSecret();
        }
    }

    private void FindSecret()
    {
        // Find the GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        StoryManager storyManager = FindObjectOfType<StoryManager>();


        if (storyManager == null)
        {
            Debug.LogError("StoryManager script not found in the scene!");
        }


        // Check if playerObject is found
        if (playerObject != null)
        {

            if (audioSource != null)
            {
                audioSource.Play();
                storyManager.ShowStory();
                StartCoroutine(DestroyAfterSound(audioSource.clip.length));
            }

        }
        else
        {
            Debug.Log("No object with the 'Player' tag found");
        }
    }


    private IEnumerator DestroyAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
