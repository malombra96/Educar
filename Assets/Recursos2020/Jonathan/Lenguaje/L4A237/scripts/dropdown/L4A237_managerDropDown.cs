using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.UI;
public class L4A237_managerDropDown : MonoBehaviour
{
    public bool ultimo;
    public L4A237_fin _fin;
    public GameObject nadador,bg,itemBackground;
    public Vector3 posIniNadador, posIniBg;
    public bool moveBg;
    public float speed;
    ControlPuntaje _controlPuntaje;
    ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlAudio _controlAudio;

    [Header("Colors State")]
    public Color32 _rightColor;
    public Color32 _wrongColor;
    public Color32 _defaultColor;


    [HideInInspector] public List<L4A237_dropdown> _dropdowns;

    [Header("Validar")] public Button _validar;

    int correct;

    private void Start()
    {
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();

        if (_validar)
            _validar.onClick.AddListener(EvaluateDropDown);

        _validar.interactable = false;
    }

    private void Update()
    {
        if (moveBg) {
            nadador.GetComponent<Animator>().SetBool("x", true);
            StartCoroutine(x());
        }
    }

    IEnumerator x() {
        bg.GetComponent<RectTransform>().anchoredPosition += Vector2.left * Time.deltaTime * speed;
        nadador.GetComponent<RectTransform>().anchoredPosition += Vector2.left * Time.deltaTime * speed;
        yield return new WaitForSeconds(3);
        moveBg = false;
        nadador.GetComponent<Animator>().SetBool("x", false);
    }

    void EvaluateDropDown()
    {
        foreach (var dropdown in _dropdowns)
        {
            _validar.interactable = false;
            dropdown.GetComponent<Dropdown>().interactable = false;

            switch (dropdown._type)
            {
                case L4A237_dropdown.Type.text:

                    SetTextDropDown(dropdown.GetComponent<Dropdown>().captionText, dropdown._indexCurrent == dropdown._indexRight);

                    break;

                case L4A237_dropdown.Type.image:

                    SetSpriteDropDown(dropdown.GetComponent<Image>(), dropdown._indexCurrent == dropdown._indexRight);

                    break;
            }
        }

        _controlAudio.PlayAudio(correct == _dropdowns.Count ? 1 : 2);
       
        if (correct == _dropdowns.Count)
        {
            moveBg = true;
            //_controlNavegacion.Forward(5);
            _controlPuntaje.IncreaseScore();
            if (ultimo)
            {
                if (_controlPuntaje._rightAnswers == _controlPuntaje.questions)
                {
                    StartCoroutine(p());
                }
                else
                {
                    _controlNavegacion.Forward(5.0f);
                }
            }
            else
            {
                _controlNavegacion.Forward(5f);
            }
        }
        else {
            moveBg = false;
            _controlNavegacion.Forward(2.0f);
        }
        

    }

    IEnumerator p() {
        yield return new WaitForSeconds(4f);
        _fin.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _fin.gameObject.SetActive(false);
        _controlNavegacion.Forward();

    }

    /// <summary>
    /// Realiza el cambio de estado [right or wrong] para la imagen recibida
    /// </summary>
    /// <param name="select"></param>
    /// <param name="state"></param>
    void SetSpriteDropDown(Image select, bool state)
    {
        switch (state)
        {
            case true:

                select.color = _rightColor;
                correct++;
                _controlPuntaje.IncreaseScore();

                break;

            case false:

                select.color = _wrongColor;

                break;
        }
    }

    /// <summary>
    /// Realiza el cambio de estado [right or wrong] para el texto recibido
    /// </summary>
    /// <param name="select"></param>
    /// <param name="state"></param>
    void SetTextDropDown(Text select, bool state)
    {
        select.color = new Color32(255, 255, 255, 255);
        switch (state)
        {
            case true:

                
                itemBackground.GetComponent<Image>().color = _rightColor;
                correct++;
               

                break;

            case false:

                itemBackground.GetComponent<Image>().color = _wrongColor;

                break;
        }
    }

    void DefaultTextDropDown(Text select) => select.color = Color.white;

    void DefaultSpriteDropDown(Image select) => select.color = Color.white;


    /// <summary>
    /// Set State Button Validar
    /// </summary>
    public void StateBtnValidar()
    {
        foreach (var dropdown in _dropdowns)
        {
            if (dropdown.state)
                _validar.interactable = true;
            else
            {
                _validar.interactable = false;
                break;
            }

        }

    }

    public void ResetManagerDropDown()
    {
        foreach (var dropdown in _dropdowns)
        {
            var bh = dropdown.GetComponent<L4A237_dropdown>();
            var dd = dropdown.GetComponent<Dropdown>();

            dropdown.reset = true;

            dd.value = 0;
            bh._indexCurrent = dd.value;

            if (bh._type == L4A237_dropdown.Type.text)
            {
                dd.captionText.text = dd.options[0].text;
                DefaultTextDropDown(dd.captionText);
            }
            else
            {
                dd.captionImage.sprite = dd.options[0].image;
                DefaultSpriteDropDown(dd.GetComponent<Image>());
            }


            bh.state = false;
            dd.interactable = true;
            dropdown.reset = false;
            nadador.GetComponent<RectTransform>().anchoredPosition = posIniNadador;
            bg.GetComponent<RectTransform>().anchoredPosition = posIniBg;
            nadador.GetComponent<Animator>().SetBool("x",false);

        }
        itemBackground.GetComponent<Image>().color = _defaultColor;
        _validar.interactable = false;
        correct = 0;
    }
}
