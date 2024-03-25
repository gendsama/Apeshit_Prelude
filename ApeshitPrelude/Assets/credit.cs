using UnityEngine;

public class credit : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject creditObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            if (menuObject != null)
            {
                menuObject.SetActive(false);
            }

            if (creditObject != null)
            {
                creditObject.SetActive(true);
            }
        }
    }
}



