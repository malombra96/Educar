using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L4A255_carrosuel : MonoBehaviour
{
    public List<GameObject> informacion;
    public int count = 0;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;
    // Start is called before the first frame update
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in informacion)
        {
            item.gameObject.SetActive(false);
        }
        informacion[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0)
        {
            previousArrow.SetActive(false);
        }
        if (count == informacion.Count - 1)
        {
            nextArrow.SetActive(false);
        }
        if (count < informacion.Count - 1 && count > 0)
        {
            nextArrow.SetActive(true);
            previousArrow.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == informacion.Count)
        {

        }
        else if (count <= informacion.Count)
        {
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count].SetActive(true);
        }
    }
}
