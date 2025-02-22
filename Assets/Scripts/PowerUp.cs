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

        // Find the player
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
            // Calculate the direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Moves the Power-Up towards the player like a magnet
            powerRb.velocity = direction * attractionSpeed;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Player player = GetComponent<Player>();

        if (other.CompareTag("Player")) // Check if the collided object has the "Player" tag
        {
            Weapon weapon = other.GetComponent<Weapon>();

            if (weapon != null)
            {
                weapon.ActivatePowerUp(); // Activates the double shot
                MusicManager.Instance.PlaySFX(powerUpPickUpClip); // Play the power-up pickup sound effect
            }

            Destroy(gameObject); // Destroy the power-up object after it has been collected

        }

    }

}
