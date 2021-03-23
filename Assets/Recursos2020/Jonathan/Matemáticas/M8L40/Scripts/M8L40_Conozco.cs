using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8L40_Conozco : MonoBehaviour
{
    [Header("el mismo")] public GameObject bloqueo;
    [HideInInspector] public ControlNavegacion navegacion;
    [HideInInspector] public GameObject navbar;

    //[Header("Botón de actividad en curso")] public GameObject btn_actividad;
     public enum tipo_nivel
    {
        niveluno,
        niveldos,
        niveltres
    }

     public tipo_nivel _nivel;
    
    public int layoutActivo; // Guarda el layout en el que va el usuario 
     [HideInInspector] public bool nivel1 = true;
     [HideInInspector] public bool nivel2 = true;
     [HideInInspector] public bool nivel3 = true;
    private Button arrow_next;
    private Button arrow_back;
   /* [HideInInspector] */public List<GameObject> botones1;
   /* [HideInInspector] */public List<GameObject> botones2;
   /* [HideInInspector] */public List<GameObject> botones3;
    private void Awake()
    {
        navegacion = FindObjectOfType<ControlNavegacion>();
        navbar = GameObject.Find("NavBar Leccion");
        arrow_back = bloqueo.transform.GetChild(1).GetComponent<Button>();
        arrow_next = bloqueo.transform.GetChild(0).GetComponent<Button>();
        arrow_next.onClick.AddListener(Arrows_bloqueo_next);
        arrow_back.onClick.AddListener(Arrows_bloqueo_back);

        for (int i = 4; i <= 8; i ++)
            botones1.Add(navegacion._Layouts[i].transform.GetChild(5).gameObject);

        for (int i = 8; i <= 13; i ++)
            botones2.Add(navegacion._Layouts[i].transform.GetChild(5).gameObject);

        for (int i = 14; i <= 18; i++)
            botones3.Add(navegacion._Layouts[i].transform.GetChild(5).gameObject);

    }

    private void Start()
    {
        //layoutActivo = navegacion.LayoutActual();
        //if (layoutActivo >= 4 && layoutActivo <= 8)
        //    _nivel = tipo_nivel.niveluno;

        //if (layoutActivo >= 9 && layoutActivo <= 13)
        //    _nivel = tipo_nivel.niveldos;

        //if (layoutActivo >= 14 && layoutActivo < 18)
        //    _nivel = tipo_nivel.niveltres;
    }

    public void Help(int layout)
    {
        layoutActivo = layout;
        //ultimoLayout = ultimo;

        if (layout >= 4 && layout <= 8)
            _nivel = tipo_nivel.niveluno;

        if (layout >= 9 && layout <= 13)
            _nivel = tipo_nivel.niveldos;

        if (layout >= 14 && layout < 18)
            _nivel = tipo_nivel.niveltres;


        switch (_nivel)
        {
            case tipo_nivel.niveluno:
                if (nivel1)
                {
                    navbar.SetActive(true);
                    bloqueo.SetActive(true);
                    nivel1 = false;
                    navegacion.GoToLayout(1);
                }
                else
                {
                    navbar.SetActive(false);
                }
                break;
            case tipo_nivel.niveldos:
                if (nivel2)
                {
                    navbar.SetActive(true);
                    bloqueo.SetActive(true);
                    nivel2 = false;
                    navegacion.GoToLayout(1);
                }
                else
                {
                    navbar.SetActive(false);
                }
                break;
            case tipo_nivel.niveltres:
                if (nivel3)
                {
                    navbar.SetActive(true);
                    bloqueo.SetActive(true);
                    nivel3 = false;
                    navegacion.GoToLayout(1);
                }
                else
                {
                    navbar.SetActive(false);
                }
                break;
        }
        
    }
    
    public void Reanudar()
    {
        switch (_nivel)
        {
            case tipo_nivel.niveluno:
                for (int i = 0; i < botones1.Count; i ++)
                {
                    botones1[i].GetComponent<Button>().interactable = false;
                    botones1[i].GetComponent<M8L40_Help>().enabled = false;
                }
                break;
            case tipo_nivel.niveldos:
                for (int i = 0; i < botones2.Count; i++ )
                {
                    botones2[i].GetComponent<Button>().interactable = false;
                    botones2[i].GetComponent<M8L40_Help>().enabled = false;
                }
                break;
            case tipo_nivel.niveltres:
                for (int i = 0; i < botones3.Count; i++ )
                {
                    botones3[i].GetComponent<Button>().interactable = false;
                    botones3[i].GetComponent<M8L40_Help>().enabled = false;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        navegacion.GoToLayout(layoutActivo); // regresa al usuario a donde estaba 
        bloqueo.SetActive(false);
        navbar.SetActive(false);
    }

    public void Arrows_bloqueo_next()
    {
        navegacion.GoToLayout(2);
    }
    
    public void Arrows_bloqueo_back()
    {
        navegacion.GoToLayout(1);
    }
}
