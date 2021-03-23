using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3A230ManagerDragDropTexto : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    public Button validar;
    public Color32 correcto;
    public Color32 incorrecto;

    [HideInInspector] public List<L3A230DropTexto> dropsTexto;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        validar.onClick.AddListener(calificar);
        StartCoroutine(activarValidar());
    }
    public IEnumerator activarValidar()
    {
        yield return new WaitForSeconds(0.1f);
        int right = 0;

        foreach(var drop in dropsTexto)
        {
            if (drop.transform.GetChild(0).GetComponent<Text>().text != "_")
                right++;
        }
        validar.interactable = dropsTexto.Count == right;
    }
    void calificar()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        int right = 0;
        foreach (var drop in dropsTexto)
        {
            drop.GetComponent<Image>().raycastTarget = false;
            if (drop.correcto)
                right++;

            drop.texto.color = (drop.correcto ? correcto : incorrecto);
        }

        controlPuntaje.IncreaseScore(right);
        controlAudio.PlayAudio(dropsTexto.Count == right ? 1 : 2);
        controlNavegacion.Forward(2);
    }
    public void resetAll()
    {
        validar.interactable = false;
        foreach (var drop in dropsTexto)
        {
            drop.correcto = false;
            drop.GetComponent<Image>().raycastTarget = true;
            drop.texto.text = "_";
            drop.texto.color = new Color32(0, 0, 0, 255);
            drop.texto.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
