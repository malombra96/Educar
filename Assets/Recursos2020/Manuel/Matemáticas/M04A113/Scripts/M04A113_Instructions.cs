using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M04A113_Instructions : MonoBehaviour
{
    [Header("Objects Image")]
    public GameObject _desktop;
    public GameObject _mobile;

    void Start()
    {
        _desktop.SetActive(!Application.isMobilePlatform);
        _mobile.SetActive(Application.isMobilePlatform);
    }
}
