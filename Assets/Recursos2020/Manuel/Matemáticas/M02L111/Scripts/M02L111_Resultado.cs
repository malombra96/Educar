using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M02L111_Resultado : MonoBehaviour
{
    public M02L111_ControlDinosaur _ControlDinosaur;

    void OnEnable()=> _ControlDinosaur.SetResultultado();

}
