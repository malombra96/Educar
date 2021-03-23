using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ControlAudio : MonoBehaviour
{
    
    AudioSource _audioSource; //Componente de audio
    [Header("Audios de clic / correcto / Incorrecto")] [SerializeField] private AudioClip[] _audioClips; //Audios a reproducir

    //Delay al reproducir un audio


    private void Awake() {
        _audioSource = GetComponent<AudioSource>(); // Inicializa el componente
    }

    /// <summary>
    /// Reproduce el sonido en index una sola vez
    /// </summary>
    /// <param name="index">Indice del audio</param>
    public void PlayAudio(int index) {
        StartCoroutine(PlayAudiowithOutDelay(index));
    }

    /// <summary>
    /// Reproduce un sonido con delay
    /// </summary>
    /// <param name="index">indice del audio</param>
    /// <returns></returns>

    private IEnumerator PlayAudiowithDelay(int index,float delay) {
        //Verifica que el audio a repoducir no sea sonido de pop
        _audioSource.Stop();
        if (index == 0) {
            _audioSource.PlayOneShot(_audioClips[index]);
        }
        //Resto de sonidos
        else {
            yield return new WaitForSeconds(delay);
            _audioSource.PlayOneShot(_audioClips[index]);
        }
    }
    
    private IEnumerator PlayAudiowithOutDelay(int index) {
        //Verifica que el audio a repoducir no sea sonido de pop
        _audioSource.Stop();
        if (index == 0) {
            _audioSource.PlayOneShot(_audioClips[index]);
        }
        //Resto de sonidos
        else {
            yield return new WaitForSeconds(0);
            _audioSource.PlayOneShot(_audioClips[index]);
        }
    }

    /// <summary>
    /// Detiene el sonido del AudioSource en este componente
    /// </summary>
    public void StopAudio() {
        _audioSource.Stop();
    }
}
