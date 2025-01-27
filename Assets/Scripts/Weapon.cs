using System.Collections;
using System.Collections.Generic;
using UnityEngine;using Image = UnityEngine.UI.Image;


public class Weapon : MonoBehaviour
{

    [SerializeField] private Transform firePoint;

    [SerializeField] public GameObject normalBulletPrefab;
    [SerializeField] public GameObject powerUpBulletPrefab;

    private bool hasPowerUp = false;

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        if (hasPowerUp)
        {
            Instantiate(powerUpBulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Instantiate(normalBulletPrefab, firePoint.position, firePoint.rotation);
        }
        

    }

    public void ActivatePowerUp()
    {
        hasPowerUp = true;
    }

}
