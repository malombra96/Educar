using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09A075_ManagerTrailSelect : MonoBehaviour
{
    [HideInInspector] public ControlNavegacion _controlNavegacion;

    [Header("Levels")] public List<M09A075_ManagerSeleccionarToggle> _levels;

    BehaviourLayout currentLayout;

    [Header("Background Trail")] public RectTransform bgTrail;
    [Header("Block Static")] public RectTransform _blocks;
    [Header("Range Enable")] public int _range;

    [Header("Lifes")] public RectTransform _lifes;
    public int nLifes;

    void OnEnable()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
    }

    void Start()
    {
        _range = 5;
        nLifes = 2;
    } 

    void Update()
    {
        if(_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
        {   
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(((currentLayout._Layout == BehaviourLayout.TipoLayout.Actividad) && currentLayout.GetComponent<M09A075_ManagerSeleccionarToggle>()));
        }

        for (int i = 0; i < _blocks.childCount; i++)
            _blocks.GetChild(i).gameObject.SetActive(i <= _range);

        for (int i = 0; i < _lifes.childCount; i++)
            _lifes.GetChild(i).gameObject.SetActive(i<=nLifes);
            
    }

    public void ResetManager()
    {
        _range = 5;
        nLifes = 2;
    }
}
