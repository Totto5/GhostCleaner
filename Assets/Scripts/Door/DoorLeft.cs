using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLeft : MonoBehaviour
{
    public OpenDoor openDoor;

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
        if (other.CompareTag("Hand"))
        {
            if (openDoor.leftOpen == false && openDoor.rightOpen == false)
            {
                openDoor.leftOpen = true;
                other.gameObject.GetComponent<DoorHaptics>().TriggerHaptics();
            }
        }
    }
}
