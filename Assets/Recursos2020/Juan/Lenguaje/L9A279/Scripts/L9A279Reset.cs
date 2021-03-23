using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L9A279Reset : MonoBehaviour
{
    public List<GameObject> instrucciones;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(apagar);
    }

    // Update is called once per frame
    void apagar()
    {
        foreach(var instruccion in instrucciones)
        {
            instruccion.SetActive(false);
            instruccion.transform.SetAsLastSibling();
        }
        instrucciones[0].SetActive(true);
    }
}
