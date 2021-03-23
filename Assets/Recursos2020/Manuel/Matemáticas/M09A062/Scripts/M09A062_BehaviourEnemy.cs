using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A062_BehaviourEnemy : MonoBehaviour
{
    M09A062_BehaviourLevel _managerLevel;
    M09A062_ManagerSpace _managerGeneral;
    Animator _animator;

    [Header("¿Right or wrong?")] public bool _state;
    
    [Header("Step Movement")] public int _step;

    [Header("Pause Movement")] public bool _mov;
    
    private void Start()
    {
        _managerLevel = FindObjectOfType<M09A062_BehaviourLevel>();
        _managerGeneral = FindObjectOfType<M09A062_ManagerSpace>();
        
        _mov = true;
        _animator = GetComponent<Animator>();

        transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(SetAsteroid());
    }
    public IEnumerator SetAsteroid()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 300), Random.Range(600, 2000));
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider is CircleCollider2D && collision.gameObject.GetComponent<M09A062_BehaviourEnemy>() && GetComponent<M09A062_BehaviourEnemy>())
           StartCoroutine(collision.gameObject.GetComponent<M09A062_BehaviourEnemy>().SetAsteroid());
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Bullet(Clone)")
        {
            _managerLevel._answers++;
            Destroy(other.gameObject);
            GetComponent<BoxCollider2D>().enabled = false;
            SetAnswerSprite(_state);
            StartCoroutine(EnableEnemy());

            if (_state)
            {
                _managerGeneral._controlAudio.PlayAudio(1);
                _managerGeneral._controlPuntaje.IncreaseScore();

                _animator.SetInteger("state",2);
            }
            else
            {
                _managerGeneral._nlifes--;
                _managerGeneral._controlAudio.PlayAudio(2);

                _animator.SetInteger("state",1);
            }

            _managerGeneral.SetScore();

            if(_managerGeneral._nlifes == 0)
            {
                StopAllCoroutines();
                _managerLevel.StopObjectives();
                StartCoroutine(_managerGeneral.EndGame());
            }
            else if(_managerLevel._answers == _managerLevel._correct.Count)
            {    
                _managerLevel.StopObjectives();
                _managerGeneral._controlNavegacion.Forward(2);
            }

        }
        else if(other.name == "SpaceShip")
        {
            _managerLevel.StopObjectives();

            _managerGeneral._nlifes--;
            _managerGeneral._controlAudio.PlayAudio(2);

            _managerGeneral._spaceShip.GetComponent<Animator>().SetBool("explotion",true);

            if(_managerGeneral._nlifes == 0)
            {
                StopAllCoroutines();
                _managerLevel.StopObjectives();
                StartCoroutine(_managerGeneral.EndGame());
            }
            else
                StartCoroutine(DestroySpaceShip());
        }
    }

    IEnumerator EnableEnemy()
    {
        yield return new WaitForSeconds(.8f);
        gameObject.SetActive(false);
    }

    IEnumerator DestroySpaceShip()
    {
        yield return new WaitForSeconds(1);
        _managerGeneral._controlNavegacion.Forward();
        _managerGeneral._spaceShip.GetComponent<Animator>().SetBool("explotion",false);

    }

    void Update()
    {
        if (_mov)
        {
            float x = GetComponent<RectTransform>().anchoredPosition.x;
            float y = GetComponent<RectTransform>().anchoredPosition.y;
            GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y - _step);

            if (GetComponent<RectTransform>().anchoredPosition.y < -600)
                StartCoroutine(SetAsteroid());
        }


    }

    void SetAnswerSprite(bool state)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = state?
            transform.GetChild(0).GetComponent<BehaviourSprite>()._right : 
            transform.GetChild(0).GetComponent<BehaviourSprite>()._wrong;

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
