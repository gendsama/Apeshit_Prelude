using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColor : MonoBehaviour
{
    public float speed = 1.0f; // Speed of color change
    public AudioClip destroySound; // Sound to play when destroyed
    public door door;

    private Material material;
    private AudioSource audioSource;
    private float hue = 0.0f;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        // Assuming your object has a Renderer component with a material
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();

        // Get the BoxCollider component
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Increment hue over time
        hue += speed * Time.deltaTime;
        if (hue > 1.0f)
        {
            hue -= 1.0f; // Wrap around
        }

        // Convert hue to RGB
        Color color = Color.HSVToRGB(hue, 1.0f, 1.0f);

        // Apply the new color to the material
        material.color = color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Play the destroy sound
            if (destroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            // Disable the MeshRenderer
            meshRenderer.enabled = false;

            // Disable the BoxCollider
            boxCollider.enabled = false;

            // Delay the destruction of the GameObject
            Invoke("DestroyGameObject", 1.0f);
        }
    }

    void DestroyGameObject()
    {
        // Destroy the GameObject
        door.card++;
        Destroy(gameObject);
    }
}





