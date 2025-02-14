using System.Collections;
using System.Collections.Generic;
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

            if (enemy != null)
            {

                enemy.TakeDamage(3);

            }

            else if (boss != null) // Si el objeto tiene el script Boss
            {
                boss.TakeDamage(3);

            }

            Destroy(gameObject);

        }
    }
}
