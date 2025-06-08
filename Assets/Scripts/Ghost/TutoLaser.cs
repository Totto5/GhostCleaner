using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoLaser : MonoBehaviour
{
    public float laserSpeed = 27f;
    public int laser_damage = 2;
    public float lifetime = 2.0f; // ÉrÅ[ÉÄÇÃéıñΩ (ïb)

    private Vector3 direction;

    private TutoGameMane gamemanager;
    public Rigidbody _rigidbody;
    public CapsuleCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<TutoGameMane>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Laser_Move();
    }

    public void Initialize(Vector3 direction)
    {
        this.direction = direction;
        Destroy(gameObject, lifetime);
    }

    private void Laser_Move()
    {
        _rigidbody.AddForce(direction * laserSpeed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gamemanager.DamageScore();
            coll.enabled = false;
        }
    }
}
