using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Ground Tiles")]
    public List<GameObject> groundTilePrefabs = new();
    public Transform nextSpawnPoint;

    [SerializeField] private RunnerGameManager RunnerGameManager;
    [SerializeField] private StartScreenController startScreen;

    [Header("Player")]
    [SerializeField] private GameObject playerCharacter;

    [SerializeField] private List<GameObject> spawnedTiles = new();


    private void Start()
    {
        Time.timeScale = 0;
        StartSpawning();
    }

    public void StartSpawning()
    {
        startScreen.LevelTitle();
        if (RunnerGameManager.currentLevel == RunnerGameManager.GetCurrentLevel())
        {
            for (int i = 0; i < 15; i++)
            {
                SpawnTile(true);
            }
        }
    }

    public void SetCurrentLevel(int level)
    {
        playerCharacter.transform.position = nextSpawnPoint.position;
        RunnerGameManager.currentLevel = level;
    }

    public GameObject SpawnTile(bool spawnItems)
    {        
        if (RunnerGameManager.currentLevel <= groundTilePrefabs.Count)
        {
            int prefabIndex = RunnerGameManager.currentLevel;
            if (prefabIndex >= 0 && prefabIndex < groundTilePrefabs.Count)
            {
                GameObject tilePrefab = groundTilePrefabs[prefabIndex];                
                GameObject temp = Instantiate(tilePrefab, nextSpawnPoint.position, Quaternion.identity, RunnerGameManager.levelManager);
                spawnedTiles.Add(temp);
                nextSpawnPoint = temp.transform.Find("NextSpawnPoint");

                if (spawnItems)
                {
                    temp.GetComponent<GroundTile>().SpawnObstacle();
                }

                return temp;
            }
            else
            {
                Debug.Log("No spawnea");
            }
        }
        else
        {
            Debug.Log("No spawnea");
        }
        return null;
    }

    public void DestroyPreviousLevelTiles()
    {
        foreach (var tile in spawnedTiles)
        {
            Destroy(tile);
        }
        spawnedTiles.Clear();
    }
}
