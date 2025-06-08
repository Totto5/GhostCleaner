using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutoGhostMap : MonoBehaviour
{
    public TMP_Text ScoreText;
    private TutoGameMane gameManager;
    public GoastType goastType;
    public enum GoastType
    {
        Weak, Dangerous, OneEye
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<TutoGameMane>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goastType == GoastType.Weak)
        {
            ScoreText.SetText(gameManager.WGscore.ToString());
        }
        else if (goastType == GoastType.Dangerous)
        {
            ScoreText.SetText(gameManager.OEGscore.ToString());
        }
        else
        {
            ScoreText.SetText(gameManager.DGscore.ToString());
        }
    }
}
