using System;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_controlInput : MonoBehaviour
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
    public Button _review;
    public Button _print;

    Button _save;
    int x;
    public void Start()
    {
        x = PlayerPrefs.GetInt("review");

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

        _review.onClick.AddListener(FunctionReview);
        _restart.onClick.AddListener(FunctionRestart);
        _print.onClick.AddListener(FunctionPrint);


    }

    void Update()
    {
        if (x == 0)
        {
            if (name.text == "")
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
        else {
            GetName.SetActive(false);
            KeyPad.SetActive(false);
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


    void FunctionRestart()
    {
        _controlAudio.PlayAudio(0);
        _controlNavegacion.reiniciarEscena(0_1);
    }

    void FunctionPrint()
    {
        _controlAudio.PlayAudio(0);
        _behaviourScreenShot.TakeScreenShoot();

    }
}
