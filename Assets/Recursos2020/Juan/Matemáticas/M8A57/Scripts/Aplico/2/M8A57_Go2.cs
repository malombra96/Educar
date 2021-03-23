using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A57_Go2 : MonoBehaviour
{
    [Header("Control navegación : ")] public ControlNavegacion ControlNavegacion;
    [Header("numero del layout : ")] public int ir;
    
    public bool preguntaContestada;

    public IEnumerator tiempo(float i)
    {
        yield return new WaitForSeconds(i);
        ControlNavegacion.GoToLayout(ir);
    }
}
