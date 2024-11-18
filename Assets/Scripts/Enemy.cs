using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public Vector2 screenBounds; // Límites de la pantalla

    void Start()
    {
        // Determinar los límites de la pantalla (en unidades del mundo)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Update()
    {
        // Generar movimiento aleatorio
        float dx = Random.Range(-1f, 1f) * speed * Time.deltaTime;
        float dy = Random.Range(-1f, 1f) * speed * Time.deltaTime;

        // Actualizar la posición
        Vector3 newPosition = transform.position + new Vector3(dx, dy, 0);

        // Mantener dentro de los límites
        newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x, screenBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y, screenBounds.y);

        transform.position = newPosition;
    }
}
