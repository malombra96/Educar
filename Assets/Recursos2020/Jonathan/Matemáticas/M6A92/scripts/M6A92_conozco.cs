using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6A92_conozco : MonoBehaviour
{

    public int count = 0;
    public List<Sprite> spritePasos,spriteBackground;
    public Image imagenPasos,backgroundImage;
    public List<GameObject> objects;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;


    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);

        backgroundImage.sprite = spriteBackground[0];
        imagenPasos.sprite = spritePasos[0];
        foreach (var o in objects)
            o.SetActive(false);
        objects[0].SetActive(true);


    }
    void Update()
    {

        if (count > 0)
        {
            previousArrow.SetActive(true);
        }
        if (count == 0)
        {
            previousArrow.SetActive(false);
        }
        if (count == spritePasos.Count - 1)
        {
            nextArrow.SetActive(false);
        }
        if (count == spritePasos.Count - 2)
        {
            nextArrow.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);

        if (count <= spritePasos.Count)
        {
            imagenPasos.sprite = spritePasos[count];
            backgroundImage.sprite = spriteBackground[count];

            foreach (var o in objects)
                o.SetActive(false);

            objects[count].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            imagenPasos.sprite = spritePasos[count];
            backgroundImage.sprite = spriteBackground[count];
            foreach (var o in objects)
                o.SetActive(false);
            objects[count].SetActive(true);
        }
    }
}
