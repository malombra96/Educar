using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8L56Dardos : MonoBehaviour
{
    public bool acerte;
    public M8L56Colliders diana;
    private void OnEnable() => StartCoroutine(Acerto());
    IEnumerator Acerto()
    {
        yield return new WaitForSeconds(0.8f);
        if (!acerte)
            diana.darpunto(acerte);
        this.gameObject.SetActive(acerte);
    }

}
