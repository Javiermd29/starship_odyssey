using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
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

                player.TakeDamege(2);

            }


        }
        // Destruye el objeto si ha colisionado con el jugador
        Destroy(gameObject);

    }

}
