using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    //All the dialogues from the start to the end of the tutorial
    public GameObject[] popUps;
    private int popUpIndex;

    public GameObject mineralSpawner;
    public GameObject creatureSpawner;
    public GameObject Mineral;
    public Mineral mineralScript;

    public bool baseTutorial = false;
    public bool towerTutorial = false;
    private bool mineralSpawned = false;
    private bool resources_added = false;

    //to check if the player clicks on the desired keys
    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;

    private bool delayStarted = false;

    public void Update()
    {

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
                popUps[i].SetActive(true);
            else
                popUps[i].SetActive(false);
        }

        if (popUpIndex == 0 || popUpIndex == 1 || popUpIndex == 2)
        {
            //If player clicks Enter, Spacebar or LeftClick he goes to next dialogue box
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            //give resources to the player so he can spawn a unit
            if (!resources_added)
            {
                mineralScript.AddResources(5f, 5f, 5f, 5f);
                resources_added = true;
            }

            //Wait until the player spawns a "Player"
            CheckForPlayerSpawn();
        }
        else if (popUpIndex == 4)
        {
            //Wait for the player to use the desired keys to move
            CheckForMovementKeys();
        }
        else if (popUpIndex == 5)
        {
            if (!mineralSpawned)
                SpawnMineral();
        }
        else if (popUpIndex == 6)
        {
            baseTutorial = true;
        }
        else if (popUpIndex == 7)
        {
            towerTutorial = true;
        }
        else if (popUpIndex == 8)
        {
            if (!delayStarted)
            {
                StartCoroutine(CheckForLeftClickWithDelay());
                delayStarted = true;
            }
        }
        else if (popUpIndex == 9)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                mineralSpawner.SetActive(true);
                creatureSpawner.SetActive(true);
                popUpIndex++;
            }
        }
    }

    private IEnumerator CheckForLeftClickWithDelay()
    {
        yield return new WaitForSeconds(0.5f); 
        while (popUpIndex == 8)
        {
            if (Input.GetMouseButtonDown(0))
            {
                popUpIndex++;
            }
            yield return null;
        }
        delayStarted = false;
    }

    public void TowerTutorial()
    {
        popUpIndex++;
        Debug.Log("Tower spawned, popUpIndex incremented to " + popUpIndex);
        towerTutorial = false;
    }

    public void OnBaseCollision()
    {
        popUpIndex++;
        Debug.Log("Base collision, popUpIndex incremented to " + popUpIndex);
        baseTutorial = false;
    }

    void CheckForPlayerSpawn()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            popUpIndex++;
            Debug.Log("Player spawned, popUpIndex incremented to " + popUpIndex);
        }
        else
        {
            Debug.Log("Player not found");
        }
    }

    void SpawnMineral()
    {
        mineralSpawned = true;
        Vector3 position = new Vector3(1f, -2.86f, 0f);
        Instantiate(Mineral, position, Quaternion.identity);
    }

    void CheckForMovementKeys()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            sPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dPressed = true;
        }

        if (wPressed && aPressed && sPressed && dPressed)
        {
            popUpIndex++;
        }
    }

    public void OnMineralMined()
    {
        popUpIndex++;
        Debug.Log("Mineral mined, popUpIndex incremented to " + popUpIndex);
    }
}
