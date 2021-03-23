using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_learn : MonoBehaviour
{

    public bool review;
    public int count = 0;
    public List<GameObject> _images;

    public GameObject nextArrow, previousArrow;

//    [SerializeField] Control_Audio _Audio;
    [SerializeField] ControlNavegacion _Navegation;

    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.GetComponent<Button>().interactable = false;
        review = false;
        _images[count].SetActive(true);


    }
    void Update()
    {
        
            if (count > 0)
            {
                previousArrow.GetComponent<Button>().interactable = true;
            }
            else if (count == 0)
            {
                previousArrow.GetComponent<Button>().interactable = false;
            } else if (count == _images.Count) 
            {
                nextArrow.GetComponent<Button>().interactable = false;
            } else if (count == _images.Count -1) {
                nextArrow.GetComponent<Button>().interactable = true;
            }
       


    }

    public void NextQuestion()
    {
        if (count < _images.Count-1) {
            count++;
            if (count <= _images.Count - 1)
            {

                _images[count].SetActive(true);
            }
        }
        

        
    }
    public void PreviousQuestion()
    {

        if (count > 0)
        {
            
            _images[count].SetActive(false);
            count--;
        }
        
    }
}
