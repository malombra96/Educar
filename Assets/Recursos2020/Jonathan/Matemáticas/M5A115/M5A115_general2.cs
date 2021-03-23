using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class M5A115_general2 : MonoBehaviour
{
    public List<Button> botonesNumero;
    public int preguntas,count;
    public List<GameObject> inputs;
    public M5A115_managerInput2 total;
    public ControlNavegacion ControlNavegacion;

    public bool review;
    public int count2 = 0;
    public List<GameObject> informacion;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;

    // Start is called before the first frame update
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (review)
        {
        }
    }

    public void ActivateReview()
    {
        review = true;
        count2 = 0;
        foreach (var item in informacion)
        {
            item.SetActive(false);
        }

        informacion[0].SetActive(true);
    }
    public void NextQuestion()
    {
        count2++;

        _Audio.PlayAudio(0);
        if (count2 == informacion.Count)
        {
            ControlNavegacion.Forward();
            count2 = informacion.Count;
        }
        else if (count2 <= informacion.Count)
        {
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count2].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        count2--;
        if (count2 >= 0)
        {
            
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count2].SetActive(true);

            
        }
        if (count2 == -1)
        {
            ControlNavegacion.Backward();
            ActivateReview();

        }
    }


    public IEnumerator CalificarPregunta(bool value,GameObject g,bool ultimo) {
        yield return new WaitForSeconds(2f);
        if (!ultimo)
        {
            g.transform.parent.gameObject.SetActive(false);
            count++;
            if (value)
            {
                preguntas++;
            }

            if (count == inputs.Count)
            {
                if (preguntas == inputs.Count)
                {
                    total.gameObject.SetActive(true);
                }
                else
                {
                    ControlNavegacion.Forward(2f);
                }
            }
        }
        else {
            yield return new WaitForSeconds(1f);
            //g.transform.parent.gameObject.SetActive(false);
            ControlNavegacion.Forward();
        }
    }

    public void ResetAll() {
        preguntas = 0;
        count = 0;
        total.gameObject.SetActive(false);
        total.GetComponent<M5A115_managerInput2>().resetAll();
        foreach (var b in botonesNumero) {
            b.interactable = true;
            b.GetComponent<Image>().sprite = b.GetComponent<BehaviourSprite>()._default;
            b.GetComponent<Image>().SetNativeSize();
        }
        foreach (var i in inputs) {
            i.SetActive(false);
            i.gameObject.transform.GetChild(2).gameObject.GetComponent<M5A115_managerInput2>().resetAll();
        }

    }
}
