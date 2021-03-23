using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer y;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        print("x");
        y.color = new Color32(100, 200, 100, 255);
    }

}
