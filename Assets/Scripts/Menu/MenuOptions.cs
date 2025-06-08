using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    private bool optionMenuOnOff = false;
    public GameObject OptionCanvas;
    public Toggle audioToggle;

    public AudioClip pushSound;
    public float pushVolume = 0.5f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushOptionButton()
    {
        audioSource.PlayOneShot(pushSound, GameManager.MasterVolume * GameManager.PlayerVolume * pushVolume);
        OptionAction();
    }

    public void OptionAction()
    {
        audioToggle.isOn = false;
        optionMenuOnOff = !optionMenuOnOff;
        OptionCanvas.SetActive(optionMenuOnOff);
    }
}
