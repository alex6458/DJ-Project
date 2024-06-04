using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    private bool storyShown = false;


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

    }

    public void ShowStory()
    {

        popUps[popUpIndex].SetActive(true);
        storyShown = true;

    }
}
