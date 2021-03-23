using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08A050T_ManagerTrail : MonoBehaviour
{
    ControlNavegacion _ControlNavegacion;
    M08A050T_BehaviourPlayer _player;

    [Header("Mobile")] public GameObject _mobileControllers; 

    [Header("Elements Activity")]

    public GameObject[] _question = new GameObject[3];
    public GameObject _scoreRay;

    List<GameObject> _colliders = new List<GameObject>();

    GameObject collision;
    [Header("Revision Panel")] public GameObject _revision;
    public bool once = false;

    void OnEnable()
    {
        if (!GetComponent<BehaviourLayout>()._isEvaluated && !once)
        {
            _player = FindObjectOfType<M08A050T_BehaviourPlayer>();
            _ControlNavegacion = FindObjectOfType<ControlNavegacion>();
            _player._active = true;

            _mobileControllers.SetActive(Application.isMobilePlatform);

            _revision.SetActive(false);
        }
        else if(!once)
        {
            _revision.SetActive(true);

            foreach (var quest in _question)
            {
                quest.transform.SetParent(_revision.transform);
                quest.GetComponent<M08A050T_ManagerSeleccionarToggle>()._validarBTN.gameObject.SetActive(false);
                quest.SetActive(true);
            }

            _question[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-390, 120);
            _question[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(390, 120);
            _question[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -170);

            once = true;
 
        }

        
    } 
    void OnDisable() => _player._active = false;

    public void SetQuestion(int n, GameObject x)
    {
        collision = x;
        _colliders.Add(collision);
        StartCoroutine(EnabledPlayer(false));

        for (int i = 0; i < _question.Length; i++)
            _question[i].SetActive(i==n);
    }

    public void SetScore(int index, bool answer)
    {
        GameObject temp = _scoreRay.transform.GetChild(index).gameObject;

        if(answer)
            SetSpriteRay(temp,answer);

        SetCollider(answer);
        EndQuestion(index);
            
    }

    void SetCollider(bool state)
    {
        collision.GetComponent<SpriteRenderer>().sprite = state ?
        collision.GetComponent<BehaviourSprite>()._right :
        collision.GetComponent<BehaviourSprite>()._wrong;

        collision.GetComponent<PolygonCollider2D>().enabled = false;

        collision = null;
    }

    void SetSpriteRay(GameObject ray, bool state)
    {
        ray.GetComponent<Image>().sprite =  state? 
            ray.GetComponent<BehaviourSprite>()._selection : 
            ray.GetComponent<BehaviourSprite>()._default;
    }

    public void EndQuestion(int index)
    {
        _question[index].SetActive(false);
        StartCoroutine(EnabledPlayer(true));

        if((_question.Length-1) == index)
            _ControlNavegacion.Forward(1);
    }

    public IEnumerator EnabledPlayer(bool state)
    {
        yield return new WaitForSeconds(.2f);
        _player.GetComponent<Animator>().enabled = state;
        _player.enabled = state;
    } 

    

    public void ResetALL()
    {

        for (int i = 0; i < _scoreRay.transform.childCount; i++)
            _scoreRay.transform.GetChild(i).GetComponent<Image>().sprite = _scoreRay.transform.GetChild(i).GetComponent<BehaviourSprite>()._default;

        foreach (var collider in _colliders)
        {
            collider.GetComponent<SpriteRenderer>().sprite = collider.GetComponent<BehaviourSprite>()._default;
            collider.GetComponent<PolygonCollider2D>().enabled = true;

        }

        foreach (var quest in _question)
        {
            for (int i = 0; i < quest.transform.childCount; i++)
                quest.GetComponent<M08A050T_ManagerSeleccionarToggle>().ResetSeleccionarToggle();

            quest.SetActive(false);
        }
            
        if(_player)
        {
            _player.GetComponent<Animator>().enabled = true;
            _player.enabled = true;
            _player.transform.position = new Vector3(-18.5f,1.5f,_player.transform.position.z);
        }
            
    }

}
