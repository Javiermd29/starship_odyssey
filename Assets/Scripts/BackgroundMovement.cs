using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del fondo

    void Update()
    {
        // Mover el fondo hacia la izquierda
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Si el fondo sale de la pantalla, puedes reposicionarlo (solo para fondos infinitos)
        if (transform.position.x < -5f) // Ajusta este valor según el tamaño del fondo
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        // Reposiciona el fondo en un punto adecuado para que parezca infinito
        transform.position += new Vector3(40f, 0f, 0f); // Ajusta según el ancho de tu fondo
    }
}
