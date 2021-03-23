using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BehaviourAudioClip : MonoBehaviour
{
    ControlAudio _controlAudio;
    
    float duracionAudio;
    public Button pause;
    public Toggle toggleAudio;
    public AudioClip clip;
    
    void OnEnable()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        toggleAudio.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        
        toggleAudio.isOn = false;
        
        GetComponent<AudioSource>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = clip;
        
        toggleAudio.onValueChanged.AddListener(delegate { ReproducirAudio(); });
        pause.onClick.AddListener(delegate { PausaLayaout(true);});  
        
        duracionAudio = GetComponent<AudioSource>().clip.length;
        
    }
    
    void PausaLayaout(bool state)
    {
        if (state)
            _controlAudio.PlayAudio(0);
        
        GetComponent<AudioSource>().Stop();
        toggleAudio.isOn = false;
        toggleAudio.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);  
        
    }

    void ReproducirAudio()
    {
        GetComponent<AudioSource>().clip = clip;

        if (toggleAudio.isOn)
        {
            _controlAudio.PlayAudio(0);
            
            toggleAudio.gameObject.SetActive(false);
            pause.gameObject.SetActive(true);
            
            if (!GetComponent<AudioSource>().isPlaying) // Solo reproduce si no lo esta haciendo
            { 
                GetComponent<AudioSource>().Play(); // Reproduce el audio
            }
        }
        else
        {
            toggleAudio.gameObject.SetActive(true);
            pause.gameObject.SetActive(false);
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
        if (GetComponent<AudioSource>().time >= (duracionAudio - 0.1f) ) // Cuanto termina el audio del layout
        {
            PausaLayaout(false);
        }
    }
}
