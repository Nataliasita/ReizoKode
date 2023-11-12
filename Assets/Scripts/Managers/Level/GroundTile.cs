using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    [Header("Objects")]
    [SerializeField] GameObject[] obstaclePrefab;
    [SerializeField] GameObject pointPrefab;
    public GameObject rockPrefab;
    public Vector3 spawnPositionRock = new Vector3(0.207f, 1.204f, -0.743f);
    public Vector3 spawnRotationRock  = new Vector3(-80.88f, 24.63f, 0f);
    private void Start() 
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        Quaternion rotationRock = Quaternion.Euler(spawnRotationRock);
        Instantiate(rockPrefab,spawnPositionRock , rotationRock);
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        int randomPrefabIndex = Random.Range(0, obstaclePrefab.Length);
        Instantiate(obstaclePrefab[randomPrefabIndex], spawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnCoins()
    {
        int coinsToSpawn = 5;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(pointPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}

