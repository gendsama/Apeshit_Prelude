using UnityEngine;

public class quitgame : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            Application.Quit();
        }
    }
}