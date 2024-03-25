using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycardspawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objectsToDisable;

    void Start()
    {
        // Shuffle the array
        ShuffleArray(objectsToDisable);

        // Disable the first three objects in the shuffled array
        for (int i = 0; i < 6; i++)
        {
            objectsToDisable[i].SetActive(false);
        }
    }

    // Fisher-Yates shuffle algorithm to shuffle the array
    void ShuffleArray(GameObject[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
