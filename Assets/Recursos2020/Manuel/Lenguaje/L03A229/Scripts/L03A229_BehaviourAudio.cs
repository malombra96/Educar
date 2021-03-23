using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class L03A229_BehaviourAudio : MonoBehaviour
{
    ControlAudio _controlAudio;
    AudioSource _audio;
    
    [HideInInspector]  public float duracionAudio;
    public Button pause;
    public Toggle toggleAudio;
    public AudioClip clip;
    
    void OnEnable()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _audio = GetComponent<AudioSource>();

        toggleAudio.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
        
        toggleAudio.isOn = false;
        
        _audio.playOnAwake = false;
        _audio.clip = clip;
        
        toggleAudio.onValueChanged.AddListener(delegate { ReproducirAudio(); });
        pause.onClick.AddListener(delegate { PauseAudio(true);});  
        
    }

    private void LateUpdate()
    {
        if(_audio.time >= (duracionAudio - 0.1f))
        {
            PauseAudio(false);
        }
    }
    
    public void PauseAudio(bool state)
    {
        if (state)
            _controlAudio.PlayAudio(0);
        
        _audio.Stop();
        toggleAudio.isOn = false;
        toggleAudio.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);  
        
    }

    void ReproducirAudio()
    {
        _audio.clip = clip;

        if (toggleAudio.isOn)
        {
            _controlAudio.PlayAudio(0);
            
            toggleAudio.gameObject.SetActive(false);
            pause.gameObject.SetActive(true);
            
            if (!_audio.isPlaying) // Solo reproduce si no lo esta haciendo
            { 
                _audio.Play(); // Reproduce el audio
            }
        }
        else
        {
            toggleAudio.gameObject.SetActive(true);
            pause.gameObject.SetActive(false);
            _audio.Stop();
        }
    }
}
