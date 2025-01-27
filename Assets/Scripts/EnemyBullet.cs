using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {

        rb.velocity = transform.right * bulletSpeed;

    }

    private void Update()
    {

        if (transform.position.x > -37f)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player"))
        {

            if (player != null)
            {

                player.TakeDamege(1);

            }

            Destroy(gameObject);

        }
    }
}
