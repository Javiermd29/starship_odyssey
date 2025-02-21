using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{

    [SerializeField] private Transform firePoint;

    [SerializeField] public GameObject bossBulletPrefab;

    [SerializeField] private float shootInterval = 2;

    private Transform playerPosition;

    [SerializeField] private AudioClip bossShootClip;

    void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

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
            // Coroutine to shoot every X seconds
            yield return new WaitForSeconds(shootInterval);

            if (playerPosition != null)
            {
                Shoot();
            }

        }
    }

    private void Shoot()
    {

        if (firePoint != null && bossBulletPrefab != null && playerPosition != null)
        {
            // Instance of the enemy bullet
            GameObject enemyBullet = Instantiate(bossBulletPrefab, firePoint.position, firePoint.rotation);
            MusicManager.Instance.PlaySFX(bossShootClip);

            // Detects the player position
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
