using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    public Animator JPAnimator;
    public Animator ENAnimator;

    private bool ENmode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TipsUp()
    {
        ENmode = GameManager.EnglishMode;
        if (ENmode == false)
        {
            JPAnimator.SetTrigger("TipsUp");
        }
        else
        {
            ENAnimator.SetTrigger("TipsUp");
        }
    }

    public void TipsDown()
    {
        if (ENmode == false)
        {
            JPAnimator.SetTrigger("TipsDown");
        }
        else
        {
            ENAnimator.SetTrigger("TipsDown");
        }
    }
}
