using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

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
                player.TakeDamege(2);
            }
        }
        // Destroys the bullet if it has collided with the player
        Destroy(gameObject);
    }

}
