using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGhost : MonoBehaviour
{
    private TutoGameMane gamemanager;
    private GameObject player;
    private FlashLight _flashlight;
    private Rigidbody _rigid;

    [Header("�^�C�v")]
    public GoastType goastType;
    public enum GoastType
    {
        Weak, Dangerous, OneEye
    }

    [Header("HP")]
    public float Ghost_hp = 10;
    [System.NonSerialized] public float lastAttackedTime = 0;
    private bool die = false;
    [System.NonSerialized] public bool Muteki = false;

    [Header("�X�R�A")]
    public int Ghost_score = 10;

    [Header("���C�gHP")]
    public float light_hp = 10;
    [System.NonSerialized] public float lastLightedTime = 0;
    [System.NonSerialized] public float lastHealedTime = 0;
    public float healInterval = 0.1f;
    public float healPower = 1f;
    private float maxLightHP;

    [Header("�z���ϋv�l")]
    [Range(0f, 0.999f)] public float InvisibleDurability = 0.5f;
    [Range(0f, 0.999f)] public float VisibleDurability = 0.1f;
    [System.NonSerialized] public float Durability;//�z���ϋv�l

    [Header("���R����")]
    public float disappearanceTime = 20.0f; // �S�[�X�g�����R���ł���܂ł̎���
    private float Seen_time = 0.0f;//�v���C���[���S�[�X�g�����m���Ă��Ȃ��o�ߎ���

    [Header("�v���C���[���m")]
    public float detectedDistance = 10f;//�v���C���[���S�[�X�g�����m���鋗��
    [System.NonSerialized] public bool isBeingSeen = true; // �S�[�X�g���v���C���[�Ɍ����Ă��邩�ǂ����̃t���O

    [Header("�}�e���A��")]
    public MeshRenderer Ghost_material;//�S�[�X�g�}�e���A��
    public MeshRenderer GhostEye_material;
    private float materialTransparent = 0.0f;

    [Header("�ړ�")]
    public float Move_Speed = 3f;
    public float Move_Radius = 10f;
    public bool MoveOnOff = true;
    private Vector3 CenterPos;
    private Vector3 direction;
    private float TimetoReachTarget;//�^�[�Q�b�g�ɓ��B���邽�߂̎���
    private float timeElapsed;//�o�ߎ���

    public AudioClip ghostSound;
    public float ghostVolume = 0.5f;
    public AudioClip dieSound;
    public float dieVolume = 0.5f;
    private AudioSource audioSource;
    private bool startDie = false;
    private float nextSoundTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<TutoGameMane>();
        player = GameObject.FindWithTag("Player");
        _rigid = GetComponent<Rigidbody>();
        audioSource = this.gameObject.GetComponent<AudioSource>();

        Ghost_material.material.SetFloat("_Transparent", 0f);
        GhostEye_material.material.SetFloat("_Transparent", 0f);

        Durability = InvisibleDurability;
        maxLightHP = light_hp;

        CenterPos = this.transform.position;
        SetRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        ShowGhost();

        Disappearance();

        Player_Detection();

        Sound();

        Dead();
    }

    void FixedUpdate()
    {
        if (MoveOnOff)
        {
            Move();
        }
    }

    private void SetRandomTarget()//�ړ��^�[�Q�b�g�����߂�
    {
        float randomRadius = Random.Range(0f, Move_Radius);
        Vector3 targetPosition = CenterPos + Random.insideUnitSphere * randomRadius;
        Vector3 directionVector = targetPosition - this.transform.position;
        direction = directionVector.normalized;

        float distanceToTarget = Vector3.Distance(this.transform.position, targetPosition);
        TimetoReachTarget = distanceToTarget / Move_Speed;
        timeElapsed = 0f;
    }

    private void Move()//�ړ�
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= TimetoReachTarget)
        {
            SetRandomTarget();
        }

        _rigid.AddForce(direction * Move_Speed - _rigid.velocity, ForceMode.Acceleration);
    }

    private void Sound()
    {
        if (Time.time > nextSoundTime)
        {
            audioSource.PlayOneShot(ghostSound, GameManager.MasterVolume * GameManager.EnemyVolume * ghostVolume);
            nextSoundTime = Time.time + Random.Range(5f, 15f);
        }
    }

    private void ShowGhost()//���C�g�Ō�����
    {
        if (light_hp <= 0)
        {
            Durability = VisibleDurability;

            if (materialTransparent <= 1)
            {
                materialTransparent += Time.deltaTime / 2;
                Ghost_material.material.SetFloat("_Transparent", materialTransparent);
                GhostEye_material.material.SetFloat("_Transparent", materialTransparent);
            }
        }
        else
        {
            Durability = InvisibleDurability;

            if (materialTransparent >= 0)
            {
                materialTransparent -= Time.deltaTime / 2;
                Ghost_material.material.SetFloat("_Transparent", materialTransparent);
                GhostEye_material.material.SetFloat("_Transparent", materialTransparent);
            }
        }

        if (Time.time - lastHealedTime >= healInterval && Time.time - lastLightedTime > 0.5f)
        {
            if (light_hp < maxLightHP)
            {
                light_hp += healPower;
                lastHealedTime = Time.time;
            }
        }
    }

    private void Player_Detection()//�v���C���[���m
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectedDistance)
        {
            isBeingSeen = true;
            Seen_time = 0.0f;
            LookPlayer();
        }
        else
        {
            isBeingSeen = false;
            Seen_time += Time.deltaTime;
        }
    }

    private void LookPlayer()//�v���C���[�̂ق�������
    {
        Vector3 lookdirection = player.transform.position - transform.position;
        lookdirection.y = 0;

        Quaternion lookrotation = Quaternion.LookRotation(lookdirection);
        transform.rotation = Quaternion.Euler(0, lookrotation.eulerAngles.y, 0);
    }

    private void Disappearance()//���R����
    {
        if (!isBeingSeen && Seen_time >= disappearanceTime)
        {
            Destroy(gameObject);
        }
    }

    private void Dead()//�E�����
    {
        if (Ghost_hp <= 0 && Muteki == false)
        {
            Vector3 nowScale = this.transform.localScale;
            this.transform.localScale = new Vector3(nowScale.x - (0.3f * Time.deltaTime), nowScale.y - (0.3f * Time.deltaTime), nowScale.z - (0.3f * Time.deltaTime));
            if (this.transform.localScale.x <= 0)
            {
                die = true;
            }
            if (startDie == false)
            {
                AudioSource.PlayClipAtPoint(dieSound, this.transform.position, GameManager.MasterVolume * GameManager.EnemyVolume * dieVolume);
                startDie = true;
            }
        }

        if (die)
        {
            gamemanager.AddScore(goastType.ToString());
            Destroy(gameObject);
        }
    }

    public void stopMove()
    {
        _rigid.velocity = Vector3.zero;
    }
}
