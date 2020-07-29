﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject dialogBox2;
    float timerDisplay;
    
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }
    
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                dialogBox2.SetActive(false);
            }
        }
    }
    
    public void DisplayDialog()
    {
        
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        
    }
    
    public void DisplayDialog2()
    {
        
        timerDisplay = displayTime;
        dialogBox2.SetActive(true);
        
    }
}
