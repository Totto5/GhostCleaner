using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTuto : MonoBehaviour
{
    public Tips tips17;
    public Tips tips18;
    public Tips tips19;

    public AudioClip tipsSound;
    public float tipsVolume = 0.5f;

    public AudioClip BreakSound;
    public float BreakVolume = 0.5f;

    private AudioSource audioSource;

    private bool Tip18End = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        Tip17UP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Tip17UP()
    {
        tips17.TipsUp();
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        Invoke("Tip17DOWN", 10);
    }

    private void Tip17DOWN()
    {
        tips17.TipsDown();
    }

    private void Tip18UP()
    {
        tips18.TipsUp();
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        Invoke("Tip18DOWN", 5);
    }

    private void Tip18DOWN()
    {
        tips18.TipsDown();
    }

    void OnTriggerEnter(Collider other)
    {
        if (Tip18End == false)
        {
            if (other.CompareTag("Player"))
            {
                Tip18UP();
                Tip18End = true;
            }
        }
    }

    public void Tip19UP()
    {
        tips19.TipsUp();
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        audioSource.PlayOneShot(BreakSound, GameManager.MasterVolume * GameManager.PlayerVolume * BreakVolume);
        Invoke("Tip19DOWN", 5);
    }

    private void Tip19DOWN()
    {
        tips19.TipsDown();
    }
}
