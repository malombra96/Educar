using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M10L001_ManagerDragDrop : MonoBehaviour
{   
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas; 
    
    [Header("Item List")]
    [HideInInspector] public List<M10L001_BehaviourDrag> _drags;
    [HideInInspector] public List<M10L001_BehaviourDrop> _drops;
    /* [HideInInspector] */ public int dropCount;

    [Header("Stats Vectores Collision")]
    int dropHead;  int dropTail; int dropHeadTail; int dropDoubleHead; int dropDoubleTail;

    [Header("Button Validar")] public Button _validar;
    [Header("Boton Trazar Vector")] public Button _trazar;

    [Header("Vector Resultado")] public GameObject _resultVector;

    public enum OperatingMethod
    {
        Default,
        Match,
        Group,
        Create
    }

    [Header("Metodo de operación")] public OperatingMethod _OperatingMethod;
    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        canvas = FindObjectOfType<Canvas>();

        _validar.gameObject.SetActive(false);

        _trazar.onClick.AddListener(GetStatsVectorsDrop);
        
        _resultVector.SetActive(false);

        StateBtnTrazar();

        StartCoroutine(GetDefaultDropCount());
        
    }

    IEnumerator GetDefaultDropCount()
    {
        yield return new WaitForSeconds(.5f);
        dropCount = _drags.Count;
    }
    
    public void StateBtnTrazar() => _trazar.interactable = (dropCount >= 4);

    void GetStatsVectorsDrop()
    {
        _controlAudio.PlayAudio(0);

        int dropEvaluated = 0;
        dropHead = 0; dropTail = 0; dropHeadTail = 0; dropDoubleHead = 0; dropDoubleTail = 0;

        foreach (M10L001_BehaviourDrop drop in _drops)
        {
            drop.typeCollision = "";
            
            if(drop._hasCollision)
            {
                dropEvaluated++;

                switch (GetTypeCollision(drop))
                {
                    case "onlyHead": dropHead++; break;
                    case "onlyTail": dropTail++; break;
                    case "Head&Tail": dropHeadTail++; break;
                    case "doubleHead": dropDoubleHead++; break;
                    case "doubleTail": dropDoubleTail++; break;
                }
            }
        }

        print($" Size{dropEvaluated} => #Head={dropHead},#Tail={dropTail},#Head&Tail{dropHeadTail} #DoubleHead={dropDoubleHead},#DoubleTail={dropDoubleTail}");
        SetResultVector(dropEvaluated);
    }

    string GetTypeCollision(M10L001_BehaviourDrop drop)
    {   
        if(drop.listCollision._collision.Count == 1)
        {
            if (drop.listCollision._collider[0].GetType() == typeof(BoxCollider2D))
                drop.typeCollision = "onlyHead";
            else if(drop.listCollision._collider[0].GetType() == typeof(CircleCollider2D))
                drop.typeCollision = "onlyTail";
        }
        else
        {
            System.Type A = drop.listCollision._collider[0].GetType();
            System.Type B = drop.listCollision._collider[1].GetType();

            if(A == typeof(BoxCollider2D) && B == typeof(BoxCollider2D))
                drop.typeCollision = "doubleHead";
            else if(A == typeof(CircleCollider2D) && B == typeof(CircleCollider2D))
                drop.typeCollision = "doubleTail";
            else
                drop.typeCollision = "Head&Tail";
        }
        
        //print($"{drop.name} is {drop.typeCollision}");
        return drop.typeCollision;
        

    }

    void SetResultVector(int dropEvaluated)
    {
        Vector2 start = new Vector2();
        Vector2 end = new Vector2();
        
        if(dropEvaluated == 3)
        {
            if(dropHead == 1 && dropTail == 1 && dropHeadTail == 1)
            {
                print("Method Head & Tail");

                foreach (M10L001_BehaviourDrop drop in _drops)
                {
                    if(drop.typeCollision == "onlyTail")
                        start = drop.GetComponent<RectTransform>().anchoredPosition;
                    else if(drop.typeCollision == "onlyHead")
                        end = drop.GetComponent<RectTransform>().anchoredPosition;
                }

                ShowResultVector(start,end);

            }
            else
                print($"Vectores mal puestos {1}");
        }
        else if(dropEvaluated == 4)
        {
            if(dropDoubleHead == 1 && dropDoubleTail == 1 && dropHeadTail == 2)
            {
                print("Method Paralelogramo");

                foreach (M10L001_BehaviourDrop drop in _drops)
                {
                    if(drop.typeCollision == "doubleTail")
                        start = drop.GetComponent<RectTransform>().anchoredPosition;
                    else if(drop.typeCollision == "doubleHead")
                        end = drop.GetComponent<RectTransform>().anchoredPosition;
                }

                ShowResultVector(start,end);
            }
            else
                print($"Vectores mal puestos {2}");
        }
        else
            print($"Vectores mal puestos {3}");
            
    }

    void ShowResultVector(Vector2 start, Vector2 end)
    {
        _trazar.interactable = false;

        foreach (M10L001_BehaviourDrag drag in _drags)
            drag.enabled = false;

        float ang;
        float ax,ay,bx,by;

        ax = start.x;
        ay = start.y;

        bx = end.x;
        by = end.y;
    
        ang = Mathf.Rad2Deg*Mathf.Atan2((by-ay),(bx-ax));
        _resultVector.transform.eulerAngles = new Vector3(0,0,ang);
        _resultVector.GetComponent<RectTransform>().anchoredPosition = start;
        _resultVector.SetActive(true);

        _validar.gameObject.SetActive(true);
    }

    public void ResetDragDropVectores()
    {
        foreach (var drag in _drags)
        {
            drag.enabled = true;
            drag._drop = null;
            drag.inDrop = false;
            drag.GetComponent<RectTransform>().anchoredPosition = drag._defaultPos;
            drag.UpdateCurrentPosition();

            if(!drag.GetComponent<M10L001_BehaviourDrag>().ItsInfinite)
                DestroyImmediate(drag.gameObject); 
        }

        _drags.RemoveAll(x => x == null);
        

        foreach (var drop in _drops)
        {
            if (drop._drag.Length > 0)
            {
                drop._drag[0] = null;
                drop._drag[1] = null;
            }


            drop._hasCollision = false;
            drop.typeCollision = "";

            drop.listCollision._collider.Clear();
            drop.listCollision._collision.Clear();
        }

        dropCount = _drags.Count;
        _resultVector.SetActive(false);
        StateBtnTrazar();
        _validar.gameObject.SetActive(false);
        
            
        
    }
}
