using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L037T_ControlBeeDrag : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    [Header("Managers Drag&Drop")] public ManagerDragDrop[] _ManagerDragDrop;
    ManagerDragDrop _active;
    BehaviourLayout currentLayout;

    GameObject _honeycomb;

    int n;

    void Start()
    {
        n=-1;

        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _honeycomb = transform.GetChild(0).gameObject;

        for (int i = 0; i < _honeycomb.transform.childCount; i++)
            _honeycomb.transform.GetChild(i).gameObject.SetActive(false);

        foreach (var manager in _ManagerDragDrop)
            manager._validar.onClick.AddListener(delegate {GetAnswer(manager);});
    }

    void Update()
    {
        if(_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
            _honeycomb.SetActive(((currentLayout._Layout == BehaviourLayout.TipoLayout.Actividad) && currentLayout.GetComponent<ManagerDragDrop>()));
        
    }

    public void GetAnswer(ManagerDragDrop manager)
    {
        _active = manager;
        StartCoroutine(DelayGetAnswer());
    } 
    

    IEnumerator DelayGetAnswer()
    {
        yield return new WaitForSeconds(.1f);

        foreach (var answer in _active.answers)
            if (answer.Key.GetComponent<BehaviourDrag>() && answer.Value)
                n++;

        for (int i = 0; i <= n; i++)
            _honeycomb.transform.GetChild(i).gameObject.SetActive(true);
    }

    public void ResetContadorBee()
    {
        n=-1;

        for (int i = 0; i < _honeycomb.transform.childCount; i++)
            _honeycomb.transform.GetChild(i).gameObject.SetActive(false);
    }
}
