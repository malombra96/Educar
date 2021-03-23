using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08A050T_ManagerMountain : MonoBehaviour
{
    ControlNavegacion _ControlNavegacion;
    M08A050T_BehaviourPlayer _player;

    [Header("Elements Activity")]
    public GameObject[] _question = new GameObject[5];
    List<GameObject> _colliders = new List<GameObject>();
    public Image[] _hearts;
    int lifes;

    GameObject collision;

    [Header("Mobile")] public GameObject _mobileControllers;

    [Header("Revision Panel")] public GameObject _revision;

    public bool once = false;
    void OnEnable()
    {
        if (!GetComponent<BehaviourLayout>()._isEvaluated && !once)
        {
            _player = FindObjectOfType<M08A050T_BehaviourPlayer>();
            _ControlNavegacion = FindObjectOfType<ControlNavegacion>();
            lifes = _hearts.Length - 1;
            _player._active = true;

            _mobileControllers.SetActive(Application.isMobilePlatform);

            _revision.SetActive(false);

        }
        else if(!once)
        {
            _revision.SetActive(true);

            for (int i = 0; i < _question.Length; i++)
            {
                _question[i].transform.GetChild(0).SetParent(_revision.transform.GetChild(i));
                _question[i].transform.GetChild(0).SetParent(_revision.transform.GetChild(i));
                _question[i].SetActive(true);
            }
            
            for (int i = 0; i < _revision.transform.childCount-1; i++)
            {
                _revision.transform.GetChild(i).transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(-375,0);
                _revision.transform.GetChild(i).transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(3,3,3);


                _revision.transform.GetChild(i).transform.GetChild(1).GetComponent<RectTransform>().anchorMin = new Vector2(.5f,.5f);
                _revision.transform.GetChild(i).transform.GetChild(1).GetComponent<RectTransform>().anchorMax = new Vector2(.5f,.5f);

                _revision.transform.GetChild(i).transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(590,0);
                _revision.transform.GetChild(i).transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(2.5f,2.5f,2.5f);
            }

            once = true;
        }
    } 
    void OnDisable()
    {
        if(_player)
            _player._active = false;
    } 

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
        if(!answer)
            SubtractLife();

        SetTriangleAnswer(_question[index].transform.GetChild(1).GetComponent<Image>(),answer,index);

        if(lifes >= 0)
            StartCoroutine(EndQuestion(index));
        else
            ResetALL();
            
            //EndGame();
    }

    void SetTriangleAnswer(Image triangle, bool state, int index)
    {
        triangle.sprite = state ?
        triangle.GetComponent<BehaviourSprite>()._right :
        triangle.GetComponent<BehaviourSprite>()._wrong;

        collision.GetComponent<BoxCollider2D>().enabled = false;

        collision = null;
    }

    void SubtractLife()
    {
        Color c = _hearts[lifes].color;
        _hearts[lifes].color = new Color32((byte) c.r,(byte) c.g,(byte) c.b,100); 
        lifes--;

        if(lifes < 0)
            ResetALL();

            //EndGame();
        
    }

/*     void EndGame()
    {
        _player.GetComponent<Animator>().enabled = false;
        _player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-3);
        _player.enabled = false;

        StartCoroutine(ResetActivity());
    } */

    IEnumerator EndQuestion(int index)
    {
        yield return new WaitForSeconds(1);
        _question[index].SetActive(false);
        StartCoroutine(EnabledPlayer(true));

        if((_question.Length-1) == index)
            _ControlNavegacion.Forward(1);
    }

    public IEnumerator EnabledPlayer(bool state)
    {
        yield return new WaitForSeconds(.2f);
        _player.GetComponent<Animator>().enabled = state;
        _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _player.enabled = state;
    }  

/*     IEnumerator ResetActivity()
    {
        yield return new WaitForSeconds(2);

        lifes = _hearts.Length-1;

        foreach (var heart in _hearts)
        {
            Color c = heart.color;
            heart.color = Color.white; 
        }

        foreach (var collider in _colliders)
            collider.GetComponent<BoxCollider2D>().enabled = true;

        foreach (var quest in _question)
        {
            for (int i = 0; i < quest.transform.childCount; i++)
            {
                quest.GetComponent<M08A050T_ManagerSeleccionarToggle>().ResetSeleccionarToggle();

                if(i==1)
                    quest.transform.GetChild(i).GetComponent<Image>().sprite = quest.transform.GetChild(i).GetComponent<BehaviourSprite>()._default;
            }

            quest.SetActive(false);
        }
        
        StartCoroutine(EnabledPlayer(true));

        _player.transform.position = new Vector3(12.3f,_player.transform.position.y,_player.transform.position.z);

        
    } */

    public void  ResetALL()
    {
        lifes = _hearts.Length-1;

        foreach (var heart in _hearts)
        {
            Color c = heart.color;
            heart.color = Color.white; 
        }

        foreach (var collider in _colliders)
            collider.GetComponent<BoxCollider2D>().enabled = true;

        foreach (var quest in _question)
        {
            for (int i = 0; i < quest.transform.childCount; i++)
            {
                quest.GetComponent<M08A050T_ManagerSeleccionarToggle>().ResetSeleccionarToggle();

                if(i==1)
                    quest.transform.GetChild(i).GetComponent<Image>().sprite = quest.transform.GetChild(i).GetComponent<BehaviourSprite>()._default;
            }

            quest.SetActive(false);
        }

        if(_player)
            _player.transform.position = new Vector3(12.3f,_player.transform.position.y,_player.transform.position.z);

        
    }

}
