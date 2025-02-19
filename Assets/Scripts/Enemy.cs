using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{

    //Movimiento aleatorio de los enemigos
    private float speed = 4f; // Velocidad de movimiento
    private float changeDirectionInterval = 2f; // Intervalo para cambiar de direccion
    private Vector2 direction; // Direccion actual del movimiento
    private float timer;

    private float detecionRadius = 2f;

    // Limites del movimiento de los enemigos
    private float boundsX = 30f;
    private float boundsNegativeX = 2f;
    private float boundsY = 12.5f;
    private float boundsNegativeY = -18f;

    // Límites de la pantalla
    private Vector2 screenBounds;
    [SerializeField] private int lifePoints;

    public GameObject powerup1Prefab;
    public float dropChance = 0.2f;

    [SerializeField] private GameObject EffectOnDestroyPrefab;

    [SerializeField] private AudioClip deathClip;

    void Start()
    {
        // Determinar los límites de la pantalla (en unidades del mundo)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        timer = changeDirectionInterval;
    }

    void Update()
    {
        // Mueve al enemigo en la direccion actual
        transform.Translate(direction * speed * Time.deltaTime);

        // Detecta colisiones con otros enemigos
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detecionRadius);
        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.gameObject != this.gameObject)
            {

                Vector2 avoidanceDirection = (Vector2)(transform.position - enemy.transform.position).normalized;
                direction = (direction + avoidanceDirection).normalized;
                break;

            }
        }

        Vector3 position = transform.position;

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

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SetRandomDirection();
            timer = changeDirectionInterval;
        }

    }

    // Genera una direccion aleatoria
    private void SetRandomDirection(){
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        direction = new Vector2(randomX, randomY).normalized;
    }

    public void TakeDamage(int damage)
    {
        lifePoints -= damage;
        Debug.Log("Enemy vida restante: " + lifePoints);

        if (lifePoints <= 0)
        {
            Die();
            MusicManager.Instance.PlaySFX(deathClip);
            if (EffectOnDestroyPrefab)
            {
                Instantiate(EffectOnDestroyPrefab, transform.position, Quaternion.identity);
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

    private void TryDropItem()
    {

        if (GameManager.Instance.hasDroppedPowerUpOnce) // Si ya dropeó, no vuelve a hacerlo
        {
            Debug.Log("Este enemigo ya dropeó un Power-Up antes. No se dropearán más");
            return;
        }

        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= dropChance)
        {

            Instantiate(powerup1Prefab, transform.position, Quaternion.identity);
            Debug.Log("objeto dropeado");

           GameManager.Instance.hasDroppedPowerUpOnce = true;

        }
        else
        {
            Debug.Log("objeto no dropeado");
        }



    }

}
