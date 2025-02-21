using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] public GameObject normalBulletPrefab;
    [SerializeField] public GameObject powerUpBulletPrefab;

    [SerializeField] private AudioClip shootClip;

    void Update()
    {
        // Shoot the player bullet is 
        if (Input.GetButtonDown("Fire1") && UIManager.Instance.isPaused == false)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // If players has the powerup, instantiate one type of bullet. It plays the shoot sound
        if (GameManager.Instance.hasPowerUp)
        {
            Instantiate(powerUpBulletPrefab, firePoint.position, firePoint.rotation);
            MusicManager.Instance.PlaySFX(shootClip);
        }
        // If player doesn't have the powerup, instantiate the type of bullet. It plays the shoot sound
        else
        {
            Instantiate(normalBulletPrefab, firePoint.position, firePoint.rotation);
            MusicManager.Instance.PlaySFX(shootClip);
        }
    }
    
    // This is used for the game to know if the player has the poweruup active
    public void ActivatePowerUp()
    {
        GameManager.Instance.hasPowerUp = true;
    }

}
