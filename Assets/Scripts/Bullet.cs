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

    // Detects the collision of the object with an enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Boss boss = other.GetComponent<Boss>();

        // Check if the collided object has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // If the object is an enemy, apply damage
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
            // If the object is a boss, apply damage
            else if (boss != null)
            {
                boss.TakeDamage(1);
            }
            // Destroy the bullet
            Destroy(gameObject);

        }
        

    }

}
