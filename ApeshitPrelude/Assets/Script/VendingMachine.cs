using System.Collections;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public Vector3 launchDirection;
    public float launchForce = 10f;
    public float spawnCooldown = 2f;
    public AudioClip triggerSound; // Sound to play when triggered
    private AudioSource audioSource; // Reference to AudioSource component
    private bool canSpawn = true;

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        // Check if AudioSource component exists
        if (audioSource == null)
        {
            // If AudioSource is not found, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canSpawn && other.CompareTag("Player"))
        {
            // Play the trigger sound
            if (triggerSound != null)
            {
                audioSource.PlayOneShot(triggerSound);
            }
            SpawnPrefab();
            StartCoroutine(StartCooldown());
        }
    }

    private void SpawnPrefab()
    {
        GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        Rigidbody prefabRigidbody = spawnedPrefab.GetComponent<Rigidbody>();
        if (prefabRigidbody != null)
        {
            prefabRigidbody.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Prefab doesn't have a Rigidbody component!");
        }
    }

    private IEnumerator StartCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}

