using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A119_managerGeneral : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bandera;
    public Image aplico_monta;
    public Sprite bgDay, bgMidday, bgNight;
    public Vector2 posIniPlayer, posIniBG,target;
    public GameObject player,BG,fuego,monta;
    public int count,count_2,countInput,countDrag,countReview;
    public List<GameObject> points, excersises,drags,input,elementos;
    public bool movePlayer, moveBG;
    public float speedPlayer,speedBG;
    public ControlNavegacion controlNavegacion;
    public List<Sprite> spriteFuego;
    public GameObject nextArrow, previousArrow;
    public bool review,first;
    public ControlPuntaje controlPuntaje;

    void Start()
    {
        first = true;
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        previousArrow.SetActive(false);
        // review = false;
        foreach (var item in elementos)
        {
            item.gameObject.SetActive(false);
        }
        elementos[0].SetActive(true);
        Init();
        player.GetComponent<Animator>().SetBool("x", true);
        StartCoroutine(y());

    }

    // Update is called once per frame
    void Update()
    {
        if (review)
        {
            if (countReview == 0)
            {
                previousArrow.SetActive(false);
            }
            if (countReview == elementos.Count)
            {
                nextArrow.SetActive(false);
                previousArrow.SetActive(true);
            }
            if (countReview < elementos.Count && countReview > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }
        switch (count_2) {
            case 1:
                aplico_monta.sprite = bgDay;
                break;
            case 2:
                aplico_monta.sprite = bgDay;
                break;
            case 3:
                aplico_monta.sprite = bgMidday;
                break;
            case 4:
                aplico_monta.sprite = bgMidday;
                break;
            case 5:
                aplico_monta.sprite = bgNight;
                break;
            case 6:
                aplico_monta.sprite = bgNight;
                break;

                
        }
        if (movePlayer) {
            float step = speedPlayer * Time.deltaTime;
            if (count_2 < excersises.Count) {
                player.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(player.GetComponent<RectTransform>().anchoredPosition, points[count_2].GetComponent<RectTransform>().anchoredPosition, step);
                if (player.GetComponent<RectTransform>().anchoredPosition == points[count_2].GetComponent<RectTransform>().anchoredPosition)
                {
                    movePlayer = false;
                }
            }
            
        }
        if (moveBG) {
            float step = speedBG * Time.deltaTime;
            BG.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(BG.GetComponent<RectTransform>().anchoredPosition, target, step);
            if (BG.GetComponent<RectTransform>().anchoredPosition == target)
            {
                moveBG = false;
            }
        }        
    }
    public void ActivateReview()
    {
        bandera.SetActive(false);
        review = true;
        count = 0;
        foreach (var item in elementos)
        {
            item.SetActive(false);
        }
        elementos[0].SetActive(true);
    }
    public void NextQuestion()
    {
        countReview++;
        if (countReview == elementos.Count)
        {
            controlNavegacion.GoToLayout(5);
        }
        else if (countReview <= elementos.Count)
        {
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            elementos[countReview].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        if (countReview > 0)
        {
            countReview--;
            foreach (var item in elementos)
            {
                item.SetActive(false);
            }
            elementos[countReview].SetActive(true);
        }
    }
    public void Init() {
        if (!review) {
            bandera.SetActive(false);
            aplico_monta.sprite = bgDay;
            count = 0;
            count_2 = 0;
            countDrag = 0;
            countInput = 0;
            player.GetComponent<Animator>().SetBool("x", false);
            player.GetComponent<RectTransform>().anchoredPosition = posIniPlayer;
            BG.GetComponent<RectTransform>().anchoredPosition = posIniBG;
            movePlayer = true;
            moveBG = false;
            foreach (var p in points)
            {
                p.SetActive(true);
                p.GetComponent<Image>().sprite = p.GetComponent<BehaviourSprite>()._default;
            }
            foreach (var e in excersises)
            {
                e.SetActive(false);
            }
            foreach (var d in drags)
            {
                d.SetActive(false);
            }
            foreach (var i in input)
            {
                i.SetActive(false);
            }
            fuego.SetActive(false);
            fuego.GetComponent<Image>().sprite = spriteFuego[0];
            monta.SetActive(true);
        }
       
    }
    IEnumerator y() {
        yield return new WaitForSeconds(1f);
        excersises[count].SetActive(true);
        player.GetComponent<Animator>().SetBool("x", false);
    }
    public void NextExcersise(bool value) {
        if (count < excersises.Count)
        {
            StartCoroutine(x(value));
        }
    }
    public IEnumerator NextExcersiseDrag()
    {
        yield return new WaitForSeconds(1f);
        drags[countDrag].SetActive(false);
        countDrag++;
        if (countDrag < drags.Count)
        {
            drags[countDrag].SetActive(true);
           
        }
        //yield return new WaitForSeconds(1f);
        if (countDrag == drags.Count)
        {
            input[0].SetActive(true);
            //fuego.SetActive(true);
        }
    }
    public void NextExcersiseInput(bool value)
    {
        if (countInput < input.Count)
        {
            if (value)
            {
                fuego.SetActive(true);
                StartCoroutine(z());
            }
            else {
                fuego.SetActive(false);
                StartCoroutine(z());
            }
            
        }
    }
    IEnumerator z()
    {
       
        yield return new WaitForSeconds(1.0f);
        input[countInput].SetActive(false);
        fuego.SetActive(false);
        //yield return new WaitForSeconds(1f);

        countInput++;
        if (countInput == input.Count)
        {
            if (controlPuntaje._rightAnswers == controlPuntaje.questions)
            {
                input[countInput-1].SetActive(true);
                //controlNavegacion.Forward(1.0f);
                controlNavegacion.GoToLayout(5);
                //StartCoroutine(m());
            }
            else
            {
                input[countInput-1].SetActive(true);
                controlNavegacion.GoToLayout(5);
                //StartCoroutine(m());
            }

        }else 
        if (countInput < input.Count)
        {
            input[countInput].SetActive(true);
            
        }
       
    }

    IEnumerator m() {
        yield return new WaitForSeconds(0f);
        
    }
    IEnumerator x(bool value) {
        yield return new WaitForSeconds(1f);
        excersises[count].SetActive(false);
        if (value)
        {
            player.GetComponent<Animator>().SetBool("x", true);
            movePlayer = true;
            moveBG = true;
            target = new Vector2(BG.GetComponent<RectTransform>().anchoredPosition.x -300, BG.GetComponent<RectTransform>().anchoredPosition.y-300);
            if (count < excersises.Count)
            {
                points[count].GetComponent<Image>().sprite = points[count].GetComponent<BehaviourSprite>()._right;
                count_2++;
            }
        }
        else
        {
            if (count < excersises.Count)
            {
                points[count].GetComponent<Image>().sprite = points[count].GetComponent<BehaviourSprite>()._wrong;
            }
        }
        count++;
        
        yield return new WaitForSeconds(1f);
        if (count < excersises.Count)
        {
            excersises[count].SetActive(true);
        }

        if (count == excersises.Count)
        {
            //StartCoroutine(po());
            yield return new WaitForSeconds(0.5f);
            if (count == controlPuntaje._rightAnswers)
            {
                bandera.SetActive(true);
                yield return new WaitForSeconds(1f);
                bandera.SetActive(false);
                drags[0].SetActive(true);
                monta.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                drags[0].SetActive(true);
                monta.SetActive(false);
            }
        }
    }

    IEnumerator po() {
        yield return new WaitForSeconds(0.5f);
       
    }
    public void ResetAll() {
        if (first) {
            Init();
            foreach (var e in excersises)
            {
                e.GetComponent<M7A119_managerSeleccionar>().ResetSeleccionarToggle();
            }
            foreach (var d in drags)
            {
                d.GetComponent<M7A119_dragManager>().ResetDragDrop();
            }
            foreach (var i in input)
            {
                i.GetComponent<M7A119_inputManager>().resetAll();
            }
            foreach (var item in elementos)
            {
                item.gameObject.SetActive(false);
            }
            //StartCoroutine(y());
            Invoke("pp",2);

            //elementos[0].SetActive(true);
        }
    }
    public void pp()
    {
        //yield return new WaitForSeconds(2f);
        print("s");
        elementos[0].SetActive(true);
    }
}

