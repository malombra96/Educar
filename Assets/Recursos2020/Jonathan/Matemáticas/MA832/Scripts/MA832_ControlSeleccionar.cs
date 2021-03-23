using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA832_ControlSeleccionar : MonoBehaviour
{
    [Header("Puntos del Marcador :")] public List<GameObject> points;

    [Header("Controles : ")] 
    public ControlNavegacion controlNavegacion;
    public ControlPuntaje controlPuntaje;
    
    [Header("Aciertos :")] public int hits;
    
    [Header("Aciertos :")] public int mistakes;

    private GameObject navbar;
    void Start()
    {
        hits = 0;
        mistakes = -1;
        foreach (var point in points)
            point.SetActive(false);
        
        navbar = GameObject.Find("NavBar");
        
    }

    public void Marker()
    {
        if (mistakes == 3)
            StartCoroutine(Tiempo());

        for (int i = 0; i < points.Count; i++)
        {
            if (mistakes >= i && mistakes>-1)
            {
                points[i].SetActive(true);
            }
        }
            
    }
    
    public void Check()
    {
        if (hits == 4)
            StartCoroutine(Tiempo());
        
        controlPuntaje.IncreaseScore();
    }

    IEnumerator Tiempo()
    {
        yield return  new WaitForSeconds(2f);
        navbar.SetActive(true);
        controlNavegacion.GoToLayout(7);
    }
}
