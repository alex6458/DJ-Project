using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayGame : MonoBehaviour
{

    public AudioSource StartSound;

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

            // Wait for the duration of the audio clip
            yield return new WaitForSeconds(0.5f);

            // Load the next scene after the sound has finished playing
            SceneManager.LoadScene(1);
        }

    }

}

