using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Continue : MonoBehaviour
{
    private Menu menuText;

    // Start is called before the first frame update
    void Start()
    {
        menuText = GameObject.FindWithTag("Menu").GetComponent<Menu>();
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
