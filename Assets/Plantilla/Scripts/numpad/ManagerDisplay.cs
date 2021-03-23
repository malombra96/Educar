using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ManagerDisplay : MonoBehaviour
{
    Button botonBorrar;
    Text _display;
    [HideInInspector] public int limiteCaracteres;
    [HideInInspector] public string texto;
    /* [HideInInspector] */ public InputField currentInput; 
    ControlAudio _controlAudio;
    
  

    private void Start()
    {
        _display = transform.GetChild(0).GetComponent<Text>();
        botonBorrar = transform.GetChild(1).GetComponent<Button>();
        botonBorrar.onClick.AddListener(delegate { BorrarContenido(); });
        _controlAudio = FindObjectOfType<ControlAudio>();
    }

    public void AddCharacter(string character)
    {
        if (texto.Length < limiteCaracteres)
        {
            texto = String.Concat(texto,character);
            _display.text = texto;
            currentInput.text = texto;

            if(currentInput.GetComponent<BehaviourInputField>())
            {
                if(currentInput.GetComponent<BehaviourInputField>()._isMath)
                    currentInput.GetComponent<BehaviourInputField>().SetStateEmpty(currentInput.text);
                else
                    currentInput.GetComponent<BehaviourInputField>().SetEmptyStandard(currentInput.text);
            }
                
        }
    }


    public void AddSymbol()
    {
        if (texto.Length > 0)
        {
            char[] characters = texto.ToCharArray();

            if (Char.IsDigit(characters[0]) || characters[0] == '(' )
            {
                string textoTemp = "";

                char[] characterTemp = new char[characters.Length + 1];

                characterTemp[0] = '-';
                textoTemp = String.Concat(textoTemp, characterTemp[0]);

                for (int i = 1; i < characterTemp.Length; i++)
                {
                    characterTemp[i] = characters[i - 1];
                    textoTemp = String.Concat(textoTemp, characterTemp[i]);

                }
                texto = textoTemp;
                _display.text = texto;
                currentInput.text = texto;
            }

        }
    }

    public void BorrarContenido()
    {
        _controlAudio.PlayAudio(0);

        if (texto.Length > 0)
        {
            texto= texto.Remove(texto.Length - 1);
            currentInput.text = texto;
            
            if(currentInput.GetComponent<BehaviourInputField>())
            {
                if(currentInput.GetComponent<BehaviourInputField>()._isMath)
                    currentInput.GetComponent<BehaviourInputField>().SetStateEmpty(currentInput.text);
                else
                    currentInput.GetComponent<BehaviourInputField>().SetEmptyStandard(currentInput.text);
            }
        }
    }

    private void Update()
    {  
        _display.text = texto;
    }

    private void OnDisable()
    {
        _display.text = "";
        texto = "";
        currentInput = null;
    }

    private void OnEnable()
    {
        StartCoroutine(reasignar());
    }

     IEnumerator reasignar()
    {
       yield return new WaitForSeconds(0.09f);
       
       if(currentInput)
        texto = currentInput.text; 
    }
}
