using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheakInPlayer : MonoBehaviour
{
    public bool InPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InPlayer = true;
        }
    }

    void OnTriggerEExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InPlayer = false;
        }
    }
}
