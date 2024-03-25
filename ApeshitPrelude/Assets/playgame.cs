using UnityEngine;
using UnityEngine.SceneManagement;

public class dplaygame : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player or any other object you want to trigger the scene change
        if (other.CompareTag("GameController"))
        {
            // Load the specified scene
            SceneManager.LoadScene("apeshitprelude");
        }
    }
}

