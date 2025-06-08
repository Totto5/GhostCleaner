using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poster : MonoBehaviour
{
    public GameObject JP;
    public GameObject EN;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLang()
    {
        if (GameManager.EnglishMode)
        {
            EN.SetActive(true);
            JP.SetActive(false);
        }
        else
        {
            EN.SetActive(false);
            JP.SetActive(true);
        }
    }
}
