using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public TutoGameMane gameMane;

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
            other.gameObject.GetComponent<DoorHaptics>().TriggerHaptics();
            gameMane.TutoEnd();
        }
    }
}
