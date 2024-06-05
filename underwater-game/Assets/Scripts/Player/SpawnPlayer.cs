using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject playerObject;
    public Mineral playerResources;
    public string playerTag = "Player";
    public Button spawnButton;
    public CameraFollow cameraFollow;
    public Stats playerStats;


    void Start()
    {
        if(playerStats == null)
        {
            Debug.Log("player Stats is Null.");
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag(playerTag) != null)
        {
            spawnButton.interactable = false;
        }
        else
        {
            spawnButton.interactable = true;
        }
    }


    public void SpawnTower()
    {

        TowerCost towerCost = playerObject.GetComponent<TowerCost>();
        Vector3 spawnPos = new Vector3(0f, 0f, 0f);


        if (towerCost != null && playerResources != null)
        {
            if (playerResources.CheckResources(towerCost.woodCost, towerCost.stoneCost, towerCost.ironCost, towerCost.goldCost))
            {
                GameObject newPlayer = Instantiate(playerObject, spawnPos, Quaternion.identity);
                Health health = newPlayer.GetComponent<Health>();
                PlayerMovement movement = newPlayer.GetComponent<PlayerMovement>();

                if (health != null)
                {
                    health.maxHealth = playerStats.playerHealth;
                }

                if (movement != null)
                {
                    movement.speed = playerStats.playerSpeed;
                }

                cameraFollow.player = newPlayer.transform;
            }
            else
            {
                Debug.Log("Not enough resources");
            }
        }
        else
        {
            Debug.Log("Tower cost or playerResources not found");
        }
    }
}
