using UnityEngine;

public class back : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject creditObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            if (menuObject != null)
            {
                menuObject.SetActive(true);
            }

            if (creditObject != null)
            {
                creditObject.SetActive(false);
            }
        }
    }
}



