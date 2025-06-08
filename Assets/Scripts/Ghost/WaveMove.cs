using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMove : MonoBehaviour
{
    public float bounceFrequecy = 2.0f;
    public float bounceAmplitude = 0.5f;
    private Vector3 lastPosition;
    private Vector3 startPos;
    public bool type1 = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (type1)
        {
            WaveMove1();
        }
        else
        {
            WaveMove2();
        }
    }
    private void WaveMove1()
    {
        Vector3 parentVelocity = transform.parent.GetComponent<Rigidbody>().velocity;
        Vector3 currentDirection = parentVelocity.normalized;
        Vector3 upAxis = Vector3.Cross(currentDirection, Vector3.right).normalized;
        float verticalOffset = Mathf.Sin(Time.time * bounceFrequecy) * bounceAmplitude;
        Debug.Log("verticalOffset=" + verticalOffset);
        transform.localPosition = startPos + Vector3.up * verticalOffset;
    }
    private void WaveMove2()
    {
        Vector3 parentVelocity = transform.parent.GetComponent<Rigidbody>().velocity;
        Vector3 currentDirection = parentVelocity.normalized;
        Vector3 upAxis = Vector3.Cross(currentDirection, Vector3.right).normalized;
        float verticalOffset = Mathf.Sin(Time.time * bounceFrequecy) * bounceAmplitude;
        Debug.Log("verticalOffset=" + verticalOffset);
        transform.localPosition = startPos + upAxis * verticalOffset;
    }
}
