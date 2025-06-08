using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Vacuum : MonoBehaviour
{
    public float suctionPower = 5f;
    public float maxSuctionTime = 10f;
    public float cooldownMultiplier = 0.5f;

    public bool isSucking = false;
    public Transform targetTrans;

    public GameObject WindEffe;

    private float suctionTimer = 0f;
    private float cooldownTimer = 0f;

    public ActionBasedController controllerScr;

    public AudioClip onSound;
    public AudioClip offSound;
    public float Volume = 0.5f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            if (isSucking)
            {
                suctionTimer += Time.deltaTime;
                controllerScr.SendHapticImpulse(0.3f, 0.01f);

                if (suctionTimer >= maxSuctionTime)
                {
                    StopSuction();
                }
            }
        }

        audioSource.volume = Volume * GameManager.MasterVolume * GameManager.PlayerVolume;
    }

    private void StartSuction()
    {
        isSucking = true;
        suctionTimer = 0f;
        cooldownTimer = 0f;
        WindEffe.SetActive(true);
        controllerScr.SendHapticImpulse(0.8f,0.1f);

        audioSource.Stop();
        audioSource.clip = onSound;
        audioSource.Play();
    }

    private void StopSuction()
    {
        isSucking = false;
        cooldownTimer = suctionTimer * cooldownMultiplier;
        suctionTimer = 0f;
        WindEffe.SetActive(false);
        controllerScr.SendHapticImpulse(0.8f, 0.3f);

        audioSource.Stop();
        audioSource.clip = offSound;
        audioSource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost") && isSucking)
        {
            Ghost ghost = other.gameObject.GetComponent<Ghost>();
            Rigidbody ghostRigid = other.gameObject.GetComponent<Rigidbody>();
            float newsuctionPower = suctionPower;

            if (ghost.Ghost_hp <= 0)
            {
                targetTrans.position = WindEffe.transform.position;
                newsuctionPower = suctionPower * 2;
            }
            Vector3 suctionDirection = (targetTrans.position - other.transform.position).normalized;
            float suctionForce = newsuctionPower * (1 - ghost.Durability);

            ghostRigid.AddForce(suctionDirection * suctionForce - ghostRigid.velocity, ForceMode.Acceleration);
        }
    }

    public void PushVacuumOnOffButton(InputAction.CallbackContext context)
    {
        if (context.started && !isSucking)
        {
            StartSuction();
        }

        if (context.canceled && isSucking)
        {
            StopSuction();
        }
    }

    public void killHaptics()
    {
        controllerScr.SendHapticImpulse(0.5f, 0.3f);
    }
}
