using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapTime : MonoBehaviour
{
    public TMP_Text TimeText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.SetText(gameManager.RestTime);
    }
}
