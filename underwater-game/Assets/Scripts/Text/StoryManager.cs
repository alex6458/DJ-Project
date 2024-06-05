using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public GameObject[] popUps;
    public GameObject[] finalDialogue;
    public int popUpIndex = 0;
    private int DialogueIndex = 0;
    private bool storyShown = false;
    public SpawnCreatures spawnScript;
    private bool FinalWaveSpawned = false;
    void Update()
    {

        if(storyShown)
        {
            //If player clicks Enter, Spacebar or LeftClick he goes to next dialogue box
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                storyShown = false;
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
            }
        }

        if (popUpIndex == popUps.Length)
        {
            for (int i = 0; i < finalDialogue.Length; i++)
            {
                if (i == DialogueIndex)
                    finalDialogue[i].SetActive(true);
                else
                    finalDialogue[i].SetActive(false);
            }


            if (!PauseMenu.IsPaused)
            {
                if (DialogueIndex < finalDialogue.Length)
                {
                    //If player clicks Enter, Spacebar or LeftClick he goes to next dialogue box
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        DialogueIndex++;
                    }
                }
            }

            if (DialogueIndex >= finalDialogue.Length && !FinalWaveSpawned)
            {
                FinalWaveSpawned = true;
                StartCoroutine(spawnScript.FinalWave());
            }
        }

    }


    public void ShowStory()
    {

        popUps[popUpIndex].SetActive(true);
        storyShown = true;

    }
}
