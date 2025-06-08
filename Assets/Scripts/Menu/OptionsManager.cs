using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public GameObject AudioMenu;

    public Toggle handToggle;
    public Toggle LanguageToggle;
    public Slider MasterSlider;
    public Slider PlayerSlider;
    public Slider EnemySlider;

    private GameManager gameMane = null;
    private TutoGameMane tutoMane = null;

    public AudioClip pushSound;
    public float pushVolume = 0.5f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        handToggle.isOn = GameManager.RightMode;
        LanguageToggle.isOn = !GameManager.EnglishMode;
        MasterSlider.value = GameManager.MasterVolume;
        PlayerSlider.value = GameManager.PlayerVolume;
        EnemySlider.value = GameManager.EnemyVolume;

        gameMane = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        tutoMane = GameObject.FindWithTag("GameManager").GetComponent<TutoGameMane>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeHandMode(bool rightMode)
    {
        GameManager.RightMode = rightMode;
        audioSource.PlayOneShot(pushSound, GameManager.MasterVolume * GameManager.PlayerVolume * pushVolume);

        if (gameMane != null)
        {
            gameMane.SetHandMode();
        }
        else
        {
            tutoMane.SetHandMode();
        }
    }
    public void ChangeLanguageMode(bool languageMode)
    {
        GameManager.EnglishMode = !languageMode;
        audioSource.PlayOneShot(pushSound, GameManager.MasterVolume * GameManager.PlayerVolume * pushVolume);

        if (tutoMane != null)
        {
            tutoMane.SetLangMode();
        }
    }

    public void ChangeAudioMode(bool showAudio)
    {
        AudioMenu.SetActive(showAudio);
        audioSource.PlayOneShot(pushSound, GameManager.MasterVolume * GameManager.PlayerVolume * pushVolume);
    }

    public void ChangeMasterVolume()
    {
        GameManager.MasterVolume = MasterSlider.value;
    }
    public void ChangePlayerVolume()
    {
        GameManager.PlayerVolume = PlayerSlider.value;
    }
    public void ChangeEnemyVolume()
    {
        GameManager.EnemyVolume = EnemySlider.value;
    }
}
