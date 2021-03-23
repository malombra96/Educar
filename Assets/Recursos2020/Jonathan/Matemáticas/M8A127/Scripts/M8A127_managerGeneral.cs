using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A127_managerGeneral : MonoBehaviour
{
    public bool review;
    public GameObject nextArrow, previousArrow;
    public List<GameObject> excersises;
    public List<Sprite> homeSprites;
    public int count,count2,rights;
    public GameObject /*home,*/ home_1, result, instruction, end;
    [SerializeField] ControlNavegacion controlNavegacion;
    public bool first;
    public List<GameObject> home,fuegos;
    
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        first = true;
        init();
    }

    void Update()
    {
        if (review)
        {

            if (count2 > 0)
            {
                previousArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
            if (count2 == 0)
            {
                previousArrow.SetActive(false);
            }
            if (count2 > excersises.Count)
            {
                
            }
        }
    }

    public void ActivateReview()
    {
        review = true;
        count2 = 0;
        instruction.SetActive(false);
        end.SetActive(false);
        foreach (var item in excersises)
        {
            item.SetActive(false);
        }

        excersises[0].SetActive(true);

        foreach (var f in fuegos)
        {
            f.SetActive(false);
        }
    }
    public void NextQuestion()
    {
        count2++;
        if (count2 == excersises.Count)
        {
            controlNavegacion.Forward();
        }
        else if (count2 <= excersises.Count)
        {
            foreach (var item in excersises)
            {
                item.SetActive(false);
            }

            excersises[count2].SetActive(true);

        }


    }
    public void PreviousQuestion()
    {
        if (count2 > 0)
        {
            count2--;
            foreach (var item in excersises)
            {
                item.SetActive(false);
            }
            excersises[count2].SetActive(true);
        }
    }


    public void init() {
        if (!review) {
            rights = 0;
            foreach (var f in fuegos)
            {
                f.SetActive(false);
            }
            foreach (var e in excersises)
            {
                e.SetActive(false);
            }
            instruction.SetActive(true);
            count = 0;
            foreach (var h in home)
            {
                h.GetComponent<Image>().sprite = h.GetComponent<BehaviourSprite>()._default;
            }
           // home.GetComponent<Image>().sprite = home.GetComponent<BehaviourSprite>()._default;
            home_1.GetComponent<Image>().sprite = home_1.GetComponent<BehaviourSprite>()._default;
            end.SetActive(false);
        }
        
    }

    public IEnumerator NextExcersise(bool value) {

        if (value) {
            fuegos[count].SetActive(true);
            foreach (var h in home)
            {
                h.GetComponent<Image>().sprite = homeSprites[rights];
            }
            //home.GetComponent<Image>().sprite = homeSprites[rights];
            home_1.GetComponent<Image>().sprite = homeSprites[rights];
            rights++;
        }

        yield return new WaitForSeconds(1.5f);
        //fuegos.SetActive(false);
        if (count < excersises.Count) {
            excersises[count].SetActive(false);
            count++;

            if (count == excersises.Count) {
                if (rights == excersises.Count)
                {
                    result.GetComponent<Image>().sprite = result.GetComponent<BehaviourSprite>()._right;
                    result.GetComponent<Image>().SetNativeSize();
                }
                else {
                    result.GetComponent<Image>().sprite = result.GetComponent<BehaviourSprite>()._wrong;
                    result.GetComponent<Image>().SetNativeSize();
                }
                end.SetActive(true);
                controlNavegacion.Forward(3.0f);
            }
            else {
                excersises[count].SetActive(true);
            }
        }
    }

    public void ResetAll() {
        if (first) {
            
            for (int i = 0; i < excersises.Count; i++)
            {
                if (excersises[i].GetComponent<M8A127_managerSeleccionar>())
                {
                    excersises[i].GetComponent<M8A127_managerSeleccionar>().ResetSeleccionarToggle();
                }
                if (excersises[i].GetComponent<M8A127_managerInput>())
                {
                    excersises[i].GetComponent<M8A127_managerInput>().resetAll();
                }
                if (excersises[i].GetComponent<M8A127_managerDrag>())
                {
                    excersises[i].GetComponent<M8A127_managerDrag>().ResetDragDrop();
                }
            }
            init();
        }
    }

}

