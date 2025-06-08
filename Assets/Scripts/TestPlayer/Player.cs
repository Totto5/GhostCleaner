using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float moveSpeed = 5f;
    public float turnSpeed = 300f;
    private float move1;
    private float move2;

    /*public Transform lightTransform;
    public Transform flashLightTransform;
    public Transform lightSourceTransform;
    public Transform lightTipTransform;
    private float scaleIncrease = 0.01f;
    private float maxScale = 4f;//ライトの最大範囲
    private float minScale = 2f;//ライトの最小範囲
    private float maxDamage = 0.05f;
    private float minDamage = 0.01f;
    public float lightDamage = 0.01f;
    public bool RightMode = true;//右手モードと左手モード
    public bool LightOn = false;//ライトのOn/Off
    private Light flashLight;*/



    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //flashLight = flashLightTransform.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        /*IncreaseLightScale();
        DecreaseLightScale();
        SwitchHandMode();
        CalculateLightDamage();
        UpdateLightSize();
        SwitchLight();*/

        move1 = Input.GetAxis("Vertical");
        move2 = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * move1 * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * move2 * moveSpeed * Time.deltaTime);
    }
    /*private void IncreaseLightScale()
    {
        if ((Input.GetKey(KeyCode.H) && !RightMode) || ((Input.GetKey(KeyCode.K) && RightMode)))
        {
            Vector3 newScale = lightTransform.localScale + new Vector3(scaleIncrease, 0.0f, scaleIncrease);
            if (newScale.x <= maxScale)
            {
                lightTransform.localScale = newScale;
            }
        }
    }
    private void DecreaseLightScale()
    {
        if ((Input.GetKey(KeyCode.J) && !RightMode) || ((Input.GetKey(KeyCode.L) && RightMode)))
        {
            Vector3 newScale = lightTransform.localScale - new Vector3(scaleIncrease, 0.0f, scaleIncrease);
            if (newScale.x >= minScale)
            {
                lightTransform.localScale = newScale;
            }
        }
    }
    private void UpdateLightSize()
    {
        float currentScale = lightTransform.localScale.x;
        float normalizedScale = Mathf.InverseLerp(minScale, maxScale, currentScale);

        flashLight.spotAngle = Mathf.Lerp(10f, 30f, normalizedScale);
    }
    private void CalculateLightDamage()
    {
        float currentScale = lightTransform.localScale.x;

        float normalizedScale = Mathf.InverseLerp(minScale, maxScale, currentScale);
        lightDamage = Mathf.Lerp(maxDamage, minDamage, normalizedScale);
    }
    private void SwitchHandMode()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            RightMode = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            RightMode = false;
        }
    }
    private void SwitchLight()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LightOn = !LightOn;
            lightTransform.gameObject.SetActive(LightOn);
        }
    }
    public bool CheckLightThroughWall()
    {
        Vector3 direction = lightTipTransform.position - lightSourceTransform.position;
        float distance = direction.magnitude;
        Ray ray = new Ray(lightSourceTransform.position, lightTipTransform.position - lightSourceTransform.position);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Ghost1"))
            {
                return false;
            }
        }
        return true;
    }*/
}
