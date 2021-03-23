using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A51_instruction : MonoBehaviour
{

    public bool review;
    public int count = 0;
    public List<GameObject> _images;

    public Image mobileImage, pcImage;

    public GameObject nextArrow, previousArrow;

    public List<GameObject> acelerar;

    //    [SerializeField] Control_Audio _Audio;
    


    private void Awake()
    {
        if (Application.isMobilePlatform)
        {
            _images[1] = mobileImage.gameObject;
            foreach (var x in acelerar)
                x.SetActive(true);
        }
        else
        {
            _images[1] = pcImage.gameObject;
            foreach (var x in acelerar)
                x.SetActive(false);
        }
    }

    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.GetComponent<Button>().interactable = true;
        review = false;
        _images[count].SetActive(true);



    }
    void Update()
    {

        if (count > 0)
        {
            previousArrow.SetActive(true);
        }
        else if (count == 0)
        {
            previousArrow.SetActive(false);
        }

        if (count == _images.Count - 1)
        {
            nextArrow.SetActive(false);
        }
        else {
            nextArrow.SetActive(true);
        }



    }

    public void NextQuestion()
    {
        if (count < _images.Count - 1)
        {
            count++;
            for (int i = 0; i < _images.Count; i++)
            {
                _images[i].SetActive(false);
            }
            if (count <= _images.Count - 1)
            {

                _images[count].SetActive(true);
            }
        }



    }
    public void PreviousQuestion()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            _images[i].SetActive(false);
        }


        if (count > 0)
        {
            count--;

            _images[count].SetActive(true);
            
        }

    }

    public void ResetAll()
    {
        if (Application.isMobilePlatform)
        {
            _images[1] = mobileImage.gameObject;
            foreach (var x in acelerar)
                x.SetActive(true);
        }
        else
        {
            _images[1] = pcImage.gameObject;
            foreach (var x in acelerar)
                x.SetActive(false);
        }
        count = 0;


    }
}
