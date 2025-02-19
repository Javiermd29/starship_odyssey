using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    [SerializeField] public GameObject normalBulletPrefab;
    [SerializeField] public GameObject powerUpBulletPrefab;

    [SerializeField] private AudioClip shootClip;

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && UIManager.Instance.isPaused == false)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        if (GameManager.Instance.hasPowerUp)
        {
            Instantiate(powerUpBulletPrefab, firePoint.position, firePoint.rotation);
            MusicManager.Instance.PlaySFX(shootClip);
        }
        else
        {
            Instantiate(normalBulletPrefab, firePoint.position, firePoint.rotation);
            MusicManager.Instance.PlaySFX(shootClip);
        }
        

    }

    public void ActivatePowerUp()
    {
        GameManager.Instance.hasPowerUp = true;
    }

}
