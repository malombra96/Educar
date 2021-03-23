using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4A255_principl : MonoBehaviour
{
    public int q;
    public ControlNavegacion controlNavegacion;
    // Start is called before the first frame update
    void Start()
    {
        q = PlayerPrefs.GetInt("conozco");
        if (q == 1)
        {
            controlNavegacion.GoToInicialConozco();
            PlayerPrefs.DeleteAll();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
