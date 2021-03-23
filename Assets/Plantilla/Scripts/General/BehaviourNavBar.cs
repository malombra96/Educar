using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourNavBar : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    
    
    public enum ResourceType
    {
        onlyConozco,
        onlyAplico,
        Default
    }

    [HideInInspector] public ResourceType _type;

    [Header("Botones NavBar")] public List<GameObject> _navBar;
    [Header("Zoom Elements")] 
    public Toggle _zoomT;
    public Slider _slider;

    [Header("Temporales")]
    GameObject x;

    private void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        GetSizeTypeLayout();
    }

    void GetSizeTypeLayout()
    {
        bool x = false;
        bool y = false;
        
        foreach (var layout in _controlNavegacion._Layouts)
        {
            switch (layout._Layout)
            {
                case BehaviourLayout.TipoLayout.Conozco: x=true; break;
                case BehaviourLayout.TipoLayout.Actividad: y=true; break;
                
            }
        }

        if(x && !y)
        {
            print("Solo conozco");
            _type = ResourceType.onlyConozco;
        }
        else if(!x && y)
        {
            print("Solo aplico");
            _type = ResourceType.onlyAplico;
        }
        else
        {
            print("Conozco&Aplico");
            _type = ResourceType.Default;
        }


        ConfigureNavBar();
    }

    void ConfigureNavBar()
    {
        switch (_type)
        {
            case ResourceType.onlyConozco:

                _navBar[1].gameObject.SetActive(false);
                _navBar.Remove(_navBar[1]);


                break;

            case ResourceType.onlyAplico:

                _navBar[0].gameObject.SetActive(false);
                _navBar.Remove(_navBar[0]);


                break;

            case ResourceType.Default:


                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        x = _controlNavegacion.GetLayoutActual(); // Get active element

        if (_type == ResourceType.onlyConozco || _type == ResourceType.onlyAplico)
        {
            if (x != null)
            {
                if (x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Conozco ||
                    x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Actividad)
                {
                    SelectionState(_navBar[0]);
                    SetInteraction(true);
                }
                else if(x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Desempeño)
                {
                    SelectionState(_navBar[1]);
                    SetInteraction(_navBar[1],false);
                }
                    
                    
            }
        }
        else
        {
            if (x != null)
            {
                if (x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Conozco)
                {
                    SelectionState(_navBar[0]);
                    SetInteraction(true);
                }
                else if (x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Actividad)
                {
                    SelectionState(_navBar[1]);
                    SetInteraction(true);
                }
                else if(x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Desempeño)
                {
                    SelectionState(_navBar[2]);
                    SetInteraction(_navBar[2],false);
                }    

            }

        }

    }

    #region SpriteState

    void DefaultState()
    {
        foreach (var element in _navBar)
            element.GetComponent<Image>().sprite = element.GetComponent<BehaviourSprite>()._default;
    }
    
    void SelectionState(GameObject x)
    {
        foreach (var element in _navBar)
        {
            if(element == x)
                element.GetComponent<Image>().sprite = element.GetComponent<BehaviourSprite>()._selection;
            else
                element.GetComponent<Image>().sprite = element.GetComponent<BehaviourSprite>()._default;
                
        }    
    }

    #endregion
    
    #region Methods Element [Enabled,Disabled,Interaction]
        
    /// <summary>
    /// Habilita todos los elementos del NavBvar
    /// </summary>
    public void Habilitar() 
    {
        foreach (var elem in _navBar) 
        {
            elem.SetActive(true);
        }
        
    }
    
    /// <summary>
    /// Inhabilita [false] o Habilita [true] los todos los botones del NavBar menos el recibido
    /// </summary>
    /// <param GameObject="obj">Elemento que cambia su estado</param>
    public void Restringir(GameObject obj,bool state) 
    {
        foreach (var elem in _navBar) 
        {
            if (elem == obj)
            {
                elem.SetActive(state);
            }
            else
            {
                elem.SetActive(!state);
            }
        }
    }
    
    /// <summary>
    /// Deshabilita todos los botones del NavBar
    /// </summary>
    public void DesHabilitar()
    {
        foreach (var elem in _navBar)
        {
            elem.SetActive(false);
        }
    }

    public void DeshabilitarWithZoom()
    {
        foreach (var elem in _navBar)
            elem.SetActive(false);
    }
    
    /// <summary>
    /// Controla los elementos UI del zoom
    /// </summary>
    /// <param name="state"></param>
    public void SetElementZoom(bool state)
    {
        _zoomT.gameObject.SetActive(state);
        _slider.transform.parent.gameObject.SetActive(state);
    }

    public void SetInteraction(GameObject obj,bool state)
    {
        foreach (var elem in _navBar) 
        {
            if (elem == obj)
                elem.GetComponent<Button>().interactable = state;
            else
                elem.GetComponent<Button>().interactable = !state;
        }
    }

    public void SetInteraction(bool state)
    {
        foreach (var elem in _navBar) 
            elem.GetComponent<Button>().interactable = state;
    }
    
    #endregion

    #region GoToLayout
    
    /// <summary>
    /// Metodo para direccionar al layout 0
    /// </summary>
    
    public void Home()
    {
        _controlNavegacion.GoToLayout(0);
    }

    public void Cerrar()
    {
        _controlNavegacion.reiniciarEscena(0_1);
    }

    #endregion
}
