using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class M8L56Input : MonoBehaviour,IPointerClickHandler
{
    M8L56ControlConozco _managerInputField;
    InputField _input; 
   
    [HideInInspector] [Header("State Answer")] public bool _isRight;   


    void Awake()
    {
        _isRight = false;

        _managerInputField = FindObjectOfType<M8L56ControlConozco>();

        _input = GetComponent<InputField>();

        _input.onValueChanged.AddListener(delegate { SetEmptyStandard(_input.text); });       

        if (Application.isMobilePlatform)
            _input.contentType = InputField.ContentType.Standard;
    }

    public void SetStateEmpty(string s)
    {        

        if (s.Length == 1)
        {
            bool x = string.IsNullOrWhiteSpace(s[0].ToString());
            bool y = int.TryParse(s[0].ToString(), out int n);
            //bool z = _charactersMath.Any(c => s[0].ToString().Contains(c));

            if (x)
                _input.text = "";
            else if (!y)
                _input.text = "";

        }
        else
            CheckInput(s);


        if (Application.isMobilePlatform)
            _managerInputField._lightBox.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>().texto = _input.text;       

    }

    /// Cuando no es contenido matemático, solo verifica que no este vacio
    public void SetEmptyStandard(string s)
    {
        //_isEmpty = string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        if (Application.isMobilePlatform)
            _managerInputField._lightBox.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>().texto = _input.text;

        if (s == "")
        {
            _isRight = false;            
        }
        else
        {
            _isRight = true;
        }
    }

    void CheckInput(string s)
    {
        int value;
        string[] cs = s.ToCharArray().Select(c => c.ToString()).ToArray();

    }   

   
    //void Update()
    //{
    //    if (Application.isMobilePlatform)


    //}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _managerInputField.controlAudio.PlayAudio(0);
        if (Application.isMobilePlatform)
        {
            ManagerDisplay _managerDisplay = _managerInputField._lightBox.transform.GetChild(0).GetChild(0).GetComponent<ManagerDisplay>();

            _managerDisplay.currentInput = GetComponent<InputField>();
            _managerDisplay.limiteCaracteres = GetComponent<InputField>().characterLimit;

            _managerInputField._lightBox.SetActive(true);
            _managerInputField._lightBox.GetComponent<Animator>().Play("NumPad_in");
        }
    }
}
