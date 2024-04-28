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
