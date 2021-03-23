using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class zoomImage : MonoBehaviour
{
    private Vector3 initialScale;

    public Slider _Slider;

    private float valor;

    private void Awake()
    {
        valor = _Slider.minValue;
        transform.localScale = new Vector3(_Slider.minValue,_Slider.minValue,_Slider.minValue);
    }

    public void zoom(float value)
    {
        valor = value;
    }

    private void Update()
    {
        transform.localScale = new Vector3(valor,valor,valor);
    }
}