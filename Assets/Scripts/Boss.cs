using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float speed = 4f;
    private float changeDirectionInterval = 3f; // Interval to change direction
    private Vector2 direction; // Current direction of movement

    // Boss movement boudnaries
    private float boundsX = 29f;
    private float boundsNegativeX = 7.6f;
    private float boundsY = 10f;
    private float boundsNegativeY = -15.8f;
    private float timer;

    private int lifePoints = 50;

    [SerializeField] private AudioClip bossDeathClip;

    private Vector2 screenBounds;

    void Start()
    {
        // Determine the boundaries of the screen (in world units)
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    void Update()
    {

        // Move the enemy in the current direction
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 position = transform.position;

        if (position.x <= boundsNegativeX || position.x >= boundsX)
        {
            direction.x *= -1;
            position.x = Mathf.Clamp(position.x, boundsNegativeX, boundsX);
        }

        if (position.y <= boundsNegativeY || position.y >= boundsY)
        {
            direction.y *= -1;
            position.y = Mathf.Clamp(position.y, boundsNegativeY, boundsY);
        }

        transform.position = position;

        // Check if the timer has reached zero or below and sets a new random direction for the object
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SetRandomDirection();
            // Reset the timer to the predefined interval before changing direction again
            timer = changeDirectionInterval;
        }

    }

    // Generate a random direction
    private void SetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        direction = new Vector2(randomX, randomY).normalized;
    }

    public void TakeDamage(int damage)
    {
        lifePoints -= damage;

        // Check if the boss's life points have reached zero or below
        if (lifePoints <= 0)
        {
            Destroy(gameObject);
            MusicManager.Instance.PlaySFX(bossDeathClip); // Play the boss death sound effect
            UIManager.Instance.WinPanel(); // Display the "Win" panel in the UI
        }

    }

}
