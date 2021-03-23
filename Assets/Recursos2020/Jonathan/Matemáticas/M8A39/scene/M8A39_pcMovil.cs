using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A39_pcMovil : MonoBehaviour
{

    public Image image;
    public List<Sprite> sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            image.sprite = sprite[1];
            image.SetNativeSize();
        }
        else {
            image.sprite = sprite[0];
            image.SetNativeSize();
        }
    }
}
