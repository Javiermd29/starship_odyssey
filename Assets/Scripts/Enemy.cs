using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 5f; // Velocidad de movimiento
    private Vector2 screenBounds; // Límites de la pantalla
    private int lifePoints = 3;

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

}
