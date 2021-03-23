using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BehaviourOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        SetChilds(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //print("Exit");
        StartCoroutine(ShowControllerVideo());
    }

    IEnumerator ShowControllerVideo()
    {
        yield return new WaitForSeconds(2);
        SetChilds(false);

    }

    void SetChilds(bool state)
    {
        
        //print("SetChilds");
        
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(state);
    }

}
