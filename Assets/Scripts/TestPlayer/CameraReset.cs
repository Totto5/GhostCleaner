using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraReset : MonoBehaviour
{
    public Transform _OVRCameraRig;
    public Transform _centreEyeAnchor;

    // Start is called before the first frame update
    void Start()
    {
        RecenterHeadset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecenterHeadset()
    {
        StartCoroutine(ResetCamera(new Vector3(90, 0, 0), 0));
    }

    //Resets the OVRCameraRig's position and Y-axis rotation to help align the player's starting position and view to the target parameters
    IEnumerator ResetCamera(Vector3 targetPosition, float targetYRotation)
    {
        //EditorDebugOffset();
        yield return new WaitForEndOfFrame();
        float currentRotY = _centreEyeAnchor.eulerAngles.y;
        float difference = targetYRotation - currentRotY;
        _OVRCameraRig.Rotate(0, difference, 0);

        Vector3 newPos = new Vector3(targetPosition.x - _centreEyeAnchor.position.x, 0, targetPosition.z - _centreEyeAnchor.position.z);
        _OVRCameraRig.transform.position += newPos;
    }
}
