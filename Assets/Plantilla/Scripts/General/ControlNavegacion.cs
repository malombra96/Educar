using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlNavegacion : MonoBehaviour
{
    public enum Grade
    {
        Primary,
        Secundary
    }

    [Header("Seleccione el grado")] public Grade _grade;

    [Header ("Elementos de Navegacion")] public List<BehaviourLayout> _Layouts;
    
    /// <summary>
    /// Determina el layaout activo en escena
    /// </summary>
    /// <returns>indice del elemento</returns>
    public int LayoutActual()
    {
        for (int i = 0; i < _Layouts.Count; i++)
        {
            if (_Layouts[i].gameObject.activeSelf)
            {
                return i;
            }
        }

        return 99;
    }

    /// <summary>
    /// Devuelve  el gameobject del layout actual
    /// </summary>
    /// <returns></returns>
    public GameObject GetLayoutActual()
    {
        return LayoutActual() <= _Layouts.Count ? _Layouts[LayoutActual()].gameObject : null;
    }
    
    public void GoToLayout (int index) => StartCoroutine(GoTo(index));

    IEnumerator GoTo(int index)
    {
        yield return new WaitForSeconds(0.2f);
        
        foreach (var elemento in _Layouts) {
            elemento.gameObject.SetActive (false);
        }

        _Layouts[index].gameObject.SetActive (true);
    }

    public void GoToLayout (int index,float seconds) => StartCoroutine(delayGoTo(index,seconds));

    IEnumerator delayGoTo (int index,float seconds) 
    {
        yield return new WaitForSeconds (seconds);

        foreach (var elemento in _Layouts) 
            elemento.gameObject.SetActive (false);

        _Layouts[index].gameObject.SetActive (true);
    }

    

    /// <summary>
    /// Direcciona hacia la portada 
    /// </summary>
    /// <param name="type"></param>
    public void GoToInicialLayout() => GoToLayout(0);
    
    /// <summary>
    /// Direcciona hacia el primer Conozco 
    /// </summary>
    /// <param name="type"></param>
    public void GoToInicialConozco()
    {
        for (int i = 0; i < _Layouts.Count; i++)
        {
            if (_Layouts[i].GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Conozco)
            {
                GoToLayout(_Layouts.IndexOf(_Layouts[i]));
                break;
            }
                
        }
    }
    
    /// <summary>
    /// Direcciona hacia el primer Conozco 
    /// </summary>
    /// <param name="type"></param>
    public void GoToInicialAplico()
    {
        for (int i = 0; i < _Layouts.Count; i++)
        {
            if (_Layouts[i].GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Actividad)
            {
                GoToLayout(_Layouts.IndexOf(_Layouts[i]));
                break;
            }
                
        }
    }

    /// <summary>
    /// Direcciona hacia el primer Conozco 
    /// </summary>
    /// <param name="type"></param>
    public void GoToDesempeno()
    {
        for (int i = 0; i < _Layouts.Count; i++)
        {
            if (_Layouts[i].GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Desempeño)
            {
                GoToLayout(_Layouts.IndexOf(_Layouts[i]));
                break;
            }
                
        }
    }
    
    
    /// <summary>
    /// Activa el siguiente layout
    /// </summary>
    public void Forward () {
        StartCoroutine (delayForward ());
    }

    IEnumerator delayForward () {
        yield return new WaitForSeconds (0.2f);
        var siguienteElemento = LayoutActual () + 1;

        if (siguienteElemento > 0 && siguienteElemento < _Layouts.Count) {
            GoToLayout (siguienteElemento);
        } else {
            //_controlAudio.PlayAudio(2);
        }
    }

    /// <summary>
    /// Activa el siguiente layout
    /// </summary>
    public void Forward (float segundos) {
        StartCoroutine (_Forward (segundos));

    }

    /// <summary>
    /// Dirige al siguiente Layout sin  tiempo especifico
    /// </summary>
    private IEnumerator _Forward (float segundos) {

        var siguienteElemento = LayoutActual () + 1;

        if (siguienteElemento > 0 && siguienteElemento < _Layouts.Count) {
            yield return new WaitForSeconds (segundos);
            GoToLayout (siguienteElemento);
        } else {
            //_controlAudio.PlayAudio(2);
        }
    }

    /// <summary>
    /// Hace una espera de n segundos
    /// </summary>
    /// <param name="seconds">segundos a a esperar</param>
    /// <returns></returns>
    private IEnumerator CustomDelay (float seconds) {
        yield return new WaitForSeconds (seconds);
    }

    /// <summary>
    /// Activa el anterior layout
    /// </summary>
    public void Backward () {
        StartCoroutine (delayBackward ());
    }

    IEnumerator delayBackward () {
        yield return new WaitForSeconds (0.2f);
        var anteriorElemento = LayoutActual () - 1;

        if (anteriorElemento >= 0 && anteriorElemento < _Layouts.Count) {
            GoToLayout (anteriorElemento);
        } else {
            //_controlAudio.PlayAudio(2);
        }
    }

    public int? GetLayout (GameObject elem) {
        for (int i = 0; i < _Layouts.Count; i++) {
            if (_Layouts[i].name == elem.name) {
                return i;
            }
        }

        return null;
    }

    public void reiniciarEscena (int index) {
        SceneManager.LoadScene (index);

    }

    public void reiniciarEscena (string value) {
        StartCoroutine (DelayLoadScene (value));

    }

    IEnumerator DelayLoadScene (string value) {
        int index = int.Parse (value.Split ('_') [0]);
        int delay = int.Parse (value.Split ('_') [1]);

        yield return new WaitForSeconds (delay);
        SceneManager.LoadScene (index);
    }

    public void StopCoroutines () {
        StopAllCoroutines ();
    }
}
