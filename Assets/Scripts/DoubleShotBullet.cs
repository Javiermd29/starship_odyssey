using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoubleShotBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        // Destroys the object if it surpasss the limits
        if (transform.position.x > 35f)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Boss boss = other.GetComponent<Boss>();

        if (other.CompareTag("Enemy"))
        {
            // If the bullet hits an enemy, it takes 2 damage
            if (enemy != null)
            {
                enemy.TakeDamage(2);
            }
            // If the bullet hits the boss, it takes 2 damage
            else if (boss != null) 
            {
                boss.TakeDamage(2);
            }

            Destroy(gameObject);

        }
    }
}
