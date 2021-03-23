using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A259_interaccion : MonoBehaviour
{
    [Tooltip("False = Objeto Interactuado - True = No ha sido interactuado")]
    [Header("Estado de interacción")] public bool _state;
    [Header("Control de Interaccion")] public L5A259_conozco _controlInteractividad;

    private Component[] _component;

    void Start()
    {
        _controlInteractividad._objects.Add(this);

        _component = GetComponents(typeof(Component));

        foreach (var componente in _component)
        {
            if (componente.GetType() == typeof(Button))
                GetComponent<Button>().onClick.AddListener(delegate { _controlInteractividad.SetInteraction(_state, gameObject); });
            else if (componente.GetType() == typeof(ScrollRect))
                GetComponent<ScrollRect>().onValueChanged.AddListener(delegate { _controlInteractividad.SetInteraction(_state, gameObject); });
            else if (componente.GetType() == typeof(Slider))
                GetComponent<Slider>().onValueChanged.AddListener(delegate { _controlInteractividad.SetInteraction(_state, gameObject); });
            else if (componente.GetType() == typeof(Toggle))
                GetComponent<Toggle>().onValueChanged.AddListener(delegate { _controlInteractividad.SetInteraction(_state, gameObject); });
        }
    }
}
