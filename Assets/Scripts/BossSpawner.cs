using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    [SerializeField] private GameObject bossPrefab;

    // It spawns the boss
    public void SpawnBoss()
    {
        Vector2 spawnPosition = new Vector2(27.54f, -2.28f);
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}
