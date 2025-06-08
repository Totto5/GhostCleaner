using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator;

    public bool rightOpen = false;
    public bool leftOpen = false;

    private bool onAnimation = false;

    public AudioClip openSound;
    public float openVolume = 0.3f;
    public AudioClip closeSound;
    public float closeVolume = 0.3f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightOpen == true && onAnimation == false)
        {
            onAnimation = true;
            doorAnimator.SetTrigger("OpenRight");
            audioSource.PlayOneShot(openSound, GameManager.MasterVolume * openVolume);
        }

        if (leftOpen == true && onAnimation == false)
        {
            onAnimation = true;
            doorAnimator.SetTrigger("OpenLeft");
            audioSource.PlayOneShot(openSound, GameManager.MasterVolume * openVolume);
        }
    }

    public void OnAnimationEnd()
    {
        onAnimation = false;
        rightOpen = false;
        leftOpen = false;
        audioSource.PlayOneShot(closeSound, GameManager.MasterVolume * closeVolume);
    }
}
