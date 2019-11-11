using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    AudioManager am;

    void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    public void ClickButton()
    {
        if(am != null)
        {
            am.Play("ButtonClick");
        }
    }
}
