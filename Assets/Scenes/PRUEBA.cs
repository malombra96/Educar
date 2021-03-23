using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PRUEBA : MonoBehaviour
{
    public InputField input;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void load()
    {
        string scene = input.text;
        SceneManager.LoadScene(scene);
    }

    public void loadFlipbook()
    {
        SceneManager.LoadScene("book");
    }
}
