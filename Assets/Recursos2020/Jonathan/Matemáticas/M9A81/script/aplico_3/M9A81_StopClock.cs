using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A81_StopClock : MonoBehaviour
{
    public float timeStart;
    public Text textBox;
    public Text startBtnText;

    public bool timerActive = false;
    public float timeLeft;
    public Text text;

    // Use this for initialization
    void Update()
    {
        if (timerActive)
        {
            timeLeft -= Time.deltaTime;
            var ss = Convert.ToInt32(timeLeft % 60).ToString("00");
            var mm = (Math.Floor(timeLeft / 60) % 60).ToString("00");
            var hh = Math.Floor(timeLeft / 60 / 60).ToString("00");
            text.text = (mm + ":" + ss);
        }

    }
    public void timerButton()
    {
        timerActive = !timerActive;
        // startBtnText.text = timerActive ? "Pause" : "Start";
    }
}
  
