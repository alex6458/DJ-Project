using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayGame : MonoBehaviour
{

    public AudioSource StartSound;
    public Animator animator;
    private int nextScene;

    public void StartGame()
    {

        StartCoroutine(PlayStartSoundAndLoadScene());

    }


    private IEnumerator PlayStartSoundAndLoadScene()
    {


        // Ensure the AudioSource is not null and has a clip assigned
        if (StartSound != null)
        {
            // Play the start sound
            StartSound.Play();
            animator.SetTrigger("FadeOut");
            // Wait for the duration of the audio clip
            yield return new WaitForSeconds(1);

            // Load the next scene after the sound has finished playing
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(1);
        }

    }

}

