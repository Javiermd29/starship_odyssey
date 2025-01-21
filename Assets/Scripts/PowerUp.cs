using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private Rigidbody2D powerRb;

    private Image doubleShot;
    [SerializeField] private Sprite doubleShotActive;

    private void Start()
    {
        doubleShot = GameObject.Find("Doble Shot").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Player player = GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            doubleShot.sprite = doubleShotActive;


            Destroy(gameObject);
        }

    }

}
