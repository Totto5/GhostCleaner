using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoOneEye : MonoBehaviour
{
    [Header("スクリプト")]
    public TutoGhost ghostScr;

    public bool isInLight = false;
    public bool shoted = false;

    [Header("ダウン時の目")]
    public GameObject DownEye;

    [Header("ライトが効く強さ")]
    [Range(0, 1)] public float lightEffectLine = 0.3f;

    private float stopTime = 0.0f;//ライトから外れた後の残りの停止時間
    private float stopEffect = 2.5f;//ライトから外れた後に停止する時間

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInLight)
        {
            ghostScr.MoveOnOff = false;
            stopTime = stopEffect;
        }
        else
        {
            ghostScr.MoveOnOff = true;
        }

        if (shoted)
        {
            ghostScr.stopMove();
            shoted = false;
        }

        if (stopTime > 0)
        {
            stopTime -= Time.deltaTime;
        }

        DownEye.SetActive(isInLight);
    }

    // ライトに当たった瞬間
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FlashLight"))
        {
            TutoLight lightScr = other.gameObject.GetComponent<TutoLight>();
            if (lightScr.CheckLightThroughWallEye(this.gameObject) && lightScr.normalizedScale <= lightEffectLine)
            {
                isInLight = true;
                shoted = true;
            }
        }
    }

    // ライトに当たってる間
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FlashLight"))
        {
            TutoLight lightScr = other.gameObject.GetComponent<TutoLight>();
            if (lightScr.CheckLightThroughWallEye(this.gameObject) && lightScr.normalizedScale <= lightEffectLine)
            {
                isInLight = true;
            }
            else
            {
                isInLight = false;
            }
        }
    }

    // ライトが離れた時
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlashLight"))
        {
            isInLight = false;
        }
    }
}
