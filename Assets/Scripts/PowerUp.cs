using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private Rigidbody2D powerRb;

    [SerializeField] private SpriteRenderer doubleShotNo;
    [SerializeField] private SpriteRenderer doubleShot;

    private void Start()
    {
        doubleShotNo = this.gameObject.GetComponent<SpriteRenderer>();
        doubleShot= this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Player player = GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            doubleShotNo.enabled = false;
            doubleShot.enabled = true;


            Destroy(gameObject);
        }

    }

}
