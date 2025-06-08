using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    public GameObject Player;
    public float xOff;
    public float yOff;
    public float zOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.transform.position.x + xOff, Player.transform.position.x + yOff, Player.transform.position.x + zOff);
    }
}
