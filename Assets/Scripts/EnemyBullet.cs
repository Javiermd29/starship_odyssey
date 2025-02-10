using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        // Ignorar colisiones entre balas
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
        // Se destruye el objeto si supera los limites
        if (transform.position.x < -37f)
        {
            Destroy(gameObject);
        }

    }

    // El jugador recibe daño si el objeto entra en contacto con el jugador
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
         // Destruye el objeto si ha colisionado con el jugador
        Destroy(gameObject);

    }
}
