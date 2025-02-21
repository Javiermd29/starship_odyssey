using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    private int enemiesPerRound = 5;
    [SerializeField] private float minSpawnDistance;

    private int enemiesRemaining = 0;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Enemy spawn limits
    private float minX = -0.3f, maxX = 30f;
    private float minY = -18f, maxY = 12.5f;

    // Round number
    private int currentRound = 0;

    void Start()
    {
        StartNewRound();

        if (enemiesRemaining > 0) return;

    }

    private void StartNewRound()
    {
        currentRound++;
        UIManager.Instance.roundText.text = "ROUND: " + currentRound; // Update the round display in the UI

        enemiesRemaining = enemiesPerRound;  // Set the number of remaining enemies to the total per round

        int bossRound = PlayerPrefs.GetInt("BossRound", 4); // Retrieve the boss round threshold from PlayerPrefs (default is 4)

        // If the current round exceeds the boss round, spawn the boss
        if (currentRound > bossRound)
        {
            FindObjectOfType<BossSpawner>()?.SpawnBoss();
            UIManager.Instance.roundText.text = "BOSS TIME !"; // Update the UI to indicate boss fight

            return;
        }

        // If it's not a boss round, start spawning regular enemies
        StartCoroutine(SpawnEnemies());

    }

    IEnumerator SpawnEnemies()
    {

        for (int i = 0; i < enemiesPerRound; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);  // Wait for the specified interval before spawning the next enemy
        }

    }

    private void SpawnEnemy()
    {
        // Check if the number of spawned enemies is still below the limit for this round
        if (spawnedEnemies.Count < enemiesPerRound)
        {
            Vector2 spawnPosition;

            // Try to spawn an enemy in a valid position
            int attempts = 10;

            do
            {
                // Generate a random position within the allowed range
                spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                Debug.Log(spawnPosition);
                attempts--;

            } while (!IsValidSpawnPosition(spawnPosition) && attempts > 0);

            // If a valid position was found within the attempt limit
            if (attempts > 0)
            {
                // Create the enemy and add it to the list
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
            }
        }
    }

    bool IsValidSpawnPosition(Vector2 position)
    {
        // Check each spawned enemy to ensure the new spawn position is not too close to them
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null && Vector2.Distance(position, enemy.transform.position) < minSpawnDistance)
            {
                return false; // The position is too close to another enemy
            }
        }
        return true;
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        // Check if the enemy exists in the list of spawned enemies
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy); // Remove the destroyed enemy from the list
        }

        enemiesRemaining--; // Decrease the count of remaining enemies

        // If no enemies are left, start the next round after a delay
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
