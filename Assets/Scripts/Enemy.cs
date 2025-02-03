using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Movimiento aleatoria de los enemigos
    private float speed = 4f; // Velocidad de movimiento
    private float changeDirectionInterval = 2f; //Intervalo para cambiar de direccion
    private Vector2 direction; //Direccion actual del movimiento
    private float timer;

    private float detecionRadius = 2f;

    //bounds
    private float boundsX = 30f;
    private float boundsNegativeX = 2f;
    private float boundsY = 12.5f;
    private float boundsNegativeY = -18f;

    private Vector2 screenBounds; // Límites de la pantalla
    [SerializeField] private int lifePoints;

    public GameObject powerup1Prefab;
    //public GameObject poweup2Prefab;
    public float dropChance = 0.2f;
    public float dropChance2 = 0.2f;

    void Start()
    {
        // Determinar los límites de la pantalla (en unidades del mundo)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        timer = changeDirectionInterval;
    }

    void Update()
    {
        //Mueve al enemigo en la direccion actual
        transform.Translate(direction * speed * Time.deltaTime);

        //Detecta colisiones con otros enemigos
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

     private void SetRandomDirection(){
        //Genera una direccion aleatoria
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        direction = new Vector2(randomX, randomY).normalized;
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
            //Instantiate(ppowerup2Prefab, transform.position, Queternion.identity);
            Debug.Log("objeto dropeado");

        }
        else
        {
            Debug.Log("objeto no dropeado");
        }
    }

}
