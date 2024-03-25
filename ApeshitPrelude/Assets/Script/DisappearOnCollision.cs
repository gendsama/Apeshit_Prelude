using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
{
    // Array of game objects that can trigger disappearance
    public GameObject[] triggerObjects;

    // AudioSource component for playing sounds
    public AudioSource audioSource;

    // AudioClip for the sound to play on collision
    public AudioClip collisionSound;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with is in our array
        foreach (GameObject triggerObject in triggerObjects)
        {
            if (collision.gameObject == triggerObject)
            {
                // Play the collision sound
                audioSource.PlayOneShot(collisionSound);

                // Delay the disappearance of the object by 1 seconds
                StartCoroutine(DisappearAfterSeconds(1));
            }
        }
    }

    IEnumerator DisappearAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Disable this object
        this.gameObject.SetActive(false);
    }
}
