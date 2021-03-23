using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BehaviourNavBarLeccion : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    ControlAudio _controlAudio;
    public enum ResourceType
    {
        onlyConozco,
        onlyAplico,
        Default
    }

    [Header("Seleccione el tipo de recurso :")] public ResourceType _type;
    
    [Header("NavBar Buttons")] [Tooltip("Arrastre solo los numeros")] public List<GameObject> _navBar;
    [Header("Buttons Cover")] [Tooltip("Botones de la portada")] public List<Button> _buttonsCover;
    
    [Header("Textos Desempeño")] [Tooltip("Arrastre solo los correspondientes a conozco")] public List<Text> _textDesempeno;
    [Header("Text Score ")]  public Text _scoreText;

    [Header("Functions Buttons")] 
    public Button _home;
    public Button _performance;

    [Header("Without Desempeño")] public bool _withoutPerformance;

    [Header("Zoom Elements")]
    public Toggle _zoomT;
    public Slider _slider;

    [Header("Size Learn")] 
    [Tooltip("Ingrese la cantidad de conozco de la lección")] 
    [Range(0,6)] public int _learnSize;

    BehaviourLayout current;


    private void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        SetActionButton();
    }

    void SetActionButton()
    {
        _navBar[_learnSize].GetComponent<Button>().onClick.AddListener(_controlNavegacion.GoToInicialAplico);
        _home.GetComponent<Button>().onClick.AddListener(_controlNavegacion.GoToInicialLayout);
        _performance.GetComponent<Button>().onClick.AddListener(_controlNavegacion.GoToDesempeno);

        for (int i = 0; i < _learnSize; i++)
        {
            _navBar[i].GetComponent<Button>().onClick.AddListener(ActionGoToLayout);
            _buttonsCover[i].onClick.AddListener(ActionGoToLayout);
        }

        _buttonsCover[_learnSize].onClick.AddListener(_controlNavegacion.GoToInicialAplico); // GotoInicialAplico

        _performance.gameObject.SetActive(!_withoutPerformance);

    }

    void ActionGoToLayout()
    {
        int n;

        if(_navBar.Contains(EventSystem.current.currentSelectedGameObject))
            n = _navBar.IndexOf(EventSystem.current.currentSelectedGameObject);
        else
            n = _buttonsCover.IndexOf(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());

        
        _controlAudio.PlayAudio(0);
        _controlNavegacion.GoToLayout(n+1);
    }

    void Update()
    {
        SetLayoutActive();
    }
   

    /// Relaciona el layout activo (_type) con el boton del navBar
    void SetLayoutActive()
    {
        if(_controlNavegacion.GetLayoutActual())
            current = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (current != null)
        {
            int index = _controlNavegacion._Layouts.IndexOf(current);

            switch (current._Layout)
            {
                case BehaviourLayout.TipoLayout.Conozco:

                    for (int i = 0; i < _navBar.Count; i++)
                    {
                        _navBar[i].GetComponent<Image>().sprite = i == (index - 1) ?
                            _navBar[i].GetComponent<BehaviourSprite>()._selection :
                            _navBar[i].GetComponent<BehaviourSprite>()._default;
                    }

                    break;

                case BehaviourLayout.TipoLayout.Actividad:

                    for (int i = 0; i < _navBar.Count; i++)
                    {
                        _navBar[i].GetComponent<Image>().sprite = (i >= _learnSize)? 
                            _navBar[i].GetComponent<BehaviourSprite>()._selection:
                            _navBar[i].GetComponent<BehaviourSprite>()._default;     
                    }

                    break; 
            }
            

        }
        else
        {
            for (int i = 0; i < _navBar.Count; i++)
                _navBar[i].GetComponent<Image>().sprite = _navBar[i].GetComponent<BehaviourSprite>()._default;
        }

    }

    #region Methods Element [Enabled,Disabled,Interaction]
        
    /// <summary>
    /// Habilita todos los elementos del NavBvar
    /// </summary>
    public void SetActiveElements(bool state) 
    {
        if (state)
            GetComponent<BehaviourLeccionInteractiva>().ConfigureNavBar();
        else
        {
            foreach (var elem in _navBar)
                elem.SetActive(state);
        }
        
    }

    public void SetHome(bool state)
    {
        _home.gameObject.SetActive(state);
    }

    public void SetPerformance(bool state)
    {
        if(!_withoutPerformance)
            _performance.gameObject.SetActive(state);
    }

    public void SetInteractionHome(bool state)
    {
        _home.GetComponent<Button>().interactable = state;
    }

    public void SetInteractionPerformance(bool state)
    {
        if(!_withoutPerformance)
            _performance.GetComponent<Button>().interactable = state;
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
