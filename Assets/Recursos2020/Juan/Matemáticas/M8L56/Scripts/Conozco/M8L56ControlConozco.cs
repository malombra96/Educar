using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L56ControlConozco : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    public TipoDeAplico tipoDeAplico;
    public GameObject datos;
    public GameObject teoria;
    public GameObject _lightBox;
    public GameObject mano;
    public GameObject botones;
    int i = 0;
    float media;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();

        for (int x = 0; x < botones.transform.childCount; x++)
        {
            botones.transform.GetChild(x).GetComponent<Button>().onClick.AddListener(Operacion);
            botones.transform.GetChild(x).gameObject.SetActive(false);
            teoria.transform.GetChild(x).gameObject.SetActive(false);
        }       
        mano.SetActive(false);

        InvokeRepeating("ActivadorBoton", 0.1f, 0.1f);
    }

    public enum TipoDeAplico
    {
        DesviacionMedia,
        Varianza
    }
    
    public void ActivadorBoton()
    {
        int activo = 0;
        for (int x = 0; x < datos.transform.childCount; x++)
            if (datos.transform.GetChild(x).GetComponent<M8L56Input>()._isRight)
                activo++;

        if(activo == datos.transform.childCount)
        {            
            botones.transform.GetChild(0).gameObject.SetActive(true);
            if(botones.transform.GetChild(0).GetComponent<Button>().interactable)
                mano.SetActive(true);
        }
        else
        {            
            botones.transform.GetChild(0).gameObject.SetActive(false);
            mano.SetActive(false);
        }
    }
    
    void Operacion()
    {
        controlAudio.PlayAudio(0);
        teoria.transform.GetChild(i).gameObject.SetActive(true);
        switch (i)
        {
            case 0:
                
                botones.transform.GetChild(i).GetComponent<Button>().interactable = false;
                mano.SetActive(false);

                for (int x = 0; x < datos.transform.childCount; x++)
                { 
                    var text = teoria.transform.GetChild(0).transform.GetChild(x).GetComponent<Text>();
                    text.text = datos.transform.GetChild(x).transform.GetChild(1).GetComponent<Text>().text;
                    media += Convert.ToSingle(text.text);
                }
                teoria.transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = Convert.ToString(media);
                media /= datos.transform.childCount;
                teoria.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text = Convert.ToString(media);
                botones.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case 1:
                botones.transform.GetChild(i).GetComponent<Button>().interactable = false;
                for (int x = 0; x < teoria.transform.GetChild(i).transform.GetChild(0).transform.childCount; x++)
                {
                    var text = teoria.transform.GetChild(i).transform.GetChild(0).transform.GetChild(x).GetComponent<Text>();
                    text.text = datos.transform.GetChild(x).transform.GetChild(1).GetComponent<Text>().text;                    
                }
                d = 0;
                botones.transform.GetChild(2).gameObject.SetActive(false);
                StartCoroutine(diferencia());                
                break;

            case 2:
                float respuesta = 0;
                float tempo = 0;
                botones.transform.GetChild(i).GetComponent<Button>().interactable = false;
                for (int x = 0; x < 5; x++) 
                {                    
                    var text = teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(x).GetComponent<Text>().text;
                    
                    if (tipoDeAplico == TipoDeAplico.Varianza)
                        respuesta = Convert.ToSingle(text) * Convert.ToSingle(text);
                    else
                        respuesta = Convert.ToSingle(text);
                    
                    teoria.transform.GetChild(i).GetChild(x).GetComponent<Text>().text = Convert.ToString(Math.Round(respuesta,1));

                   tempo += respuesta;
                }
                teoria.transform.GetChild(i).transform.GetChild(5).GetComponent<Text>().text = Convert.ToString(Math.Round(tempo,1));
                tempo /= datos.transform.childCount;
                teoria.transform.GetChild(i).transform.GetChild(6).GetComponent<Text>().text = Convert.ToString(Math.Round(tempo,1));
                break;
        }        
        i++;       
    }
    int d;
    IEnumerator diferencia()
    {
        yield return new WaitForSeconds(1f);
        teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(d).gameObject.SetActive(true);
        teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(d).GetComponent<Text>().text = datos.transform.GetChild(d).transform.GetChild(1).GetComponent<Text>().text;

        float temp = Convert.ToSingle(teoria.transform.GetChild(1).transform.GetChild(0).transform.GetChild(d).GetComponent<Text>().text);
        float resta = temp - media;
        if (resta < 0)
            resta *= -1;        

        teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(d).GetComponent<Text>().text = Convert.ToString( Math.Round(Convert.ToDouble(resta),1));        
        teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(d).
            transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = temp + " - " + media;
        teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(d).transform.GetChild(0).gameObject.SetActive(true);

        if (tipoDeAplico == TipoDeAplico.Varianza)
        {
            resta *= resta;
            teoria.transform.GetChild(1).transform.GetChild(2).transform.GetChild(d).GetComponent<Text>().text = Convert.ToString(Math.Round(Convert.ToDouble(resta), 1));
            teoria.transform.GetChild(1).transform.GetChild(2).transform.GetChild(d).gameObject.SetActive(true);
        }

        if (d>0)
            teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(d-1).transform.GetChild(0).gameObject.SetActive(false);

        d++;

        if (d < 5)
            StartCoroutine(diferencia());
        else
            botones.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void resetAll()
    {
        i = 0;
        for (int x = 0; x < botones.transform.childCount; x++)
        {
            //botones.transform.GetChild(x).GetComponent<Button>().onClick.AddListener(Operacion);
            botones.transform.GetChild(x).GetComponent<Button>().interactable = true;
            botones.transform.GetChild(x).gameObject.SetActive(false);
            teoria.transform.GetChild(x).gameObject.SetActive(false);
        }
        for (int x = 0; x < datos.transform.childCount; x++)
        {
            datos.transform.GetChild(x).GetComponent<InputField>().text = "";
            teoria.transform.GetChild(1).transform.GetChild(1).transform.GetChild(x).gameObject.SetActive(false);
            datos.transform.GetChild(x).GetComponent<M8L56Input>()._isRight = false;
        }
        mano.SetActive(false);
    }
}
