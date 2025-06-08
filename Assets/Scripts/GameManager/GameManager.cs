using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [Header("制限時間")]
    public float countdownMinutes = 3;
    private float countdownSeconds;

    [Header("スコア")]
    public static int totalscore = 0;

    [Header("右手モードと左手モード")]
    public static bool RightMode = true;
    public PlayerInput playerInput;

    public GameObject RightLight;
    public GameObject LeftLight;
    public GameObject RightVacuum;
    public GameObject LeftVacuum;

    public DynamicMoveProvider moveScr;
    public float dashSpeed = 1.5f;

    public ActionBasedController RightControllerScr;
    public ActionBasedController LeftControllerScr;

    public static float MasterVolume = 0.5f;
    public static float EnemyVolume = 0.5f;
    public static float PlayerVolume = 0.5f;

    public static bool EnglishMode = false;

    public Volume volume;
    private ColorAdjustments colorAdjust;
    private bool startLoadScene = false;
    private Vignette vignette;

    public GameObject ClearCanvas;

    [System.NonSerialized] public string RestTime;
    [System.NonSerialized] public int WGscore = 0;
    [System.NonSerialized] public int OEGscore = 0;
    [System.NonSerialized] public int DGscore = 0;

    public float bossTime = 60;
    public GameObject BossChain;
    public GameObject LockIcon;
    public GameObject LockEffe;
    public OpenDoorDouble doubleDoor;
    private bool BossUnLock = false;

    public GameTuto gameTuto;

    public PlayerSound playerSound;

    public AudioClip endSound;
    public float endVolume = 0.5f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        countdownSeconds = 60 * countdownMinutes;
        volume.profile.TryGet(out colorAdjust);
        volume.profile.TryGet(out vignette);
        audioSource = this.gameObject.GetComponent<AudioSource>();
        ScoreManager.ClearGame = false;
        totalscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();

        if (countdownSeconds <= 0)
        {
            GameEnd();
        }

        if (countdownSeconds <= bossTime)
        {
            if(BossUnLock == false)
            {
                BossChain.SetActive(false);
                LockIcon.SetActive(false);
                LockEffe.SetActive(false);
                doubleDoor.doorLock = false;
                gameTuto.Tip19UP();

                BossUnLock = true;
            }
        }

        if (startLoadScene)
        {
            colorAdjust.postExposure.value -= 5f * Time.deltaTime;
            if (colorAdjust.postExposure.value <= -10)
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }

    private void CountDown()
    {
        if (countdownMinutes > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            RestTime = span.ToString(@"mm\:ss");
        }
        else
        {
            RestTime = "00:00";
        }
    }

    public void GameEnd()
    {
        moveScr.moveSpeed = 0;
        SetNotSeeObjects();
        ClearCanvas.SetActive(true);
        ScoreManager.ClearGame = true;
        audioSource.PlayOneShot(endSound, GameManager.MasterVolume * GameManager.PlayerVolume * endVolume);
        Invoke("MoveScene", 3f);
    }

    private void MoveScene()
    {
        startLoadScene = true;
    }

    public void SetHandMode()
    {
        if(RightMode)
        {
            playerInput.SwitchCurrentActionMap("Right");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Left");
        }
    }

    public void AddScore(int point, string GhostType)
    {
        totalscore += point;
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

    public void DamageScore(int point)
    {
        totalscore -= point;
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
        RightLight.SetActive(!RightMode);
        LeftLight.SetActive(RightMode);
        RightVacuum.SetActive(RightMode);
        LeftVacuum.SetActive(!RightMode);
    }

    public void SetNotSeeObjects()
    {
        RightLight.SetActive(false);
        LeftLight.SetActive(false);
        RightVacuum.SetActive(false);
        LeftVacuum.SetActive(false);
    }

    public void PushDashOnOffButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moveScr.moveSpeed *= dashSpeed;
        }

        if (context.canceled)
        {
            moveScr.moveSpeed /= dashSpeed;
        }
    }
}
