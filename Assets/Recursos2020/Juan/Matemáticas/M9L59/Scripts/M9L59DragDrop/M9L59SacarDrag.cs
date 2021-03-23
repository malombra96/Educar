using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9L59SacarDrag : MonoBehaviour
{
    public Transform padre;
    public Transform other;

    // Start is called before the first frame update
    void Start()
    {
        //padre = ge
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<M9L59BehaviourDrag>()._drop && GetComponent<CanvasGroup>().blocksRaycasts)
            GetComponent<Transform>().SetParent(padre);
        else
            GetComponent<Transform>().SetParent(other);
    }
}
