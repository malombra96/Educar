using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ControlInputName : MonoBehaviour
{
    ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;
    ControlRevision _controlRevision;

    BehaviourScreenShot _behaviourScreenShot;

    [Header("Deskop Object")] public GameObject GetName;
    [Header("Mobile Object")] public GameObject KeyPad;
    
    [Header("Text-Desempeño")]
    public Text date;
    public Text name;

    [Header("Function Buttons")] 
    public Button _restart;
    public Button _print;
    public Button _review;

    [Header("Go to a different layout in review")] public bool _needGoTo;
    [Tooltip("Ingrese el index")] public int _ToIndex;

    Button _save;

    public void Start()
    {
        if (!Application.isMobilePlatform)
        {
            GetName.SetActive(true);
            KeyPad.SetActive(false);
        }
        else 
        {
            GetName.SetActive(false);
            KeyPad.SetActive(true);
        }

        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlRevision = FindObjectOfType<ControlRevision>();
        _behaviourScreenShot = FindObjectOfType<BehaviourScreenShot>();

        date.text = DateTime.Now.ToString("MM/dd/yyyy");
        _save = GetName.transform.GetChild(0).GetComponent<Button>();
        _save.onClick.AddListener(SaveName);

        _restart.onClick.AddListener(FunctionRestart);
        _print.onClick.AddListener(FunctionPrint);

        if(_needGoTo)
            _review.onClick.AddListener(delegate{FunctionReview(_ToIndex);});
        else
            _review.onClick.AddListener(FunctionReview);

        
    }

    void Update()
    {
        if (name.text =="")
        {
            _save.gameObject.SetActive(false);
        }
        else
        {
            _save.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.Return))
            {
                GetName.SetActive(false);
                   
            }

        }
    }

    void SaveName()
    {
        _controlAudio.PlayAudio(0);
         GetName.SetActive(false);
    }

    void FunctionReview()
    {
        _controlAudio.PlayAudio(0);
        _controlNavegacion.GoToInicialAplico();
        _controlRevision.SetLayoutStateReview();
        _controlRevision._state = true;
    }

    void FunctionReview(int index)
    {
        _controlAudio.PlayAudio(0);
        _controlNavegacion.GoToLayout(index);
        _controlRevision.SetLayoutStateReview();
        _controlRevision._state = true;
    }


    void FunctionRestart()
    {
        _controlAudio.PlayAudio(0);
        _controlNavegacion.reiniciarEscena("0_1");
    }

    void FunctionPrint()
    {
        _controlAudio.PlayAudio(0);
        _behaviourScreenShot.TakeScreenShoot();

    }


}