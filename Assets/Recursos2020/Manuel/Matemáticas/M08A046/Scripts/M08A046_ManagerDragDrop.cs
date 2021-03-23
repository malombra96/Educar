using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class M08A046_ManagerDragDrop : MonoBehaviour
{
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public Canvas canvas; 

    [Header("Button Validar")] public Button _validar;
    [Header("Box Pivot")] public Transform _boxPivot;

    [Header("Box Cubes")] public Transform[] _boxCube;

    [HideInInspector]  public List<GameObject> _cubes;
    [Header("Cubes ON")] public int countDrop;

    [Header("LightBoxNumPad")] public GameObject _lighBox;

    float H;
    float V;

    void OnEnable() => _boxPivot.eulerAngles = new Vector3(-25,40,0);

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        canvas = FindObjectOfType<Canvas>(); 

        countDrop = -1;

        foreach (Transform box in _boxCube)
        {
            for (int i = 0; i < box.childCount; i++)
            {
                _cubes.Add(box.GetChild(i).gameObject);
                box.GetChild(i).gameObject.SetActive(false);
            }
                
        }
        
        SetValidar(); 
            
    }
    void LateUpdate()
    {
        _boxPivot.Rotate(0f, -SimpleInput.GetAxis("Horizontal"), 0f, Space.World);
        _boxPivot.Rotate(SimpleInput.GetAxis("Vertical"),  0f, 0f, Space.Self);

        _boxPivot.gameObject.SetActive(!_lighBox.activeSelf);

    }

    public void SetStateCube()
    {
        for (int i = 0; i < _cubes.Count; i++)
            _cubes[i].SetActive(i<=countDrop);
    }

    public void SetValidar()
    {
        _validar.gameObject.SetActive(countDrop == (_cubes.Count - 1));
    }

    public void ResetDragDrop()
    {
        countDrop = -1;
        SetStateCube();
        SetValidar();
    }
}
