using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class BehaviourLeccionInteractiva : MonoBehaviour
{
    [HideInInspector]public BehaviourNavBarLeccion _navBarLeccion;

    void Start()
    {
        _navBarLeccion = GetComponent<BehaviourNavBarLeccion>();
        ConfigureNavBar();
    }

    public void ConfigureNavBar()
    {
        switch (_navBarLeccion._type)
        {
            case BehaviourNavBarLeccion.ResourceType.onlyConozco:

                for (int i = 0; i < _navBarLeccion._navBar.Count; i++)
                {   
                    _navBarLeccion._navBar[i].gameObject.SetActive(i < _navBarLeccion._learnSize);
                    _navBarLeccion._buttonsCover[i].gameObject.SetActive(i < _navBarLeccion._learnSize);
                    
                }

                for (int i = 0; i < _navBarLeccion._textDesempeno.Count; i++)
                    _navBarLeccion._textDesempeno[i].gameObject.SetActive(i < _navBarLeccion._learnSize);

                _navBarLeccion._scoreText.gameObject.SetActive(false);

                break;

            case BehaviourNavBarLeccion.ResourceType.onlyAplico:
            
                for (int i = 0; i < _navBarLeccion._navBar.Count; i++)
                {
                    _navBarLeccion._navBar[i].gameObject.SetActive(i < 1);
                    _navBarLeccion._buttonsCover[i].gameObject.SetActive(i < 1);
                }

                for (int i = 0; i < _navBarLeccion._textDesempeno.Count; i++)
                    _navBarLeccion._textDesempeno[i].gameObject.SetActive(false);

                _navBarLeccion._scoreText.gameObject.SetActive(true);
                    

                break;

            case BehaviourNavBarLeccion.ResourceType.Default:

                for (int i = 0; i < _navBarLeccion._navBar.Count; i++)
                {
                    _navBarLeccion._navBar[i].gameObject.SetActive(i < _navBarLeccion._learnSize+1); // Se suma por que tiene aplico ademas de los conozco
                    _navBarLeccion._buttonsCover[i].gameObject.SetActive(i < _navBarLeccion._learnSize+1);
                }

                for (int i = 0; i < _navBarLeccion._textDesempeno.Count; i++)
                    _navBarLeccion._textDesempeno[i].gameObject.SetActive(i < _navBarLeccion._learnSize);

                _navBarLeccion._scoreText.gameObject.SetActive(true);
                    

                break;

        }
    }
    
    
}
