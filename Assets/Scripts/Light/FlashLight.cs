using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : MonoBehaviour
{
    public Transform lightTipTransform;
    public CapsuleCollider lightColl;
    private Ghost ghost;

    [System.NonSerialized] public float normalizedScale;

    public bool LightOn = true;//ライトのOn/Off

    [Header("大きさの増減速度")]
    public float RadiusScaleIncrease = 0.01f;

    [Header("判定範囲（半径）")]
    public float maxRadiusScale = 10f;
    public float minRadiusScale = 1f;

    [Header("判定範囲（距離）")]
    public float maxHeightScale = 100f;
    public float minHeightScale = 40f;

    [Header("判定範囲（位置）")]
    public float maxCenterScale = 50f;
    public float minCenterScale = 20f;

    [Header("ライトの半径")]
    public float maxLightAngle = 80f;
    public float minLightAngle = 10f;
    public float InnerOuterRate = 0.35f;//ライトのほやけ

    [Header("ライトの距離")]
    public float maxLightRange = 50f;
    public float minLightRange = 20f;

    [Header("ライトの強さ")]
    public float maxIntensity = 150f;
    public float minIntensity = 10f;

    [Header("ダメージ")]
    public float maxDamage = 5f;
    public float minDamage = 1f;
    [System.NonSerialized] public float lightDamage = 0.01f;
    public float attackInterval = 0.5f;

    [Header("ライト")]
    public Light spotLight;
    public Light pointLight;

    private bool PushedBigButton = false;
    private bool PushedSmallButton = false;

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
        if(PushedBigButton && LightOn)
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

    private void normalizeScale()//大きさの標準化
    {
        normalizedScale = Mathf.InverseLerp(minRadiusScale, maxRadiusScale, lightColl.radius);
        if (normalizedScale <= 0.0001f)
        {
            normalizedScale = 0.0001f;
        }
        //Debug.Log(normalizedScale);
    }

    private void UpdateCollider()//当たり判定の変更
    {
        lightColl.height = Mathf.Lerp(maxHeightScale, minHeightScale, normalizedScale);

        lightColl.center = new Vector3(0, 0, Mathf.Lerp(maxCenterScale, minCenterScale, normalizedScale) + 1f);
    }


    private void UpdateLight()//ライトの変更
    {
        spotLight.spotAngle = Mathf.Lerp(minLightAngle, maxLightAngle, normalizedScale);
        spotLight.innerSpotAngle = spotLight.spotAngle * InnerOuterRate;

        spotLight.range = Mathf.Lerp(maxLightRange, minLightRange, normalizedScale);

        spotLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, normalizedScale);
    }

    private void CalculateLightDamage()//ダメージの変更
    {
        lightDamage = Mathf.Lerp(maxDamage, minDamage, normalizedScale);
    }

    private void OnTriggerStay(Collider other)//当たり判定
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            if (CheckLightThroughWall(other.gameObject))
            {
                ghost = other.gameObject.GetComponent<Ghost>();
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

    private void OnTriggerEnter(Collider other)//当たり判定
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            if (CheckLightThroughWall(other.gameObject))
            {
                controllerScr.SendHapticImpulse(0.3f, 0.1f);
            }
        }
    }

    private bool CheckLightThroughWall(GameObject ghostObj)//障害物がないか
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

    public bool CheckLightThroughWallEye(GameObject ghostObj)//障害物がないか
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

    public void PushLightBigButton(InputAction.CallbackContext context)//大きく
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

    public void PushLightSmallButton(InputAction.CallbackContext context)//小さく
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
