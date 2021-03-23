using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A76GameControler : MonoBehaviour
{
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    ControlAudio controlAudio;
    public bool seguirDisparando = true;
    public Recurso recurso;
    public enum Recurso
    {
        globo,
        Diana
    }
    // Start is called before the first frame update
    void Start()
    {
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlAudio = FindObjectOfType<ControlAudio>();
    }    
    public void Calificar(bool respuesta )
    {
        seguirDisparando = false;
        if (respuesta)
            controlPuntaje.IncreaseScore();

        controlAudio.PlayAudio(respuesta ? 1 : 2);
        controlNavegacion.Forward(2);
    }

    public void modoRevision()=> seguirDisparando = false;

    public void ResetAll()
    {
        seguirDisparando = true;       
        
        switch (recurso)
        {
            case Recurso.globo:
                for (int x = 0; x < transform.childCount; x++)
                {
                    if (transform.GetChild(x).GetComponent<Animator>())
                    {
                        transform.GetChild(x).GetComponent<Animator>().SetBool("Expla", false);

                        transform.GetChild(x).GetComponent<Image>().enabled = true;
                        transform.GetChild(x).GetComponent<Image>().SetNativeSize();

                        transform.GetChild(x).transform.GetChild(0).GetComponent<Image>().sprite =
                            transform.GetChild(x).transform.GetChild(0).GetComponent<BehaviourSprite>()._default;
                    }
                    
                }
                break;

            case Recurso.Diana:
                for (int x = 0; x < transform.childCount; x++)
                {
                    if (transform.GetChild(x).GetComponent<M7A76Opciones>())
                        transform.GetChild(x).GetComponent<Image>().sprite = transform.GetChild(x).GetComponent<BehaviourSprite>()._default;     

                }
                break;

        }
    }
}
