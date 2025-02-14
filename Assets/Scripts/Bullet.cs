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
        Boss boss = other.GetComponent<Boss>();

        if (other.CompareTag("Enemy"))
        { 

            if (enemy != null)
            {

                enemy.TakeDamage(1);

            }

            else if (boss != null) // Si el objeto tiene el script Boss
            {
                boss.TakeDamage(1);

            }

            Destroy(gameObject);

        }
        

    }

}
