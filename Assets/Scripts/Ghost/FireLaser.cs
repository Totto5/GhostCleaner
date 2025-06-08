using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    private GameObject player;
    public GameObject laserPrefab;
    public Transform laserStartPoint; // レーザーの発射開始地点
    public Ghost ghostScript;

    private float nextLaserTime; // 次にレーザーを発射する時間
    public float LaserRate = 2.0f; // レーザーを発射する間隔（秒）

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        nextLaserTime = Time.time + LaserRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextLaserTime && ghostScript.isBeingSeen && ghostScript.light_hp > 0)
        {
            Laser();
            nextLaserTime = Time.time + LaserRate;
        }
    }

    private void Laser()
    {
        Vector3 direction = (player.transform.position - laserStartPoint.position).normalized;

        GameObject laser = Instantiate(laserPrefab, laserStartPoint.position, Quaternion.LookRotation(direction));

        LaserController laserController = laser.GetComponent<LaserController>();
        if (laserController != null)
        {
            laserController.Initialize(direction);
        }
    }
}
