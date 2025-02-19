using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private Rigidbody2D powerRb;
    private Transform player;
    [SerializeField] private float attractionSpeed = 5f;
    [SerializeField] private Sprite doubleShotActive;

    [SerializeField] private AudioClip powerUpPickUpClip;

    private void Start()
    {

        // Encontrar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

    }

    private void Update()
    {
        if (player != null)
        {
            // Calcula la direccion hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;

            // Mueve el Power-Up hacia el jugador
            powerRb.velocity = direction * attractionSpeed;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Player player = GetComponent<Player>();

        if (other.CompareTag("Player"))
        {

            Weapon weapon = other.GetComponent<Weapon>();

            if (weapon != null)
            {
                weapon.ActivatePowerUp();
                MusicManager.Instance.PlaySFX(powerUpPickUpClip);
            }

            Destroy(gameObject);

        }

    }

}
