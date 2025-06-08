using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TutoGameMane : MonoBehaviour
{
    public PlayerInput playerInput;

    public GameObject RightLight;
    public GameObject LeftLight;
    public GameObject RightVacuum;
    public GameObject LeftVacuum;

    public DynamicMoveProvider moveScr;
    public float dashSpeed = 1.5f;

    public ActionBasedController RightControllerScr;
    public ActionBasedController LeftControllerScr;

    public Volume volume;
    private ColorAdjustments colorAdjust;
    private bool startLoadGameScene = false;
    private Vignette vignette;

    public int WGscore = 0;
    public int OEGscore = 0;
    public int DGscore = 0;

    public TutoManager tutoMane;

    public Poster WG_poster;
    public Poster OEG_poster;
    public Poster DG_poster;

    public PlayerSound playerSound;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out colorAdjust);
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoadGameScene)
        {
            colorAdjust.postExposure.value -= 3f * Time.deltaTime;
            if (colorAdjust.postExposure.value <= -10)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void TutoEnd()
    {
        moveScr.moveSpeed = 0;
        SetNotSeeObjects();
        startLoadGameScene = true;
    }

    public void SetHandMode()
    {
        if (GameManager.RightMode)
        {
            playerInput.SwitchCurrentActionMap("Right");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Left");
        }
    }

    public void SetLangMode()
    {
        WG_poster.ChangeLang();
        OEG_poster.ChangeLang();
        DG_poster.ChangeLang();
    }

    public void AddScore(string GhostType)
    {
        if (GhostType == "Weak")
        {
            WGscore++;
        }
        else if (GhostType == "OneEye")
        {
            OEGscore++;
        }
        else
        {
            DGscore++;
        }
    }

    public void DamageScore()
    {
        playerSound.DamageAudio();
        vignette.intensity.value = 0.4f;
        Invoke("resetRed", 0.5f);

        RightControllerScr.SendHapticImpulse(1f, 0.2f);
        LeftControllerScr.SendHapticImpulse(1f, 0.2f);
    }

    private void resetRed()
    {
        vignette.intensity.value = 0f;
    }

    public void SetSeeObjects()
    {
        if (tutoMane.TutoInRoom == true)
        {
            RightLight.SetActive(!GameManager.RightMode);
            LeftLight.SetActive(GameManager.RightMode);
            RightVacuum.SetActive(GameManager.RightMode);
            LeftVacuum.SetActive(!GameManager.RightMode);
        }
    }

    public void SetNotSeeObjects()
    {
        if (tutoMane.TutoInRoom == true)
        {
            RightLight.SetActive(false);
            LeftLight.SetActive(false);
            RightVacuum.SetActive(false);
            LeftVacuum.SetActive(false);
        }
    }
}
