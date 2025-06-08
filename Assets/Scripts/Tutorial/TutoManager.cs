using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public TutoGameMane gameMane;

    public Tips tips1;
    public Tips tips2;
    public Tips tips3;
    public Tips tips4;
    public Tips tips4_5;
    public Tips tips5;
    public Tips tips6;
    public Tips tips7;
    public Tips tips8;  
    public Tips tips9;
    public Tips tips10;
    public Tips tips11;
    public Tips tips12;
    public Tips tips13;
    public Tips tips13_5;
    public Tips tips14;
    public Tips tips15;
    public Tips tips16;

    public float intervalTime = 1.0f;

    public Transform Player;
    private Vector3 firstPos;
    private Quaternion firstRote;

    private bool sta1 = false;
    private bool sta2 = false;
    private bool sta3 = false;
    private bool sta4 = false;
    private bool sta5 = false;
    private bool sta6 = false;
    private bool sta78 = false;
    private bool sta910 = false;
    private bool sta11 = false;
    private bool sta12 = false;
    private bool sta13 = false;
    private bool sta1415 = false;

    public CheakInPlayer Sta3Coll;
    public CheakInPlayer Sta4Coll;

    public Transform GeneSpot;
    public GameObject WG;
    public GameObject OEG;
    public GameObject DG;

    private GameObject GeneGhost;

    private TutoGhost ghostScr;
    private TutoOneEye OEGScr;

    public TutoVacuum vacuum1;
    public TutoVacuum vacuum2;
    public TutoLight light1;
    public TutoLight light2;

    public GameObject LeftCon;
    public GameObject RightCon;

    public OpenTutoDoor door;

    public GameObject NextTrigger;

    public bool TutoInRoom = false;

    public AudioClip tipsSound;
    public float tipsVolume = 0.5f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        Invoke("Tip1", intervalTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (sta1 == true)
        {
            if (Player.transform.position != firstPos)
            {
                Tip2();
                sta1 = false;
            }
        }
        if(sta2 == true)
        {
            if (Quaternion.Angle(firstRote, Player.transform.rotation) > 1f)
            {
                Tip3();
                sta2 = false;
                sta3 = true;
            }
        }
        if(sta3 == true)
        {
            if (Sta3Coll.InPlayer == true)
            {
                Tip4();
                sta3 = false;
                sta4 = true;
            }
        }
        if(sta4 == true)
        {
            if(Sta4Coll.InPlayer == true)
            {
                Tip5();
                sta4 = false;
            }
        }
        if(sta5 == true)
        {
            if(ghostScr.light_hp <= 0)
            {
                Tip6();
                sta5 = false;
                sta6 = true;
            }
        }
        if(sta6 == true)
        {
            if(vacuum1.isSucking == true || vacuum2.isSucking == true)
            {
                Tip78();
                sta6 = false;
                sta78 = true;
            }
        }
        if(sta78 == true)
        {
            if(ghostScr.Ghost_hp <= 0)
            {
                Tip910();
                sta78 = false;
                sta910 = true;
            }
        }
        if(sta910 == true)
        {
            if(light1.PushedBigButton || light1.PushedSmallButton || light2.PushedBigButton || light2.PushedSmallButton)
            {
                Tip11();
                sta910 = false;
            }
        }
        if (sta11 == true)
        {
            if (OEGScr.shoted == true)
            {
                Tip12();
                sta11 = false;
                sta12 = true;
            }
        }
        if (sta12 == true)
        {
            if (ghostScr.Ghost_hp <= 0)
            {
                Tip13();
                sta12 = false;
            }
        }
        if (sta13 == true)
        {
            if (ghostScr.Ghost_hp <= 0)
            {
                Tip1415();
                sta13 = false;
                sta1415 = true;
            }
        }
        if (sta1415 == true)
        {
            if (light1.LightOn == false || light2.LightOn == false)
            {
                Tip16();
                sta1415 = false;
            }
        }
    }

    private void Tip1()
    {
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips1.TipsUp();

        firstPos = Player.transform.position;
        sta1 = true;
    }
    private void Tip2()
    {
        tips1.TipsDown();
        Invoke("Tip2UP", intervalTime);
    }
    private void Tip3()
    {
        tips2.TipsDown();
        Invoke("Tip3UP", intervalTime);
    }
    private void Tip4()
    {
        tips3.TipsDown();
        Invoke("Tip4UP", intervalTime);
    }
    private void Tip5()
    {
        tips4.TipsDown();
        tips4_5.TipsDown();
        Invoke("Tip5UP", intervalTime);
    }
    private void Tip6()
    {
        tips5.TipsDown();
        Invoke("Tip6UP", intervalTime);
    }
    private void Tip78()
    {
        tips6.TipsDown();
        Invoke("Tip78UP", intervalTime);
    }
    private void Tip910()
    {
        tips7.TipsDown();
        tips8.TipsDown();
        Invoke("Tip910UP", intervalTime);
    }
    private void Tip11()
    {
        tips9.TipsDown();
        tips10.TipsDown();
        Invoke("Tip11UP", intervalTime);
    }
    private void Tip12()
    {
        tips11.TipsDown();
        Invoke("Tip12UP", intervalTime);
    }
    private void Tip13()
    {
        tips12.TipsDown();
        Invoke("Tip13UP", intervalTime);
    }
    private void Tip1415()
    {
        tips13.TipsDown();
        tips13_5.TipsDown();
        Invoke("Tip1415UP", intervalTime);
    }
    private void Tip16()
    {
        tips14.TipsDown();
        tips15.TipsDown();
        Invoke("Tip16UP", intervalTime);
    }



    private void Tip2UP()
    {
        firstRote = Player.transform.rotation;
        sta2 = true;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips2.TipsUp();
    }
    private void Tip3UP()
    {
        door.doorLock = false;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips3.TipsUp();
    }
    private void Tip4UP()
    {
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips4.TipsUp();
        tips4_5.TipsUp();
    }
    private void Tip5UP()
    {
        door.doorLock = true;

        TutoInRoom = true;
        gameMane.SetSeeObjects();
        LeftCon.SetActive(false);
        RightCon.SetActive(false);

        GeneGhost = Instantiate(WG, GeneSpot.position, GeneSpot.rotation);
        ghostScr = GeneGhost.GetComponent<TutoGhost>();
        ghostScr.Muteki = true;

        sta5 = true;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips5.TipsUp();
    }
    private void Tip6UP()
    {
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips6.TipsUp();
    }
    private void Tip78UP()
    {
        ghostScr.Muteki = false;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips7.TipsUp();
        tips8.TipsUp();
    }
    private void Tip910UP()
    {
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips9.TipsUp();
        tips10.TipsUp();
    }
    private void Tip11UP()
    {
        GeneGhost = Instantiate(OEG, GeneSpot.position, GeneSpot.rotation);
        ghostScr = GeneGhost.GetComponent<TutoGhost>();
        ghostScr.Muteki = true;
        OEGScr = GameObject.FindWithTag("GhostEye").GetComponent<TutoOneEye>();

        sta11 = true;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips11.TipsUp();
    }
    private void Tip12UP()
    {
        ghostScr.Muteki = false;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips12.TipsUp();
    }
    private void Tip13UP()
    {
        GeneGhost = Instantiate(DG, GeneSpot.position, GeneSpot.rotation);
        ghostScr = GeneGhost.GetComponent<TutoGhost>();

        sta13 = true;

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips13.TipsUp();
        tips13_5.TipsUp();
    }
    private void Tip1415UP()
    {
        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips14.TipsUp();
        tips15.TipsUp();
    }
    private void Tip16UP()
    {
        NextTrigger.SetActive(true);

        audioSource.PlayOneShot(tipsSound, GameManager.MasterVolume * GameManager.PlayerVolume * tipsVolume);
        tips16.TipsUp();
    }
}
