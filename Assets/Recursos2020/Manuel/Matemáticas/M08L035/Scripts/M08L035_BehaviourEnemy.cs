using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L035_BehaviourEnemy : MonoBehaviour
{
    M08L035_ManagerLevel _managerLevel;
    M08L035_GeneralSpaceManager _managerGeneral;
    Animator _animator;

    [Header("¿Right or wrong?")] public bool _state;
    
    [Header("Step Movement")] public int _step;

    [Header("Pause Movement")] public bool _mov;
    
    private void Start()
    {
        _managerLevel = FindObjectOfType<M08L035_ManagerLevel>();
        _managerGeneral = FindObjectOfType<M08L035_GeneralSpaceManager>();
        
        _mov = true;
        _animator = GetComponent<Animator>();

        StartCoroutine(SetAsteroid());
    }

    public IEnumerator SetAsteroid()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-260, 600), Random.Range(600, 2000));
    }

    void OnCollisionStay2D(Collision2D collision)
    {
       if (collision.collider is CircleCollider2D && collision.gameObject.name.Contains("Enemy") == name.Contains("Enemy"))
           StartCoroutine(collision.gameObject.GetComponent<M08L035_BehaviourEnemy>().SetAsteroid());
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Bullet(Clone)")
        {
            Destroy(other.gameObject);
            _animator.SetBool("explotion",true);
            _managerLevel.StopObjectives();
            SetAnswerSprite(_state);

            if (_state)
            {
                _managerGeneral._nEnemy++;
                _managerGeneral._controlPuntaje.IncreaseScore();
                _managerGeneral._controlAudio.PlayAudio(1);
            }
            else
            {
                _managerGeneral._nlifes--;
                _managerGeneral._controlAudio.PlayAudio(2);

                if(_managerGeneral._nlifes == 0)
                    StartCoroutine(_managerGeneral.EndGame());
            }
            
            _managerGeneral._controlNavegacion.Forward(2);
        }
        else if(other.name == "SpaceShip")
        {
            _managerLevel.StopObjectives();

            _managerGeneral._nlifes--;
                _managerGeneral._controlAudio.PlayAudio(2);

            _managerGeneral._spaceShip.GetComponent<Animator>().SetBool("explotion",true);

            if(_managerGeneral._nlifes == 0)
                StartCoroutine(_managerGeneral.EndGame());
            else
                StartCoroutine(DestroySpaceShip());
        }
    }

    IEnumerator DestroySpaceShip()
    {
        yield return new WaitForSeconds(2);
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
        transform.GetChild(1).GetComponent<Image>().sprite = state?
            transform.GetChild(1).GetComponent<BehaviourSprite>()._right : 
            transform.GetChild(1).GetComponent<BehaviourSprite>()._wrong;

        transform.GetChild(1).gameObject.SetActive(true);
    }
}
