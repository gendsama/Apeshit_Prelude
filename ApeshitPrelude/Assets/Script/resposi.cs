using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class resposi : MonoBehaviour
{
    public bool ded;
    public Transform spw;
    public Transform head;
    public Transform ori;
    //public GameObject xrRig;  // Drag your XR Rig GameObject here in Inspector
    // Start is called before the first frame update
    void Start()
    {
        ded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ded)
        {
            TeleportToPosition();
        }   
    }
    public void TeleportToPosition()
    {

        Vector3 ofst = head.position - ori.position;
        ori.position = spw.position - ofst;

        ded = false;
    }

}
