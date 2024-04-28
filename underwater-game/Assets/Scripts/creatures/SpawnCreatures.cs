using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.VersionControl.Message;

public class SpawnCreatures : MonoBehaviour
{
    public GameObject[] bodyPrefabs;
    public GameObject[] tailPrefabs;
    public GameObject base_creature;

    public Vector3 default_body_pos = new Vector3(0f, 0f, 0f);
    public Vector3 default_tail_pos = new Vector3(0f, 0f, 0f);
    public Vector3 default_creature_pos = new Vector3(5f, 1f, 0f);

    public float spawn_interval = 5f;

    float currTime;
    float lastSpawnTime;



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
    

    // Start is called before the first frame update
    void Start()
    {
        currTime = Time.time;
        lastSpawnTime = currTime;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime - lastSpawnTime > spawn_interval)
        {
            CreateRandomCreature();
            lastSpawnTime = currTime;
        } 
    }

}
