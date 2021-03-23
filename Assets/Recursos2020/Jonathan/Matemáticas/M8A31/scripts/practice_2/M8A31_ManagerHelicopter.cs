using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_ManagerHelicopter : MonoBehaviour
{
    public List<GameObject> _firemanObjects;

    M8A31_helicopter _player;

    ControlAudio audio;

    ControlPuntaje puntaje;

    ControlNavegacion navegacion;

    bool firstTime;

    public GameObject collider2;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        _player = GameObject.FindObjectOfType<M8A31_helicopter>();
        audio = GameObject.FindObjectOfType<ControlAudio>();
        puntaje = GameObject.FindObjectOfType<ControlPuntaje>();
        navegacion = GameObject.FindObjectOfType<ControlNavegacion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QualifyFireman(GameObject firemanObject, GameObject helicopter) {
        collider2.SetActive(false);
            StartCoroutine(MoveHelipcopter(firemanObject,helicopter));
    }

    IEnumerator MoveHelipcopter(GameObject firemanObject, GameObject helicopter) {

        yield return new WaitForSeconds(0.5f);
        

        if (firemanObject.GetComponent<M8A31_fireman>().isRight)
        {
            puntaje.IncreaseScore();
            audio.PlayAudio(1);
            firemanObject.GetComponent<SpriteRenderer>().sprite = firemanObject.GetComponent<M8A31_fireman>()._sprites[2];
            helicopter.GetComponent<M8A31_helicopter>().anim = true;
            helicopter.GetComponent<Animator>().Play("helicopterRight");
        }
        else {
            audio.PlayAudio(2);
            firemanObject.GetComponent<SpriteRenderer>().sprite = firemanObject.GetComponent<M8A31_fireman>()._sprites[2];
            helicopter.GetComponent<M8A31_helicopter>().anim = true;
            helicopter.GetComponent<Animator>().Play("helicopterWrong");
        }

        navegacion.Forward(8);

    }

    public void ResetAll()
    {
        if (firstTime) {
            collider2.SetActive(true);
            puntaje.resetScore();
            foreach (var item in _firemanObjects)
            {
                item.GetComponent<SpriteRenderer>().sprite = item.GetComponent<M8A31_fireman>()._sprites[0];
            }

            _player.GetComponent<Animator>().Play("helipcoterIdle");
            _player.canMove = true;
            _player.anim = false;
            _player.transform.position = _player.initialPosition;
            _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            _player.buttonSeleccionar.GetComponent<Button>().interactable = true;
            _player.transform.localScale = Vector3.one;
        }
        
    }
}
