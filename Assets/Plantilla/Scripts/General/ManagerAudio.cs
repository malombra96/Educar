using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ManagerAudio : MonoBehaviour
{
    float duracionAudio;
    public Button pause;
    public Toggle toggleAudio;
    public AudioClip clip;

    
    private void OnEnable()
    {
        pause.gameObject.SetActive(true);
        toggleAudio.gameObject.SetActive(false);
        
        GetComponent<AudioSource>();
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = clip;
        
        toggleAudio.onValueChanged.AddListener(delegate { ReproducirAudio(); });
        
    }

    private void OnDisable()
    {
        toggleAudio.isOn = false;
        toggleAudio.GetComponent<Image>().sprite = toggleAudio.GetComponent<BehaviourSprite>()._default;
    }

    // Start is called before the first frame update
    void Start()
    {
        pause.onClick.AddListener(delegate { PausaLayaout();});    
        duracionAudio = GetComponent<AudioSource>().clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        //print(duracionAudio + " - " + _parent.GetComponent<AudioSource>().time);
        if (GetComponent<AudioSource>().time >= (duracionAudio - 0.1f) ) // Cuanto termina el audio del layout
        {
            PausaLayaout();
        }
       
 
        if (GetComponent<AudioSource>().time >= clip.length - 0.1f )
        {
            toggleAudio.isOn = false;
            toggleAudio.GetComponent<Image>().sprite = toggleAudio.GetComponent<BehaviourSprite>()._default;
        }

        #region Control AudioSource Layout [Parent]

        if (!GetComponent<AudioSource>().enabled || !GetComponent<AudioSource>().isPlaying )
        {
            toggleAudio.gameObject.SetActive(true);
            pause.gameObject.SetActive(false);            
        }  

        #endregion

        #region Control AudioSourceToogle [Enunciado]

        if (!GetComponent<AudioSource>().isPlaying)
        {
            toggleAudio.isOn = false;
            toggleAudio.GetComponent<Image>().sprite = toggleAudio.GetComponent<BehaviourSprite>()._default;
        }

        #endregion


    }

    public void PausaLayaout()
    {
        GetComponent<AudioSource>().Stop();
        toggleAudio.gameObject.SetActive(true);
        pause.gameObject.SetActive(false);
    }
    
    public void ReproducirAudio()
    {
        if (toggleAudio.isOn)
        {
            toggleAudio.GetComponent<Image>().sprite = toggleAudio.GetComponent<BehaviourSprite>()._selection;
            
            if (!GetComponent<AudioSource>().isPlaying) // Solo reproduce si no lo esta haciendo
            { 
                GetComponent<AudioSource>().Play(); // Reproduce el audio
            
            }
        }
        else
        {
            toggleAudio.GetComponent<Image>().sprite = toggleAudio.GetComponent<BehaviourSprite>()._default;
            GetComponent<AudioSource>().Stop();
        }


    }
    
     
    

    
    
}
