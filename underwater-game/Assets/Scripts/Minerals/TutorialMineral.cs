using UnityEngine;
using System.Collections;


public class TutorialMineral : MonoBehaviour
{
    public float Resource1 = 0f;
    public float Resource2 = 0f;
    public float Resource3 = 0f;
    public float Resource4 = 0f;
    private bool collisionState = false;

    private AudioSource audioSource;
    private TutorialManager tutorialManager;



    private void Start()
    {
        // Get the existing AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Optional: Check if the AudioSource component exists and log a warning if it doesn't
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on the game object.");
        }

        tutorialManager = FindObjectOfType<TutorialManager>();

        if(tutorialManager  == null)
        {
            Debug.LogWarning("TutorialManager component not found.");
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
        if (Input.GetMouseButtonDown(1) && collisionState) // Change 1 to 0 for left click, 2 for middle click
        {
            Debug.Log("Right clicked");
            MineBlock();
        }
    }

    private void MineBlock()
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
                playerResources.StoreResources(Resource1, Resource2, Resource3, Resource4);

                if (audioSource != null)
                {
                    audioSource.Play();
                    tutorialManager.OnMineralMined();

                    StartCoroutine(DestroyAfterSound(audioSource.clip.length));
                }
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


    private IEnumerator DestroyAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
