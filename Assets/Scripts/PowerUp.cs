using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private Rigidbody2D powerRb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Player player = GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

}
