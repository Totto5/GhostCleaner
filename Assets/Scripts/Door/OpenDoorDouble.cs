using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorDouble : MonoBehaviour
{
    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;

    public bool doorOpen = false;

    public bool doorLock = false;

    private bool onAnimation = false;

    public AudioClip openSound;
    public float openVolume = 0.3f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorLock == false && doorOpen == true && onAnimation == false)
        {
            onAnimation = true;
            leftDoorAnimator.SetTrigger("OpenDoor");
            rightDoorAnimator.SetTrigger("OpenDoor");
            audioSource.PlayOneShot(openSound, GameManager.MasterVolume * openVolume);
        }

        if (doorLock == true && doorOpen == true)
        {
            doorOpen = false;
        }
    }
}
