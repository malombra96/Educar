using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A241LightBox : MonoBehaviour
{
    public List<Button> buttons;
    public List<GameObject> info;
    ControlAudio controlAudio;

    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        foreach (var buton in buttons)
            buton.onClick.AddListener( delegate { activar(buton.transform.GetSiblingIndex()); });        
    }

    // Update is called once per frame
    void activar(int idex)
    {
        controlAudio.PlayAudio(0);
        info[idex].SetActive(true);
    }
    public void Closed()
    {
        controlAudio.PlayAudio(0);
        foreach (var i in info)
            i.SetActive(false);
    }
}
