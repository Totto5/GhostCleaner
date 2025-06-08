using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public OpenDoorDouble openDoorDouble;

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
            if (openDoorDouble.doorOpen == false)
            {
                openDoorDouble.doorOpen = true;
                other.gameObject.GetComponent<DoorHaptics>().TriggerHaptics();
            }
        }
    }
}
