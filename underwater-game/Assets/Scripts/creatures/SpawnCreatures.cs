using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class SpawnCreatures : MonoBehaviour
{
    public GameObject[] bodyPrefabs;
    public GameObject[] tailPrefabs;
    public GameObject base_creature;

    public Vector3 default_body_pos = new Vector3(0f, 0f, 0f);
    public Vector3 default_tail_pos = new Vector3(0f, 0f, 0f);
    public Vector3 default_creature_pos = new Vector3(5f, 1f, 0f);

    public float spawn_interval = 5f;

    public float spawnInterval = 5f; 
    public float timeBetweenWaves = 120f; 
    public float difficultyIncreaseRate = 0.1f;   //tendo em conta a wave podemos usar este valor para ajustar parametros

    public TextMeshProUGUI waveWarningText;
    public GameObject waveWarningObject;

    private int waveNumber = 0;



    public void SpawnCreature(GameObject body, GameObject tail)
    {
        var creature = Instantiate(base_creature, default_creature_pos, Quaternion.identity);

        var body_pos = creature.transform.localPosition + default_body_pos + body.GetComponent<BodyBase>().posOffset;
        var creature_body = Instantiate(body, body_pos, Quaternion.identity, creature.transform);

        var tail_pos = creature.transform.localPosition + default_tail_pos + tail.GetComponent<TailBase>().posOffset;
        var creature_tail = Instantiate(tail, tail_pos, Quaternion.identity, creature.transform);

        var creature_script = creature.GetComponent<CreatureBase>();
        creature_script.body = creature_body;
        creature_script.tail = creature_tail;
    }

    public void CreateRandomCreature()
    {
        var random = new System.Random();
        var body = bodyPrefabs[random.Next(bodyPrefabs.Length)];
        var tail = tailPrefabs[random.Next(tailPrefabs.Length)];
        SpawnCreature(body, tail);
    }


    IEnumerator SpawnEnemyWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            waveNumber++;
            waveWarningObject.SetActive(true);
            waveWarningText.text = "Wave " + waveNumber + " incoming!"; // Update the warning message

            yield return new WaitForSeconds(2f); // Display the warning for 2 seconds

            waveWarningObject.SetActive(false); // Disable the warning text

            // Calculate the number of enemies based on wave number
            int numEnemies = waveNumber * 2;

            for (int i = 0; i < numEnemies; i++)
            {
                CreateRandomCreature();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWave());
    }
}
