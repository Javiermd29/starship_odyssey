using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 5f; // Velocidad de movimiento
    private Vector2 screenBounds; // Límites de la pantalla
    private int lifePoints = 3;

    public GameObject powerup1Prefab;
    public float dropChance = 0.2f;

    void Start()
    {
        // Determinar los límites de la pantalla (en unidades del mundo)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Update()
    {
        
    }

    public void TakeDamege(int damage)
    {
        lifePoints -= damage;
        Debug.Log("Enemy vida restante: " + lifePoints);

        if (lifePoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Ship2 destruido");
        }

    }

    private void OnDestroy()
    {
        TryDropItem();
    }

    private void TryDropItem()
    {

        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= dropChance)
        {

            Instantiate(powerup1Prefab, transform.position, Quaternion.identity);
            Debug.Log("objeto dropeado");

        }
        else
        {
            Debug.Log("objeto no dropeado");
        }
    }

}
