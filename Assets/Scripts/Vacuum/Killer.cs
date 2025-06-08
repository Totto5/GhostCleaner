using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    public Vacuum vacuumScr;
    public float Attackness = 1;
    public float attackInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost") && vacuumScr.isSucking)
        {
            Ghost ghost = other.gameObject.GetComponent<Ghost>();

            if (Time.time - ghost.lastAttackedTime >= attackInterval)
            {
                ghost.Ghost_hp -= Attackness;
                if(ghost.Ghost_hp <= 0)
                {
                    vacuumScr.killHaptics();
                }
                ghost.lastAttackedTime = Time.time;
            }
        }
    }
}
