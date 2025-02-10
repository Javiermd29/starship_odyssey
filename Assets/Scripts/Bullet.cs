using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void Update()
    {

        if (transform.position.x > 35f)
        {
            Destroy(gameObject);
        }

    }

    // Detecta la colision del objeto con un enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (other.CompareTag("Enemy"))
        { 

            if (enemy != null)
            {

                enemy.TakeDamege(1);

            }

            // El objeto se destruye si colisiona con el enemigo
            Destroy(gameObject);

        }
    }

}
