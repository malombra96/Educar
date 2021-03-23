using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;


public class M8L40_group : MonoBehaviour
{
    // Start is called before the first frame update

    public List<InputField> inputs;
    public Button validar;
    public bool calificado;
    public int contador;

    void Start()
    {   
    }

    void Update()
    {        
    }

    public void SetStateValidarBTN(bool value)
    {
        if (value)
        {
            contador++;
        }
        else {
            contador--;
        }


        if (contador == inputs.Count)
        {
            validar.interactable = true;
        }
        else {
            validar.interactable = false;
        }
    }
}
