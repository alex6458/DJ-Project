using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    int spawnID = -1;
    public Mineral playerResources;
    public List<GameObject> towersPrefabs;
    public List<Image> towersUI;
    public Tilemap spawnTilemap;
    public Transform spawnTowerRoot;
    public Stats towerStats;
    private TutorialManager tutorialManager;

    void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();

        if (tutorialManager == null)
            Debug.LogWarning("TutorialManager component not found.");

        if (spawnTilemap == null)
            Debug.LogWarning("SpawnTilemap component not found.");

        if (spawnTowerRoot == null)
            Debug.LogWarning("spawnTowerRoot component not found");

        if (towerStats == null)
            Debug.LogWarning("towerStats component not found");
    }

    void Update()
    {
        if (CanSpawn())
            DetectSpawnPoint();
    }

    bool CanSpawn()
    {
        return spawnID != -1;
    }

    void DetectSpawnPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
            var cellPosCentered = spawnTilemap.GetCellCenterWorld(cellPosDefault);

            Debug.Log(spawnTilemap.GetColliderType(cellPosDefault));

            if (spawnTilemap.GetColliderType(cellPosDefault) == Tile.ColliderType.Sprite)
            {
                cellPosCentered = new Vector3(cellPosCentered.x, cellPosCentered.y, 0);
                TowerCost towerCost = towersPrefabs[spawnID].GetComponent<TowerCost>();

                if (towerCost != null && playerResources != null)
                {
                    if (playerResources.CheckResources(towerCost.woodCost, towerCost.stoneCost, towerCost.ironCost, towerCost.goldCost))
                    {
                        SpawnTower(cellPosCentered);
                        spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
                        DeselectTower();
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
    }

    private void SpawnTower(Vector3 pos)
    {
        GameObject tower = Instantiate(towersPrefabs[spawnID], spawnTowerRoot);
        tower.transform.position = pos;

        if (tutorialManager.towerTutorial)
            tutorialManager.OnBaseCollision();

        Health health = tower.GetComponent<Health>();
        TowerBehaviour towerb = tower.GetComponent<TowerBehaviour>();
        
        if(towerb != null)
        {
            if(towerb.towerType == "Archer")
            {
                towerb.attackSpeed = towerStats.ArcherAttackSpeed;
                towerb.attackDamage = towerStats.ArcherDamage;

                if(health != null)
                {
                    health.maxHealth = towerStats.ArcherHealth;   
                }
                else
                    Debug.Log("Health Script not found");
            }
            else if (towerb.towerType == "Mage")
            {
                towerb.attackSpeed = towerStats.MageAttackSpeed;
                towerb.attackDamage = towerStats.MageDamage;

                if (health != null)
                {
                    health.maxHealth = towerStats.MageHealth;
                }
                else
                    Debug.Log("Health Script not found");
            }
        }
        else
        {
            Debug.Log("Tower Behaviour script not found");
        }
    }

    public void SelectTower(int id)
    {
        if (spawnID == id)
        {
            DeselectTower();
        }
        else
        {
            DeselectTower();
            spawnID = id;
            towersUI[spawnID].color = Color.white;
            spawnTilemap.gameObject.SetActive(true);
        }
    }

    public void DeselectTower()
    {
        spawnID = -1;
        foreach (var t in towersUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
        spawnTilemap.gameObject.SetActive(false);
    }
}
