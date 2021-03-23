using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class M4A112_A3 : MonoBehaviour
{
    public List<GameObject> inputs,candados;
    public int count,correctas,count2;
    public ControlNavegacion controlNavegacion;
    public GameObject resultado;
    public bool review;
    public GameObject nextArrow, previousArrow;
    public GameObject navbar;


    // Start is called before the first frame update
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        foreach (var i in inputs) {
            i.SetActive(false);
        }
        foreach (var c in candados)
        {
            c.GetComponent<Image>().sprite = c.GetComponent<BehaviourSprite>()._default;
        }
        inputs[0].SetActive(true);
        resultado.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (review)
        {
            //if (count > 0)
            //{
            //    previousArrow.SetActive(true);
            //    previousArrow.SetActive(true);
            //}
            //if (count == 0)
            //{
            //    previousArrow.SetActive(false);
            //}
        }
    }

    public void ActivateReview()
    {
        review = true;
        count2 = 0;
        foreach (var item in inputs)
        {
            item.SetActive(false);
        }

        inputs[0].SetActive(true);
        resultado.SetActive(false);
    }

    public void NextQuestion()
    {
        count2++;

        if (count2 == inputs.Count)
        {
            controlNavegacion.Forward();
        }
        else if (count2 <= inputs.Count)
        {


            foreach (var item in inputs)
            {
                item.SetActive(false);
            }

            inputs[count2].SetActive(true);

        }
    }

    public void PreviousQuestion()
    {
        if (count2 >= 0)
        {
            count2--;
            foreach (var item in inputs)
            {
                item.SetActive(false);
            }

            if (count2 == -1)
            {
                inputs[count2].SetActive(true);
                count2 = 0;
                controlNavegacion.Backward();
            }
            else {
                inputs[count2].SetActive(true);
            }
        }
    }


    public IEnumerator Next(bool value) {
        yield return new WaitForSeconds(2f);


        if (value)
        {
            candados[count].GetComponent<Image>().sprite = candados[count].GetComponent<BehaviourSprite>()._right;
        }
        foreach (var i in inputs)
        {
            i.SetActive(false);
        }
        count++;
        if (count < inputs.Count)
        {
            inputs[count].SetActive(true);
        }

        if (count == inputs.Count)
        {
            if (correctas == inputs.Count)
            {
                resultado.SetActive(true);
                navbar.SetActive(false);
                controlNavegacion.Forward(3.0f);
            }
            else
            {
                controlNavegacion.Forward(1.0f);
            }

        }

    }

    public void ResetAll() {
        count = 0;
        resultado.SetActive(false);
        foreach (var i in inputs)
        {
            i.GetComponent<M4A112_managerA3>().resetAll();
            i.SetActive(false);
        }
        inputs[0].SetActive(true);
        foreach (var c in candados)
        {
            c.GetComponent<Image>().sprite = c.GetComponent<BehaviourSprite>()._default;
        }
        correctas = 0;
    }
}
