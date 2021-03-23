using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M1L70botonAnimacion : MonoBehaviour
{
    public GameObject[] animatorsParaDesactivar;
    public GameObject cajaCerrada, cajaAbierta;
    ControlAudio controlAudio;
    public string nombreDelParametro;
    public Animator animatorParaActivar;
    public Button boton;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        boton.onClick.AddListener(DesactivarAnimaciones);

    }

    void DesactivarAnimaciones()
    {
        controlAudio.PlayAudio(0);
        for (int i = 0; i < animatorsParaDesactivar.Length; i++)
        {
            animatorsParaDesactivar[i].SetActive(false);            
            //Destroy(animatorsParaDesactivar[i]);            
        }
        animatorParaActivar.SetBool(nombreDelParametro, true);
        
    }    
    public void resetAll()
    {
        animatorParaActivar.Rebind();
        //animatorParaActivar.SetBool(nombreDelParametro, false);
        cajaCerrada.SetActive(true);
        cajaAbierta.SetActive(false);
    }
}
