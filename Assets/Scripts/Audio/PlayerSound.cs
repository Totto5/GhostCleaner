using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip FootSound;
    public float footVolume = 0.5f;
    public AudioClip DamageSound;
    public float damageVolume = 0.5f;
    private AudioSource audioSource;

    private Vector3 lastTrans;
    public Transform PlayerTrans;
    public float coolTime = 0.8f;
    private float lastTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTime < Time.time - coolTime)
        {
            if(Vector3.Distance(lastTrans, PlayerTrans.position) != 0)
            {
                audioSource.pitch = 1.0f + Random.Range(-0.5f, 0.5f);
                audioSource.PlayOneShot(FootSound, GameManager.MasterVolume * GameManager.PlayerVolume * footVolume);

                lastTime = Time.time;
                lastTrans = PlayerTrans.position;
            }
        }
    }

    public void DamageAudio()
    {
        audioSource.pitch = 1.5f;
        if (DamageSound != null)
        {
            audioSource.PlayOneShot(DamageSound, GameManager.MasterVolume * GameManager.PlayerVolume * damageVolume);
        }
    }
}
