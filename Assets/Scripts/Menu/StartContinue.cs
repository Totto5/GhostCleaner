using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartContinue : MonoBehaviour
{
    private StartMenu menuText;

    // Start is called before the first frame update
    void Start()
    {
        menuText = GameObject.FindWithTag("Menu").GetComponent<StartMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushContinueButton()
    {
        menuText.MenuAction();
    }
}
