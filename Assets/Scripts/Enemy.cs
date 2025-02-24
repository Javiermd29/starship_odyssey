using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{

    // Random enemy movement
    private float speed = 4f;
    private float changeDirectionInterval = 2f; // Interval to change direction
    private Vector2 direction; // Current direction of movement
    private float timer;

    private float detecionRadius = 2f;

    // Enemy movement boundaries
    private float boundsX = 30f;
    private float boundsNegativeX = 2f;
    private float boundsY = 12.5f;
    private float boundsNegativeY = -18f;

    // Screen limits
    private Vector2 screenBounds;
    [SerializeField] private int lifePoints;

    // Powerup prefab and drop chance
    public GameObject powerup1Prefab;
    public float dropChance = 0.2f;

    // Particle system and death sound
    [SerializeField] private GameObject EffectOnDestroyPrefab;
    [SerializeField] private AudioClip deathClip;

    private Vector3 particleOffset = Vector3.forward * -1;

    void Start()
    {
        // Determine the boundaries of the screen (in world units)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        timer = changeDirectionInterval;
    }

    void Update()
    {
        // Move the enemy in the current direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Detect collisions with other enemies
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detecionRadius);
        foreach (Collider2D enemy in nearbyEnemies)
        {
            // Changes the direction if it detects another enemy
            if (enemy.gameObject != this.gameObject)
            {
                Vector2 avoidanceDirection = (Vector2)(transform.position - enemy.transform.position).normalized;
                direction = (direction + avoidanceDirection).normalized;
                break;
            }
        }

        Vector3 position = transform.position;
        // restricts enemies from exceeding established limits 
        if (position.x <= boundsNegativeX || position.x >= boundsX)
        {
            direction.x *= -1;
            position.x = Mathf.Clamp(position.x, boundsNegativeX, boundsX);
        }

        if (position.y <= boundsNegativeY || position.y >= boundsY)
        {
            direction.y *= -1;
            position.y = Mathf.Clamp(position.y, boundsNegativeY, boundsY);
        }

        transform.position = position;

        // After X seconds the enemy changes the direction
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomDirection();
            timer = changeDirectionInterval;
        }

    }

    // Generate a random drection
    private void SetRandomDirection(){
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        direction = new Vector2(randomX, randomY).normalized;
    }

    public void TakeDamage(int damage)
    {
        lifePoints -= damage;

        if (lifePoints <= 0)
        {
            Die();
            MusicManager.Instance.PlaySFX(deathClip);
            // Instantiate the death particle system
            if (EffectOnDestroyPrefab)
            {
                Instantiate(EffectOnDestroyPrefab, transform.position + particleOffset, Quaternion.identity);
            }
        }
    }

    private void Die()
    {
        enemySpawner spawner = FindObjectOfType<enemySpawner>();
        if (spawner != null)
        {
            spawner.EnemyDestroyed(gameObject);
        }
        TryDropItem();
        Destroy(gameObject);
    }

    // Function to drop the powerup
    private void TryDropItem()
    {
        // If it has been dropped, no enemy will drop more powerups
        if (GameManager.Instance.hasDroppedPowerUpOnce) 
        {
            return;
        }

        float randomValue = Random.Range(0f, 1f);

        // if "randomValue" is lower or equal to the drop chance, drops the powerup and the bool "hasDroppedPowerUpOnce" turns true
        if (randomValue <= dropChance)
        {

            Instantiate(powerup1Prefab, transform.position, Quaternion.identity);

           GameManager.Instance.hasDroppedPowerUpOnce = true;

        }
        else
        {
        }

    }

}
