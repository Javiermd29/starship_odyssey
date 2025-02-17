using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //Movimiento aleatorio del jefe
    private float speed = 4f; // Velocidad de movimiento
    private float changeDirectionInterval = 3f; // Intervalo para cambiar de direccion
    private Vector2 direction; // Direccion actual del movimiento

    // Limites del movimiento de los enemigos
    private float boundsX = 30f;
    private float boundsNegativeX = 2f;
    private float boundsY = 12.5f;
    private float boundsNegativeY = -18f;
    private float timer;

    private int lifePoints = 50;

    // Límites de la pantalla
    private Vector2 screenBounds;

    void Start()
    {
        // Determinar los límites de la pantalla (en unidades del mundo)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {

        // Mueve al enemigo en la direccion actual
        transform.Translate(direction * speed * Time.deltaTime);

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
    private void SetRandomDirection()
    {
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
            Destroy(gameObject);
        }

    }

}
