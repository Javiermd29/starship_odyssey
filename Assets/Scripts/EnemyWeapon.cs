using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] public GameObject enemyBulletPrefab;

    [SerializeField] private float shootInterval = 2;

    private Transform playerPosition;
    [SerializeField] private AudioClip enemyShootClip;

    private void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // It detects the player position
        if (playerObject != null)
        {
            playerPosition = playerObject.transform;
        }

        StartCoroutine(ShootAtPlayer());

    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            // it shoots the player every X seconds
            if (playerPosition != null)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (firePoint != null && enemyBulletPrefab != null && playerPosition != null)
        {
            // Instantiate the bullet prefab and plays the shoot sound
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
            MusicManager.Instance.PlaySFX(enemyShootClip);

            Vector2 direction = (playerPosition.position - firePoint.position).normalized;

            Rigidbody2D rb = enemyBullet.GetComponent<Rigidbody2D>();

            // Rotate the bullet to the player position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemyBullet.transform.rotation = Quaternion.Euler(0, 0, angle + 180f);

            if (rb != null)
            {
                rb.velocity = direction * 10f;
            }
        }
    }
}
