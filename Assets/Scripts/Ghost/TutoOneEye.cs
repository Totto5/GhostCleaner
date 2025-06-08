using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoOneEye : MonoBehaviour
{
    [Header("�X�N���v�g")]
    public TutoGhost ghostScr;

    public bool isInLight = false;
    public bool shoted = false;

    [Header("�_�E�����̖�")]
    public GameObject DownEye;

    [Header("���C�g����������")]
    [Range(0, 1)] public float lightEffectLine = 0.3f;

    private float stopTime = 0.0f;//���C�g����O�ꂽ��̎c��̒�~����
    private float stopEffect = 2.5f;//���C�g����O�ꂽ��ɒ�~���鎞��

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

    // ���C�g�ɓ��������u��
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

    // ���C�g�ɓ������Ă��
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

    // ���C�g�����ꂽ��
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FlashLight"))
        {
            isInLight = false;
        }
    }
}
