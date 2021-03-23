using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourClosePopUp : MonoBehaviour
{
    ManagerPopUp _managerPopUp;
    void Start()
    {
        _managerPopUp = FindObjectOfType<ManagerPopUp>();
        GetComponent<Button>().onClick.AddListener(_managerPopUp.ClosePopUp);
    }

    

}
