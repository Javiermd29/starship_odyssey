using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        // Ignore collisions between bullets
        Collider2D enemyCollider = GetComponent<Collider2D>();
        Collider2D[] enemyBullets = FindObjectsOfType<Collider2D>();

        foreach (Collider2D otherCollider in enemyBullets)
        {
            if (otherCollider.CompareTag("EnemyBullet") && otherCollider != enemyCollider)
            {
                Physics2D.IgnoreCollision(enemyCollider, otherCollider);
            }
        }
    }

    private void Update()
    {
        // The object is destroyed if it exceeds the limits
        if (transform.position.x < -37f)
        {
            Destroy(gameObject);
        }

    }

    // The player takes damage if the object comes into contact with the player
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player"))
        {

            if (player != null)
            {

                player.TakeDamege(1);

            }


        }
        // Destroys the object if it has collided with the player
        Destroy(gameObject);

    }
}
