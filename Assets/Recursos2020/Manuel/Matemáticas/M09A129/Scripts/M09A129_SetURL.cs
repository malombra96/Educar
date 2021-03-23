using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class M09A129_SetURL : MonoBehaviour
{
    public void OpenURL()
    {

        Application.ExternalEval("window.open('http://sie.educar.com.co/VisorGaleria/visor.php?archivo=OVAs/li_mat9_sistemas_de_ecuaciones_lineales_3x31.zip');");
    } 
}
