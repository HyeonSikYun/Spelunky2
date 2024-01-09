using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs; // ������ �������� ������ ����Ʈ
    public List<Transform> spawnPoints; // �������� ��ġ�� ��ġ�� ������ ����Ʈ

    public int numberOfItemsToSpawn = 3; // ��ġ�� ������ ����

    private List<Transform> usedSpawnPoints = new List<Transform>(); // �̹� ����� ��ġ ��ġ�� ������ ����Ʈ

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        // �ߺ� ���� �������� ��ġ�� ��ġ ����
        int itemsSpawned = 0;
        while (itemsSpawned < numberOfItemsToSpawn && spawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomIndex];

            if (!usedSpawnPoints.Contains(spawnPoint))
            {
                // �ߺ��� ��ġ�� �ƴϸ� ������ ����
                int randomItemIndex = Random.Range(0, itemPrefabs.Count);
                GameObject itemPrefab = itemPrefabs[randomItemIndex];
                Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);

                // ����� ��ġ�� ���
                usedSpawnPoints.Add(spawnPoint);

                itemsSpawned++;
            }
        }
    }
}
