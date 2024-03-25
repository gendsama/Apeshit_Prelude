using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public int card;
    // Start is called before the first frame update
    void Start()
    {
        card = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (card == 3)
        {
            Destroy(gameObject);
        }
    }
}
