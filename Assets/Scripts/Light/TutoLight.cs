using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TutoLight : MonoBehaviour
{
    public Transform lightTipTransform;
    public CapsuleCollider lightColl;
    private TutoGhost ghost;

    [System.NonSerialized] public float normalizedScale;

    public bool LightOn = true;//���C�g��On/Off

    [Header("�傫���̑������x")]
    public float RadiusScaleIncrease = 0.01f;

    [Header("����͈́i���a�j")]
    public float maxRadiusScale = 10f;
    public float minRadiusScale = 1f;

    [Header("����͈́i�����j")]
    public float maxHeightScale = 100f;
    public float minHeightScale = 40f;

    [Header("����͈́i�ʒu�j")]
    public float maxCenterScale = 50f;
    public float minCenterScale = 20f;

    [Header("���C�g�̔��a")]
    public float maxLightAngle = 80f;
    public float minLightAngle = 10f;
    public float InnerOuterRate = 0.35f;//���C�g�̂ق₯

    [Header("���C�g�̋���")]
    public float maxLightRange = 50f;
    public float minLightRange = 20f;

    [Header("���C�g�̋���")]
    public float maxIntensity = 150f;
    public float minIntensity = 10f;

    [Header("�_���[�W")]
    public float maxDamage = 5f;
    public float minDamage = 1f;
    [System.NonSerialized] public float lightDamage = 0.01f;
    public float attackInterval = 0.5f;

    [Header("���C�g")]
    public Light spotLight;
    public Light pointLight;

    public bool PushedBigButton = false;
    public bool PushedSmallButton = false;

    public ActionBasedController controllerScr;

    public GameObject MiniMap;
    public GameObject MapEffe;
    public GameObject MapLight;

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
        Big();
        Small();

        normalizeScale();

        UpdateCollider();
        UpdateLight();
        CalculateLightDamage();
    }

    private void Big()
    {
        if (PushedBigButton && LightOn)
        {
            if (lightColl.radius <= maxRadiusScale)
            {
                float newRadiusScale = lightColl.radius + RadiusScaleIncrease;
                if (newRadiusScale >= maxRadiusScale)
                {
                    newRadiusScale = maxRadiusScale;
                }
                lightColl.radius = newRadiusScale;
                controllerScr.SendHapticImpulse(0.1f, 0.01f);
            }
        }
    }

    private void Small()
    {
        if (PushedSmallButton && LightOn)
        {
            if (lightColl.radius >= minRadiusScale)
            {
                float newRadiusScale = lightColl.radius - RadiusScaleIncrease;
                if (newRadiusScale <= minRadiusScale)
                {
                    newRadiusScale = minRadiusScale;
                }
                lightColl.radius = newRadiusScale;
                controllerScr.SendHapticImpulse(0.1f, 0.01f);
            }
        }
    }

    private void normalizeScale()//�傫���̕W����
    {
        normalizedScale = Mathf.InverseLerp(minRadiusScale, maxRadiusScale, lightColl.radius);
        if (normalizedScale <= 0.0001f)
        {
            normalizedScale = 0.0001f;
        }
        //Debug.Log(normalizedScale);
    }

    private void UpdateCollider()//�����蔻��̕ύX
    {
        lightColl.height = Mathf.Lerp(maxHeightScale, minHeightScale, normalizedScale);

        lightColl.center = new Vector3(0, 0, Mathf.Lerp(maxCenterScale, minCenterScale, normalizedScale) + 1f);
    }


    private void UpdateLight()//���C�g�̕ύX
    {
        spotLight.spotAngle = Mathf.Lerp(minLightAngle, maxLightAngle, normalizedScale);
        spotLight.innerSpotAngle = spotLight.spotAngle * InnerOuterRate;

        spotLight.range = Mathf.Lerp(maxLightRange, minLightRange, normalizedScale);

        spotLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, normalizedScale);
    }

    private void CalculateLightDamage()//�_���[�W�̕ύX
    {
        lightDamage = Mathf.Lerp(maxDamage, minDamage, normalizedScale);
    }

    private void OnTriggerStay(Collider other)//�����蔻��
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            if (CheckLightThroughWall(other.gameObject))
            {
                ghost = other.gameObject.GetComponent<TutoGhost>();
                if (Time.time - ghost.lastLightedTime >= attackInterval)
                {
                    if (ghost.light_hp > -0.5)
                    {
                        ghost.light_hp -= lightDamage;
                        ghost.lastLightedTime = Time.time;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)//�����蔻��
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            if (CheckLightThroughWall(other.gameObject))
            {
                controllerScr.SendHapticImpulse(0.3f, 0.1f);
            }
        }
    }

    private bool CheckLightThroughWall(GameObject ghostObj)//��Q�����Ȃ���
    {
        Vector3 direction = ghostObj.transform.position - lightTipTransform.position;

        Ray ray = new Ray(lightTipTransform.position, direction);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ghost") || hit.collider.CompareTag("GhostEye"))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckLightThroughWallEye(GameObject ghostObj)//��Q�����Ȃ���
    {
        Vector3 direction = ghostObj.transform.position - lightTipTransform.position;

        Ray ray = new Ray(lightTipTransform.position, direction);
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("GhostEye"))
            {
                return true;
            }
        }
        return false;
    }

    public void PushLightOnOffButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            controllerScr.SendHapticImpulse(0.8f, 0.1f);
            LightOn = !LightOn;

            lightColl.enabled = LightOn;
            spotLight.enabled = LightOn;
            pointLight.enabled = LightOn;
            MiniMap.SetActive(!LightOn);
            MapEffe.SetActive(!LightOn);
            MapLight.SetActive(!LightOn);

            audioSource.PlayOneShot(pushSound, GameManager.MasterVolume * GameManager.PlayerVolume * pushVolume);
        }
    }

    public void PushLightBigButton(InputAction.CallbackContext context)//�傫��
    {
        if (context.started)
        {
            PushedBigButton = true;
        }

        if (context.canceled)
        {
            PushedBigButton = false;
        }
    }

    public void PushLightSmallButton(InputAction.CallbackContext context)//������
    {
        if (context.started)
        {
            PushedSmallButton = true;
        }

        if (context.canceled)
        {
            PushedSmallButton = false;
        }
    }
}
