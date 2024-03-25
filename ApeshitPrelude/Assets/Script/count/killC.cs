using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class killC : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public int kc = 0;

    // Start is called before the first frame update
    void Start()
    {
        txt = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = kc.ToString();
    }
}
