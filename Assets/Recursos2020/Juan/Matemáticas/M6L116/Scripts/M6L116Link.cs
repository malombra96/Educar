using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L116Link : MonoBehaviour
{
       
    public void darLink()
    {
        //Application.OpenURL(link);
        Application.ExternalEval("window.open('https://sie.educar.com.co/cas/login?service=http%3A%2F%2Fsie.educar.com.co%2FVisorGaleria%2Fvisor.php%3Farchivo%3DOVAs%2Fli_mat6_operaciones_entre_conjuntos.zip');");
    }
}
