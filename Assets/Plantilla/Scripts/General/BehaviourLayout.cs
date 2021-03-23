using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BehaviourLayout : MonoBehaviour
{
    [Header("Arratre aqui el objeto Bar Navegacion")] 
    public BehaviourNavBar _BehaviourNavBar; 
    public BehaviourNavBarLeccion _BehaviourNavBarLeccion; 
    
    #region "Tipo de escena"
    public enum TipoLayout 
    {
        None,
        Portada,
        Conozco,
        Actividad,
        Informacion,
        PopUp,
        Video,
        Desempeño
    }
    
    [Header("Seleccion que tipo de layout es:")] public TipoLayout _Layout;

    #endregion
    
    [Header("Audio Option")]
    public bool audio = false; // El layout tiene audio?

    [Header("Estado de revision")] public bool _isEvaluated;
    
    // Start is called before the first frame update
    void Start()
    {
        //_isEvaluated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_BehaviourNavBar && _BehaviourNavBarLeccion == null)
            SetupNavBarActivity();
        else if(_BehaviourNavBar == null && _BehaviourNavBarLeccion)
            SetupNavBarLesson();


        
        #region OpcionesAudio

        if (gameObject.GetComponent<AudioSource>() == null && audio)
        {
            gameObject.AddComponent<AudioSource>();
            gameObject.GetComponent<AudioSource>().playOnAwake = true;
            

        }
        else if (gameObject.GetComponent<AudioSource>() != null && !audio)
        {
            DestroyImmediate(gameObject.GetComponent<AudioSource>());
        }

        #endregion
    }

    void SetupNavBarLesson()
    {
        switch (_Layout)
        {
            case TipoLayout.None:
                _BehaviourNavBarLeccion.SetActiveElements(false);
                _BehaviourNavBarLeccion.SetElementZoom(false);
                _BehaviourNavBarLeccion.SetHome(false);
                _BehaviourNavBarLeccion.SetPerformance(false);
                break;
            case TipoLayout.Portada:
                _BehaviourNavBarLeccion.SetActiveElements(false);
                _BehaviourNavBarLeccion.SetElementZoom(true);
                _BehaviourNavBarLeccion.SetHome(false);
                _BehaviourNavBarLeccion.SetPerformance(false);
                
                break;
            case TipoLayout.Conozco:
                _BehaviourNavBarLeccion.SetActiveElements(true);
                _BehaviourNavBarLeccion.SetElementZoom(true);
                _BehaviourNavBarLeccion.SetHome(true);
                _BehaviourNavBarLeccion.SetPerformance(true);

                
                _BehaviourNavBarLeccion.SetInteraction(true);
                _BehaviourNavBarLeccion.SetInteractionHome(true);
                _BehaviourNavBarLeccion.SetInteractionPerformance(true);
                break;
            case TipoLayout.Actividad:
                _BehaviourNavBarLeccion.SetActiveElements(true);
                _BehaviourNavBarLeccion.SetElementZoom(true);
                _BehaviourNavBarLeccion.SetHome(true);
                _BehaviourNavBarLeccion.SetPerformance(true);

                _BehaviourNavBarLeccion.SetInteraction(true);
                _BehaviourNavBarLeccion.SetInteractionHome(true);
                _BehaviourNavBarLeccion.SetInteractionPerformance(true);

                break;
            case TipoLayout.Informacion:
                break;
            case TipoLayout.PopUp:
                _BehaviourNavBarLeccion.SetActiveElements(false);
                _BehaviourNavBarLeccion.SetElementZoom(true);
                _BehaviourNavBarLeccion.SetHome(false);
                _BehaviourNavBarLeccion.SetPerformance(false);
                break;
            case TipoLayout.Video:
                _BehaviourNavBarLeccion.SetActiveElements(false);
                _BehaviourNavBarLeccion.SetElementZoom(false);
                _BehaviourNavBarLeccion.SetHome(false);
                _BehaviourNavBarLeccion.SetPerformance(false);
                break;
            case TipoLayout.Desempeño:
                _BehaviourNavBarLeccion.SetActiveElements(true);
                _BehaviourNavBarLeccion.SetElementZoom(false);
                _BehaviourNavBarLeccion.SetHome(true);
                _BehaviourNavBarLeccion.SetPerformance(true);
                
                _BehaviourNavBarLeccion.SetInteraction(false);
                _BehaviourNavBarLeccion.SetInteractionHome(false);
                _BehaviourNavBarLeccion.SetInteractionPerformance(true);
                break;
            
        }
    }

    void SetupNavBarActivity()
    {
        switch (_Layout)
        {
            case TipoLayout.None:
                _BehaviourNavBar.DesHabilitar();
                _BehaviourNavBar.SetElementZoom(false);
                break;
            case TipoLayout.Portada:
                _BehaviourNavBar.DesHabilitar();
                _BehaviourNavBar.SetElementZoom(true);
                break;
            case TipoLayout.Conozco:
                _BehaviourNavBar.Habilitar();
                _BehaviourNavBar.SetElementZoom(true);
                //_BehaviourNavBar.Intertactuable(true);
                break;
            case TipoLayout.Actividad:
                _BehaviourNavBar.Habilitar();
                _BehaviourNavBar.SetElementZoom(true);
                //_BehaviourNavBar.Intertactuable(true);
                break;
            case TipoLayout.Informacion:
                break;
            case TipoLayout.PopUp:
                _BehaviourNavBar.DesHabilitar();
                _BehaviourNavBar.SetElementZoom(true);
                break;
            case TipoLayout.Video:
                _BehaviourNavBar.DesHabilitar();
                _BehaviourNavBar.SetElementZoom(false);
                break;
            case TipoLayout.Desempeño:
                _BehaviourNavBar.DesHabilitar();
                //_BehaviourNavBar.Restringir(_BehaviourNavBar._navBar[_BehaviourNavBar._navBar.Count-1],true);
                _BehaviourNavBar.SetElementZoom(false);
                break;
            
        }
    }
}
