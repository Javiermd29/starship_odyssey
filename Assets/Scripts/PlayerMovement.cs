using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float vertical;
    [SerializeField] private float speed;

    [SerializeField] private Rigidbody2D rb;


    private void Start()
    {
        transform.position = new Vector2(-30, -2);
    }

    void Update()
    {

        horizontal = Input.GetAxis("Horizontal") * speed;
        vertical = Input.GetAxis("Vertical") * speed;

        //player boundaries
        if (transform.position.y >= 12.5f)
        {
            transform.position = new Vector2(transform.position.x, 12.5f);
        }
        else if (transform.position.y <= -18f)
        {
            transform.position = new Vector2(transform.position.x, -18f);
        }

        if (transform.position.x >= 30f)
        {
            transform.position = new Vector2(30f, transform.position.y);
        }
        else if (transform.position.x <= -30f)
        {
            transform.position = new Vector2(-30f, transform.position.y);
        }

    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(horizontal, vertical);

    }
}
