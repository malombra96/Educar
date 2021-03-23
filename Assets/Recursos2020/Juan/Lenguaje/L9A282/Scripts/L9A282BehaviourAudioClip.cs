using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L9A282BehaviourAudioClip : MonoBehaviour
{
    ControlAudio _controlAudio;
    public L9A282BehaviourAudioClip other;
    float duracionAudio;
    public Button pause;
    public Toggle toggleAudio;
    public AudioClip clip;    
    void OnEnable()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        toggleAudio.transform.SetAsLastSibling();
        toggleAudio.isOn = false;

        GetComponent<AudioSource>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = clip;

        toggleAudio.onValueChanged.AddListener(delegate { ReproducirAudio(); });
        pause.onClick.AddListener(delegate { PausaLayaout(true); });

        duracionAudio = GetComponent<AudioSource>().clip.length;

    }

    public void PausaLayaout(bool state)
    {
        if (state)
            _controlAudio.PlayAudio(0);

        GetComponent<AudioSource>().Stop();
        toggleAudio.isOn = false;
        
        toggleAudio.transform.SetAsLastSibling();
    }

    void ReproducirAudio()
    {
        GetComponent<AudioSource>().clip = clip;

        if (toggleAudio.isOn)
        {
            _controlAudio.PlayAudio(0);

            pause.transform.SetAsLastSibling();
            other.PausaLayaout(false);//Desactiva el audio del audioClip
            if (!GetComponent<AudioSource>().isPlaying) // Solo reproduce si no lo esta haciendo
            {               
                GetComponent<AudioSource>().Play(); // Reproduce el audio
            }
        }
        else
        {
            toggleAudio.transform.SetAsLastSibling();
            GetComponent<AudioSource>().Stop();
        }

    }

    private void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        ReproducirAudio();
    }

    private void Update()
    {
        if (GetComponent<AudioSource>().time >= (duracionAudio - 0.1f)) // Cuanto termina el audio del layout
        {
            PausaLayaout(false);
        }
    }
}
