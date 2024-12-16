using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    public float tilt = 5f;

    private ParticleSystem trailParticles;  // Reference to the Particle System

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;

        // Get the Particle System component
        trailParticles = GetComponentInChildren<ParticleSystem>();
        if (trailParticles != null)
        {
            var main = trailParticles.main;
            main.loop = false;  // Set loop to false to control emission manually
            trailParticles.Stop();  // Initially, stop the particles
        }
    }

    private void Update()
    {
        // Keyboard or mouse input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;

            // Start the trail particles when the player jumps
            if (trailParticles != null)
            {
                trailParticles.Play();
            }
        }

        // Touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;

                // Start the trail particles when the player jumps
                if (trailParticles != null)
                {
                    trailParticles.Play();
                }
            }
        }

        // Apply gravity over time
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;

        // Stop the particles when the player is falling
        if (trailParticles != null && direction.y <= 0)
        {
            trailParticles.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacles") {
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "Scoring") {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
