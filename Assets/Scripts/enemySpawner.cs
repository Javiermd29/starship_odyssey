using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int enemiesPerRound;
    [SerializeField] private float minSpawnDistance;

    private int enemiesRemaining = 0; // Enemigos por ronda
    private int currentEnemyCount = 0; // Enemigos vivos en la ronda

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Limites de spawn del enemigo
    private float minX = -0.3f, maxX = 30f;
    private float minY = -18f, maxY = 12.5f;

    // Numero de ronda
    private int currentRound = 0;

    void Start()
    {
        StartNewRound();

        if (enemiesRemaining > 0) return;

    }

    private void StartNewRound()
    {
        currentRound++;
        Debug.Log("RONDA: " + currentRound);


        enemiesRemaining = enemiesPerRound;

        if (currentRound > 3)
        {

            Debug.Log("no se spawnean más enemigos");

            return;
        }

        StartCoroutine(SpawnEnemies());

    }

    IEnumerator SpawnEnemies()
    {

        for (int i = 0; i < enemiesPerRound; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

    }

    private void SpawnEnemy()
    {
        if (spawnedEnemies.Count < enemiesPerRound)
        {
            Vector2 spawnPosition;

            // Intenta generar un enemigo en una posición válida
            int attempts = 10;

            do
            {

                spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                Debug.Log(spawnPosition);
                attempts--;

            } while (!IsValidSpawnPosition(spawnPosition) && attempts > 0);

            if (attempts > 0)
            {
                // Crea el enemigo y añade a la lista
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
                return false; // La posición está demasiado cerca de otro enemigo
            }
        }
        return true;
    }

    public void EnemyDestroyed(GameObject enemy)
    {

        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
        }

        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaitAndStartNextRound());
        }

    }

    IEnumerator WaitAndStartNextRound()
    {
        yield return new WaitForSeconds(2f);
        StartNewRound();
    }

}
