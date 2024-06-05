using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public GameObject[] bodyPrefabs;
    public GameObject base_creature;

    void Start()
    {
        base_creature.GetComponent<Health>().maxHealth = 10;
        base_creature.GetComponent<Health>().currentHealth = 10;

        foreach (GameObject prefab in bodyPrefabs)
        {
            prefab.GetComponent<BodyBase>().attackDamage = 1f;
        }

    }

}
