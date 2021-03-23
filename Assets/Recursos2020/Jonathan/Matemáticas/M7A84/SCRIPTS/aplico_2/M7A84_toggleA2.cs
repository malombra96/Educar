using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class M7A84_toggleA2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imagenCoordenada;
    public bool isRight,op,puntoInicio;
    Toggle toggle;
    M7A84_manager _manager;
    ControlAudio controlAudio;
    [HideInInspector] [Header("Coordenada")] public Vector3 _point;
    public GameObject puntoA,puntoB;
    public List<GameObject> puntosRights;
    public int x, y;
    
    private void Awake()
    {
        
        _manager = FindObjectOfType<M7A84_manager>();
        controlAudio = FindObjectOfType<ControlAudio>();
        toggle = gameObject.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { ChangeSprite(); });
        _manager.toggles.Add(toggle);
        _point = GetComponent<RectTransform>().position;
        _point.z -= .1f;
    }
    void Start()
    {
        toggle.interactable = false;
    }
    private void Update()
    {
        if (puntoA != null && !puntoInicio && puntoB != null)
        {
            toggle.interactable = false;
        }
        if (puntoA != null && puntoB != null)
        {
            toggle.interactable = false;
        }
    }

    public void ChangeSprite() {
        
        if (!op)
        {
            
            if (toggle.isOn)
            {
                controlAudio.PlayAudio(0);
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;

                _manager.AddPoints(gameObject);

            }
            else
            {
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;
                _manager.DeletePoint(gameObject);

            }
        }
        else {
            
            if (toggle.isOn)
            {
                controlAudio.PlayAudio(0);
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
                _manager.SetLine(toggle);
            }
            else
            {
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;

                if (_manager.x)
                {
                    Destroy(_manager.x);
                    _manager.ClearTemp();
                }


            }
        }
    }
}
