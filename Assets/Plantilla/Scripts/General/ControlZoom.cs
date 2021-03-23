using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlZoom : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;

    /*[HideInInspector]*/ public GameObject actual;

    [SerializeField] private float maxZoom = 3f;
    
    [Header("UI-Elements")]

    public GameObject elementoInactivo;
    
    public Toggle _toggleZoom;

    public Slider _Slider;

    float deltaMagnitudeDiff,distancia;

    bool estado = false ;
    
    Vector3 initialScale;
    
    float valor;

    private void Awake()
    {
        valor = _Slider.minValue;
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    // Start is called before the first frame update
    void Start()
    {
        elementoInactivo.SetActive(false);
        GetComponent<ScrollRect>().enabled = true;
        
        _toggleZoom.onValueChanged.AddListener(delegate { SliderShow(); });
        _Slider.onValueChanged.AddListener(zoomLayout);

    }

    void LateUpdate() => actual = _controlNavegacion.GetLayoutActual(); 


    void SliderShow()
    {
        GetLayoutActive();
        
        if (_toggleZoom.isOn)
        {
            _toggleZoom.transform.GetChild(0).GetComponent<Image>().sprite = _toggleZoom.GetComponent<BehaviourSprite>()._selection;
            _Slider.transform.parent.GetComponent<Animator>().SetInteger("state",1);
        }
        else
        {
            _toggleZoom.transform.GetChild(0).GetComponent<Image>().sprite = _toggleZoom.GetComponent<BehaviourSprite>()._default;
            _Slider.transform.parent.GetComponent<Animator>().SetInteger("state",2);
            StartCoroutine(defaultAnimation());
            
        }
    }

    IEnumerator defaultAnimation()
    {
        yield return new WaitForSeconds(1.05f);
        _Slider.transform.parent.GetComponent<Animator>().SetInteger("state",0);
    }

    void GetLayoutActive()
    {
        if (_toggleZoom.isOn)
        {
            elementoInactivo.SetActive(true);
            GetComponent<ScrollRect>().content = actual.GetComponent<RectTransform>();
            GetComponent<ScrollRect>().enabled = _toggleZoom.isOn;    
        }
        else
        {
            DefaultZoom();
            
        }
    }

    void DefaultZoom()
    {
        _Slider.value = _Slider.minValue;
        GetComponent<ScrollRect>().content.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    void zoomLayout(float value)
    {
        actual.GetComponent<RectTransform>().localScale = new Vector3(value, value, 1); // Before z=0; 8 Junio by Juan
        elementoInactivo.SetActive(value != _Slider.minValue);
            
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(initialScale, desiredScale);
        desiredScale = Vector3.Min(initialScale * maxZoom, desiredScale);
        return desiredScale;
    }

}
