using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartMenu : MonoBehaviour
{
    public bool AvailableMenu = true;//メニュー使用可否
    private bool OnMenu = false;//メニュー状態

    public GameObject MenuObj;//プレハブ
    private GameObject NowMenu;//設置済みメニュー

    public Transform PlayerTrans;//プレイヤーの位置
    public Transform PlayerRote;//プレイヤーの向いてる方向
    private float PlayerDirection;//プレイヤーの向いてるY軸方向（ラジアン）

    public float Y_Offset = 1.5f;
    public float Menu_Radius = 2;

    public TutoGameMane gameMane;
    public XRInteractorLineVisual RightLine;
    public XRInteractorLineVisual LeftLine;

    public DynamicMoveProvider moveScr;
    private float defaultSpeed;

    public Volume volume;
    private ColorAdjustments colorAdjust;
    public bool startLoadScene = false;

    public AudioClip onSound;
    public float onVolume = 0.5f;
    public AudioClip offSound;
    public float offVolume = 0.5f;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = moveScr.moveSpeed;
        volume.profile.TryGet(out colorAdjust);
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoadScene)
        {
            colorAdjust.postExposure.value -= 5f * Time.deltaTime;
            if (colorAdjust.postExposure.value <= -10)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void PushMenuButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MenuAction();
        }
    }

    public void MenuAction()
    {
        if (OnMenu == false && AvailableMenu == true)
        {
            PlayerDirection = PlayerRote.eulerAngles.y * Mathf.Deg2Rad;
            NowMenu = Instantiate(MenuObj, new Vector3(PlayerTrans.position.x + Menu_Radius * Mathf.Sin(PlayerDirection), PlayerTrans.position.y + Y_Offset, PlayerTrans.position.z + Menu_Radius * Mathf.Cos(PlayerDirection)),
                                    Quaternion.Euler(new Vector3(MenuObj.transform.rotation.x, PlayerRote.eulerAngles.y, MenuObj.transform.rotation.z)));
            OnMenu = true;
            gameMane.SetNotSeeObjects();
            RightLine.enabled = true;
            LeftLine.enabled = true;
            moveScr.moveSpeed = 0;
            audioSource.PlayOneShot(onSound, GameManager.MasterVolume * GameManager.PlayerVolume * onVolume);
        }
        else
        {
            Destroy(NowMenu);
            OnMenu = false;
            gameMane.SetSeeObjects();
            RightLine.enabled = false;
            LeftLine.enabled = false;
            moveScr.moveSpeed = defaultSpeed;
            audioSource.PlayOneShot(offSound, GameManager.MasterVolume * GameManager.PlayerVolume * offVolume);
        }
    }
}
