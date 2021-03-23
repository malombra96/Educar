using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A130_punto : MonoBehaviour
{
    // Start is called before the first frame update

    public bool correcto,op,x;
    public M9A130_managerToggle general;
    Button b;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click() {
        general.añadir(gameObject);
    }
}
