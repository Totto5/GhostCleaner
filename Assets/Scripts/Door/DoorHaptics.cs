using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHaptics : MonoBehaviour
{
    public ActionBasedController controllerScr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerHaptics()
    {
        controllerScr.SendHapticImpulse(0.4f, 0.1f);
    }
}
