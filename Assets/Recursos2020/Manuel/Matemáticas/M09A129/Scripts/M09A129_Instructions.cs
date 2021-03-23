using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A129_Instructions : MonoBehaviour
{
    [Header("Object Platform")] 
    public GameObject _mobile;
    public GameObject _desktop;


    // Start is called before the first frame update
    void Start()
    {
        _mobile.SetActive(Application.isMobilePlatform);
        _desktop.SetActive(!Application.isMobilePlatform);
    }
}
