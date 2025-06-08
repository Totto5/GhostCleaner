using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Title : MonoBehaviour
{
    private Menu menuText;

    public AudioClip pushSound;
    public float pushVolume = 0.5f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        menuText = GameObject.FindWithTag("Menu").GetComponent<Menu>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushTitleButton()
    {
        audioSource.PlayOneShot(pushSound, GameManager.MasterVolume * GameManager.PlayerVolume * pushVolume);
        menuText.startLoadScene = true;
    }
}
