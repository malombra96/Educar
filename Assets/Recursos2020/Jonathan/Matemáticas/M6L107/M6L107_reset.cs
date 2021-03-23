using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M6L107_reset : MonoBehaviour
{

    public List<M6L107_managerDrag> drags;
    public List<M6L107_managerToggle> toggles;
    public List<M6L107_managerInput> inputs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAll()
    {
        foreach (var d in drags) {
            d.ResetDragDrop();
        }

        foreach (var t in toggles)
        {
            t.ResetSeleccionarToggle();
        }

        foreach (var i in inputs)
        {
            i.resetAll();
        }
    }

    public void Review() {
        foreach (var t in toggles)
        {
            t.Review();
        }
    }
}
