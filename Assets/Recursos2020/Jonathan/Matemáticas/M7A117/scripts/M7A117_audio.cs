using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A117_audio : MonoBehaviour
{
    public ControlAudio ControlAudio;
    public Toggle toggle;
    public int audio;
    public AudioSource AudioSource;
    public bool play;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { playStop(toggle); });
    }

    // Update is called once per frame
    void Update()
    {
        if (play) {
            print(AudioSource.time);

            if (AudioSource.isPlaying)
            {
                toggle.isOn = true;
                play = true;
            }
            else {
                toggle.isOn = false;
                play = false;
            }
        }
        
    }

    public void playStop(Toggle t) {
        if (t.isOn)
        {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._selection;
           // ControlAudio.PlayAudio(audio);
            AudioSource.clip = audioClip;
            AudioSource.Play();

            play = true;
        }
        else {
            t.GetComponent<Image>().sprite = t.GetComponent<BehaviourSprite>()._default;
            ControlAudio.StopAudio();
            AudioSource.Stop();
            AudioSource.clip = null;
            play = false;
        }
    }
}
