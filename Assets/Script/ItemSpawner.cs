using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs; // 아이템 프리팹을 저장할 리스트
    public List<Transform> spawnPoints; // 아이템을 배치할 위치를 저장할 리스트

    public int numberOfItemsToSpawn = 3; // 배치할 아이템 개수

    private List<Transform> usedSpawnPoints = new List<Transform>(); // 이미 사용한 배치 위치를 저장할 리스트

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        // 중복 없이 아이템을 배치할 위치 선택
        int itemsSpawned = 0;
        while (itemsSpawned < numberOfItemsToSpawn && spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];

            if (!usedSpawnPoints.Contains(spawnPoint))
            {
                // 중복된 위치가 아니면 아이템 생성
                int randomItemIndex = Random.Range(0, itemPrefabs.Count);
                GameObject itemPrefab = itemPrefabs[randomItemIndex];
                Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);

                // 사용한 위치를 기록
                usedSpawnPoints.Add(spawnPoint);

                itemsSpawned++;
            }
        }
    }
}
