using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_stopWatch : MonoBehaviour
{
    public float timeStart, seconds;
    public Text textBox;
    public Text startBtnText;

    public bool timerActive = false;

    // Use this for initialization
    void Start()
    {
        textBox.text = timeStart.ToString("00:00");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            timeStart += Time.deltaTime;
            seconds = timeStart % 60;
            textBox.text = timeStart.ToString("00:00");
        }
    }
    public void timerButton()
    {
        timerActive = !timerActive;
        // startBtnText.text = timerActive ? "Pause" : "Start";
    }
}
