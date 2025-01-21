using System.Collections;
using System.Collections.Generic;
using UnityEngine;using Image = UnityEngine.UI.Image;


public class Weapon : MonoBehaviour
{

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;

    /*private Image doubleShot;
    [SerializeField] private Sprite doubleShotActive;*/



    /*private void Start()
    {
        doubleShot = GameObject.Find("Doble Shot").GetComponent<Image>();
    }*/

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        /*if (doubleShot.sprite = doubleShotActive)
        {
            Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        }*/

    }

}
