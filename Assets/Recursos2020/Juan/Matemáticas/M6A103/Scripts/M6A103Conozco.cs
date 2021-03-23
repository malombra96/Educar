using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6A103Conozco : MonoBehaviour
{
    public Button next, back;
    int x;
    public List<Sprite> sprites;
    public Image informacion;

    ControlAudio controlAudio;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;

        controlAudio = FindObjectOfType<ControlAudio>();

        next.onClick.AddListener(siguente);
        back.onClick.AddListener(atras);

        informacion.sprite = sprites[0];
        back.gameObject.SetActive(false);

    }
    void siguente()
    {
        controlAudio.PlayAudio(0);
        if (x < sprites.Count-1)
        {           
            x++;
            informacion.sprite = sprites[x];

            if (x==1)
                back.gameObject.SetActive(true);

            if (x == sprites.Count - 1)
                next.gameObject.SetActive(false);
        }        
    }
    void atras()
    {
        controlAudio.PlayAudio(0);
        if (x > 0)
        {
            x--;
            informacion.sprite = sprites[x];

            if (x < sprites.Count - 1)
                next.gameObject.SetActive(true);

            if (x == 0)
                back.gameObject.SetActive(false);
        }      
           
    }
   
}
