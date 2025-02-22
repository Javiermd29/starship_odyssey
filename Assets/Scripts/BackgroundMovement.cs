using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Velocidad de movimiento del fondo
    public float speed = 4f;

    void Update()
    {
        // Move background to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -5f)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        // Reposition the background to appear infinite
        transform.position += new Vector3(40f, 0f, 0f);
    }
}
