using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies = 5;
    private int currentEnemyCount = 0;
    public float minSpawnDistance = 10f;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private float minX = -0.3f, maxX = 30f;
    private float minY = -18f, maxY = 12.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemies.Count < maxEnemies)
        {
            Vector2 spawnPosition;

            // Intenta generar un enemigo en una posici�n v�lida
            int attempts = 10;
            do
            {
                spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                Debug.Log(spawnPosition);
                attempts--;
            } while (!IsValidSpawnPosition(spawnPosition) && attempts > 0);

            if (attempts > 0)
            {
                // Crea el enemigo y a�ade a la lista
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
            }
        }
    }

    bool IsValidSpawnPosition(Vector2 position)
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null && Vector2.Distance(position, enemy.transform.position) < minSpawnDistance)
            {
                return false; // La posici�n est� demasiado cerca de otro enemigo
            }
        }
        return true;
    }

    public void EnemyDestroyed()
    {
        // Llama a este m�todo cuando un enemigo sea destruido
        currentEnemyCount--;
    }

}
