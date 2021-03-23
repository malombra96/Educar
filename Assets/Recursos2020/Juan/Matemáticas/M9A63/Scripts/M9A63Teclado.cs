using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M9A63Teclado : MonoBehaviour
{
    public GameObject numeros;
    public GameObject simbolos, imagen;
    ControlAudio controlAudio;    
    [HideInInspector] public InputField inputField;
    Button BotonBorrar;
    [HideInInspector] public M9A63ManagerInputField _managerInputField;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();

        BotonBorrar = transform.GetChild(2).GetComponent<Button>();
        BotonBorrar.onClick.AddListener(borrar);
        BotonBorrar.gameObject.SetActive(false);        

        for (int x = 0; x < numeros.transform.childCount; x++)
            numeros.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { tecla(numeros.transform); });
        

        for (int x = 0; x < simbolos.transform.childCount; x++)
            simbolos.transform.GetChild(x).GetComponent<Toggle>().onValueChanged.AddListener(delegate { tecla(simbolos.transform); });

        apagarTeclado();
    }    
    void borrar()
    {
        controlAudio.PlayAudio(0);
        if (inputField.GetComponent<M9A63BehaviourInputField>().infinito)
            Destroy(inputField.GetComponent<M9A63BehaviourInputField>().infinito);

        inputField.text = null;
        inputField.GetComponent<M9A63BehaviourInputField>()._isEmpty = true;
        _managerInputField.SetStateValidarBTN();
    }
    void tecla(Transform p)
    {        
        for (int x = 0; x < p.childCount; x++)
        {
            if (p.GetChild(x).GetComponent<Toggle>().isOn)
            {               
                controlAudio.PlayAudio(0);
                p.GetChild(x).GetComponent<Toggle>().isOn = false;
                inputField.transform.GetChild(1).GetComponent<Text>().color = new Color32(0, 0, 0, 255);
                inputField.text = p.GetChild(x).gameObject.name;

                if (x == 19 || x == 20)
                {
                    inputField.transform.GetChild(1).GetComponent<Text>().color = new Color32(0, 0, 0, 0);
                    GameObject temp;
                    if (!inputField.GetComponent<M9A63BehaviourInputField>().infinito)
                    {
                        temp = Instantiate(imagen, transform);
                        inputField.GetComponent<M9A63BehaviourInputField>().infinito = temp;
                        temp.name = "temporal";
                        temp.SetActive(true);
                        temp.GetComponent<RectTransform>().anchoredPosition = new Vector2
                            (inputField.GetComponent<RectTransform>().anchoredPosition.x, temp.GetComponent<RectTransform>().anchoredPosition.y);
                        
                    }
                    else
                        temp = inputField.GetComponent<M9A63BehaviourInputField>().infinito;

                    if (x == 19)
                    {
                        temp.GetComponent<Image>().sprite = temp.GetComponent<BehaviourSprite>()._default;                       
                    }
                    else if (x == 20)
                    {
                        temp.GetComponent<Image>().sprite = temp.GetComponent<BehaviourSprite>()._selection;                        
                    }

                    temp.GetComponent<Image>().SetNativeSize();
                }
                else
                {
                    if (inputField.GetComponent<M9A63BehaviourInputField>().infinito)
                        Destroy(inputField.GetComponent<M9A63BehaviourInputField>().infinito);
                }                              
                break;
            }
        }
    }
    public void apagarTeclado()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Activar(bool numerico)
    {
        transform.GetChild(numerico ? 0 : 1).gameObject.SetActive(true);
        transform.GetChild(numerico ? 0 : 1).GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(inputField.GetComponent<RectTransform>().anchoredPosition.x,
            transform.GetChild(numerico ? 0 : 1).GetComponent<RectTransform>().anchoredPosition.y);
        numerico = !numerico;
        transform.GetChild(numerico ? 0 : 1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
