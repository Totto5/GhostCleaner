using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLang : MonoBehaviour
{
    public TMP_Text text;
    public string EN;
    public string JP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.EnglishMode == false)
        {
            text.SetText(JP);
        }
        else
        {
            text.SetText(EN);
        }
    }
}
