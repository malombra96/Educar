using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L11A241Laverinto : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    public GameObject pc;
    public GameObject movil;
    int x;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<BehaviourLayout>()._isEvaluated = true;
        if (Application.isMobilePlatform)
        {
            pc.SetActive(false);
            movil.SetActive(true);
        }
        else
        {
            pc.SetActive(true);
            movil.SetActive(false);
        }
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        x = 5;
    }    
    public IEnumerator cambioDeActividad()
    {
        controlAudio.PlayAudio(0);
        yield return new WaitForSeconds(.1f);
        controlNavegacion.GoToLayout(x, 1);
        if (x < 7)
            x++;
        else
            StartCoroutine(apagar());
    }
    IEnumerator apagar()
    {
        yield return new WaitForSeconds(.8f);
        transform.GetChild(2).gameObject.SetActive(false);
    }
    public void resetAll()
    {
        x = 5;
        if (GetComponent<BehaviourLayout>()._isEvaluated)
        {
            transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = transform.GetChild(2).GetComponent<L11A241Player>().posDefault;
            transform.GetChild(2).gameObject.SetActive(true);
        }

        for (int i = 0; i < transform.GetChild(1).childCount; i++)
            transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
    }
}
