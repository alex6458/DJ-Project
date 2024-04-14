using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    //id of tower to spawn
    int spawnID = -1;

    //List of towers prefabs
    public List<GameObject> towersPrefabs;

    //List of towers UI
    public List<Image> towersUI;

    //Spawnpoints tilemap
    public Tilemap spawnTilemap;

    public Transform spawnTowerRoot;


    void Update()
    {
        if((CanSpawn()))
            DetectSpawnPoint();
    }

    bool CanSpawn()
    {
        if (spawnID == -1)
            return false;

        return true;
    }

    void DetectSpawnPoint()
    {
        //If mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //get the mouse position
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //get position of cell in tilemap
            var CellPosDefault = spawnTilemap.WorldToCell(mousePos);

            //get the center position of the cell
            var CellPosCentered = spawnTilemap.GetCellCenterWorld(CellPosDefault);

            Debug.Log(spawnTilemap.GetColliderType(CellPosDefault));

            //check if we can spawn in that cell
            //if (spawnTilemap.GetColliderType(CellPosDefault) == Tile.ColliderType.None)
            //{
                CellPosCentered = new Vector3(CellPosCentered.x, CellPosCentered.y, 0);
                SpawnTower(CellPosCentered);
                spawnTilemap.SetColliderType(CellPosDefault, Tile.ColliderType.None);
            //}
        }

    }

    private void SpawnTower(Vector3 pos)
    {
        GameObject tower = Instantiate(towersPrefabs[spawnID] , spawnTowerRoot);
        tower.transform.position = pos;
    }


    public void SelectTower(int id)
    {
        DeselectTower();
        spawnID = id;
        towersUI[spawnID].color = Color.white;

    }

    public void DeselectTower()
    {
        spawnID = -1;
        foreach(var t in towersUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

}