using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A256_Conozco : MonoBehaviour
{
    public Button boton;
    public ControlNavegacion ControlNavegacion;
    public int layout;

    public void Abrir() {
        StartCoroutine(x());
    }

    IEnumerator x()
    {
        yield return new WaitForSeconds(1f);
        ControlNavegacion.GoToLayout(layout);
    }
}
