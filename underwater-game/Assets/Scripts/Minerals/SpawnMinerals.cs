using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnMinerals : MonoBehaviour
{

    public List<GameObject> MineralsPrefabs;
    private int waveNumber = 0;
    private float randomValue = 0;
    private float randomX = 0;
    private int lastStory = 0;
    private int storyIndex = 0;
    public float timeBetweenWaves = 120f; // Add time between waves variable
    public int storyWave = 5;
    public StoryManager story;

    // Start is called before the first frame update
    void Start()
    {
        if(story == null)
        {
            Debug.Log("Story manager not found.");
        }

        StartCoroutine(Spawner()); // Start spawning coroutine
    }


    void CreateMinerals()
    {
        randomValue = UnityEngine.Random.Range(0f, 1f); // Correct Random.Range arguments
        randomX = UnityEngine.Random.Range(1f, 50f * waveNumber); // Correct Random.Range arguments

        if (randomValue < 0.5f)
        {
            Instantiate(MineralsPrefabs[0], new Vector3(randomX, -2.86f, 0), Quaternion.identity); // Use square brackets to access list elements
        }
        else if (randomValue < 0.9f)
        {
            Instantiate(MineralsPrefabs[1], new Vector3(randomX, -2.86f, 0), Quaternion.identity); // Use square brackets to access list elements
        }
        else
        {
            Instantiate(MineralsPrefabs[2], new Vector3(randomX, -2.86f, 0), Quaternion.identity); // Use square brackets to access list elements
        }

    }

    IEnumerator Spawner()
    {
        while (true)
        {
            waveNumber++;

            // Calculate the number of minerals based on wave number
            int numMinerals = waveNumber * 10;

            if (waveNumber - lastStory == storyWave && story.popUps.Count() != storyIndex)
            {
                lastStory = waveNumber;
                Instantiate(MineralsPrefabs[3], new Vector3(randomX, -3.01f, 0), Quaternion.identity);
                storyIndex++;

            }

            for (int i = 0; i < numMinerals; i++)
            {
                CreateMinerals();
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
