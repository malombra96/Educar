using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyAlphaPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Key Down")] public bool state;
    [Header("Key Hold")] public bool hold;
    private Button _subKey;

    private void Start()
    {
        hold = false;
        state = false;
        _subKey =  transform.GetChild(1).GetComponent<Button>(); // Obtiene subtecla para tildes
        _subKey.gameObject.SetActive(false); // Desactiva subtecla
    }

    /// <summary>
    /// Tecla presionada [Solo para las vocales]
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        state = true;
        //print(gameObject.name+" "+state);
        StartCoroutine("CheckKey");
    }

    /// <summary>
    /// Termino presion sobre tecla [Solo para las vocales]
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        state = false;
    }

    /// <summary>
    /// Evalua si la tecla aun esta presionada, para abrir menu subteclas
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckKey()
    {
        yield return new WaitForSeconds(.3f);

        if (state)
        {
            hold = true;
            _subKey.gameObject.SetActive(true);
            StartCoroutine("DisableKey");

        }

    }

    /// <summary>
    /// Cortina para cerrar automicamente el menu subteclas
    /// </summary>
    /// <returns></returns>
    IEnumerator DisableKey()
    {
        yield return new WaitForSeconds(1);
        hold = false;
        _subKey.gameObject.SetActive(false);
    }
}
