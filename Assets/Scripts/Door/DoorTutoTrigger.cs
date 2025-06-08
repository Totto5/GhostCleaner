using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTutoTrigger : MonoBehaviour
{
    public OpenTutoDoor openTutoDoor;

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
            if (openTutoDoor.doorOpen == false)
            {
                openTutoDoor.doorOpen = true;
                other.gameObject.GetComponent<DoorHaptics>().TriggerHaptics();
            }
        }
    }
}
