using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M10L33_element : MonoBehaviour
{
    public bool calificado;
    public M10L33_manager _managerGeneral;
    public int counter;
    public List<GameObject> child;
    public bool isActive;


    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GetQuestion() {
        counter++;

        if (counter == child.Count) {
            calificado = true;
            _managerGeneral.NextElement(gameObject);
        }
    }

}
