using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    private float horizontal;
    private float vertical;

    // Player boundaries
    private float boundsX = -30f;
    private float boundsNegativeX = -3f;
    private float boundsY = 12.5f;
    private float boundsNegativeY = -18f;

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int lifePoints;

    [SerializeField] private AudioClip playerDeathClip;

    // Postprocess Volume settings
    [SerializeField] private PostProcessVolume postProcessVolume;
    private Vignette vignette; // Vignette effect
    private float maxVignetteIntensity = 0.6f; // Max vignette intensity when getting hit
    private float normalVignetteIntensity = 0; // Normal vignette intensty
    private float vignetteFadeSpeed = 1.5f; // Speed of fade

    private void Start()
    {
        transform.position = new Vector2(-30, -2);

        if (postProcessVolume != null )
        {
            postProcessVolume.profile.TryGetSettings(out vignette);
            vignette.intensity.overrideState = true; // Enable intensity editing
            vignette.intensity.value = normalVignetteIntensity; // Initial value
        }
        else
        {
            Debug.LogError("No se encontró un Volume en la escena.");
        }
    }

    private void Update()
    {
        // Get player input for movement and apply speed
        horizontal = Input.GetAxis("Horizontal") * speed;
        vertical = Input.GetAxis("Vertical") * speed;

        // Player boundaries to prevent movement outside the allowed area
        if (transform.position.y >= boundsY)
        {
            transform.position = new Vector2(transform.position.x, 12.5f);
        }
        else if (transform.position.y <= boundsNegativeY)
        {
            transform.position = new Vector2(transform.position.x, -18f);
        }

        if (transform.position.x <= boundsX)
        {
            transform.position = new Vector2(-30f, transform.position.y);
        }
        else if (transform.position.x >= boundsNegativeX)
        {
            transform.position = new Vector2(-3f, transform.position.y);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical);
    }

    public void TakeDamege(int damage)
    {
        lifePoints -= damage;

        if (vignette != null)
        {
            vignette.intensity.value = maxVignetteIntensity;
        }

        StartCoroutine(ReduceVignetteEffect());

        if (lifePoints <= 0)
        {
            Destroy(gameObject);
            MusicManager.Instance.PlaySFX(playerDeathClip); // Play the player death sound effect
            UIManager.Instance.LosePanel(); // Display the  "Lose" panel in the UI
        }

    }
    private IEnumerator ReduceVignetteEffect()
    {
        // This loop continues to execute while the vignette intensity is greater than the normal intensity value.
        while (vignette.intensity.value > normalVignetteIntensity)
        {
            vignette.intensity.value -= Time.deltaTime * vignetteFadeSpeed; // Gradually decreases the vignette intensity
            yield return null;
        }
        vignette.intensity.value = normalVignetteIntensity; // Ensures the vignette intensity is exactly set to the normal intensity
    }
}
