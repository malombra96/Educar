using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08A050T_BehaviourCollisionPlayer : MonoBehaviour
{
    public M08A050T_ManagerTrail _ManagerTrail;
    public M08A050T_ManagerMountain _ManagerMountain;

    public M08A050T_ManagerCascade _ManagerCascade;

    ControlNavegacion _ControlNavegacion;

    [Header("Setup Audio")]

    AudioSource _audioSource;
    public AudioClip _soundWater;
    public AudioClip _soundWin;

    void Start() => _audioSource = GetComponent<AudioSource>();

    void LateUpdate()
    {
        _ManagerTrail = FindObjectOfType<M08A050T_ManagerTrail>();
        _ManagerMountain = FindObjectOfType<M08A050T_ManagerMountain>();
        _ManagerCascade = FindObjectOfType<M08A050T_ManagerCascade>();

        _ControlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name.Contains("Ray"))
            _ManagerTrail.SetQuestion(int.Parse(col.name.Split('-')[1]),col.gameObject);
        else if(col.name.Contains("Water"))
        {
            _audioSource.Stop();

            if(!_audioSource.isPlaying)
                _audioSource.PlayOneShot(_soundWater);

            if(_ManagerTrail)
                StartCoroutine(RestartPlayer(new Vector3(-18.5f,1.5f,transform.position.z)));
            else if(_ManagerMountain)
                StartCoroutine(RestartPlayer(new Vector3(12,0.2f,transform.position.z)));
            else if(_ManagerCascade)
                StartCoroutine(RestartPlayer(new Vector3(12.1f,6,transform.position.z)));
        }
        else if(col.name.Contains("Rock"))
            _ManagerMountain.SetQuestion(int.Parse(col.name.Split('-')[1]),col.gameObject);
        else if(col.name.Contains("Trunk"))
            _ManagerCascade.SetQuestion(int.Parse(col.name.Split('-')[1]),col.gameObject);
        else if(col.name.Contains("Treasure"))
            StartCoroutine(VictoryGame());
            
            
    }

    IEnumerator VictoryGame()
    {
        GetComponent<M08A050T_BehaviourPlayer>().enabled = false;
        GetComponent<Animator>().Play("Victory");
        _audioSource.PlayOneShot(_soundWin);
        yield return new WaitForSeconds(4);
        GetComponent<Animator>().enabled = false;
        _ControlNavegacion.Forward();
        
    }

    public IEnumerator RestartPlayer(Vector3 destination)
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<M08A050T_BehaviourPlayer>().enabled = false;
        transform.position = destination;


         yield return new WaitForSeconds(.2f);
        GetComponent<Animator>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<M08A050T_BehaviourPlayer>().enabled = true; 
    }
}
