using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoFireLaser : MonoBehaviour
{
    private GameObject player;
    public GameObject laserPrefab;
    public Transform laserStartPoint; // ���[�U�[�̔��ˊJ�n�n�_
    public TutoGhost ghostScript;

    private float nextLaserTime; // ���Ƀ��[�U�[�𔭎˂��鎞��
    public float LaserRate = 2.0f; // ���[�U�[�𔭎˂���Ԋu�i�b�j

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        nextLaserTime = Time.time + LaserRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextLaserTime && ghostScript.isBeingSeen && ghostScript.light_hp > 0)
        {
            Laser();
            nextLaserTime = Time.time + LaserRate;
        }
    }

    private void Laser()
    {
        Vector3 direction = (player.transform.position - laserStartPoint.position).normalized;

        GameObject laser = Instantiate(laserPrefab, laserStartPoint.position, Quaternion.LookRotation(direction));

        TutoLaser laserController = laser.GetComponent<TutoLaser>();
        if (laserController != null)
        {
            laserController.Initialize(direction);
        }
    }
}
