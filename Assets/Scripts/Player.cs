using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontal;
    private float vertical;

    //bound
    private float boundsX = 30f;
    private float boundsY = 12.5f;
    private float boundsNegativeY = -18f;

    [SerializeField] private float speed;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int lifePoints;


    private void Start()
    {
        transform.position = new Vector2(-30, -2);
    }

    void Update()
    {

        horizontal = Input.GetAxis("Horizontal") * speed;
        vertical = Input.GetAxis("Vertical") * speed;

        //player boundaries
        if (transform.position.y >= boundsY)
        {
            transform.position = new Vector2(transform.position.x, 12.5f);
        }
        else if (transform.position.y <= boundsNegativeY)
        {
            transform.position = new Vector2(transform.position.x, -18f);
        }

        if (transform.position.x >= boundsX)
        {
            transform.position = new Vector2(30f, transform.position.y);
        }
        else if (transform.position.x <= -boundsX)
        {
            transform.position = new Vector2(-30f, transform.position.y);
        }

    }

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(horizontal, vertical);

    }

    public void TakeDamege(int damage)
    {
        lifePoints -= damage;

        if (lifePoints <= 0)
        {
            Destroy(gameObject);
        }

    }

}
