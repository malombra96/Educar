using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L05A264_ManagerAplico : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;

    [Header("Girl Controller")] public L05A264_Player _girlBehaviour;
    [HideInInspector] public BehaviourLayout _current;
    public List<BehaviourLayout> _layouts;
    public List<L05A264_BehaviourCuadro> _BehaviourCuadros;

    public GameObject[] _tapas = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        StateDefault();
    }

    void LateUpdate()
    {
        _current = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>();           
    }

    void StateDefault()
    {
        _tapas[0].SetActive(true);
        _tapas[1].SetActive(true);

        GirlState(false);

        _girlBehaviour.transform.position = new Vector3(11.51f,2.75f,2.45f);
        _girlBehaviour.transform.eulerAngles = new Vector3(0,-90,0);

    }

    public void GirlState(bool state) => _girlBehaviour.enabled = state;

    public void ResetGeneral()
    {
        foreach (BehaviourLayout layout in _layouts)
            layout.GetComponent<L05A264_ManagerToggle>().ResetSeleccionarToggle();

        StateDefault();
    }
}
