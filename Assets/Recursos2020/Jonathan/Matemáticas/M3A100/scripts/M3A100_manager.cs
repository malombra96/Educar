using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class M3A100_manager : MonoBehaviour
{
    public GameObject currentExercise;
    public GameObject navBar;
    public GameObject LifesGameobject;
    public int lifes;
    public GameObject choosePlayerGameobject;
    public Button girlButton, boyButton;
    public GameObject player,bg;
    public ControlAudio controlAudio;
    public ControlNavegacion  controlNavegacion;
    public List<M3A100_managerSeleccionar> LisExercises;

    [Range (1f, 200f)]
    public float scrollSpeed = 0.25f;
    public float scrollOffset;
    Vector2 startPos;
    public bool move;

    public GameObject reviewIMage;
    public GameObject nextArrow, previousArrow;
    public bool review,first;
    public int count = 0;

    void Start()
    {
        girlButton.onClick.AddListener(delegate { ChoosePlayer(girlButton.gameObject); });
        boyButton.onClick.AddListener(delegate { ChoosePlayer(boyButton.gameObject); });
        
        foreach (var e in LisExercises) {
            e.gameObject.SetActive(false);
        }
        bg.SetActive(false);
        

        startPos = bg.GetComponent<RectTransform>().anchoredPosition;
        LifesGameobject.SetActive(false);

        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);
        first = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (move) {
            float newPos = Mathf.Repeat(Time.time * scrollSpeed,scrollOffset);
            bg.GetComponent<RectTransform>().anchoredPosition = bg.GetComponent<RectTransform>().anchoredPosition + Vector2.left * newPos;
        }

        if (review)
        {
            if (count > 0)
            {
                previousArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
            if (count == 0)
            {
                previousArrow.SetActive(false);
                LisExercises[0].gameObject.SetActive(true);
            }
            if (count > LisExercises.Count)
            {
                //aplic.SetActive(false);
                //aplico2.SetActive(true);
                reviewIMage.SetActive(true);
                reviewIMage.GetComponent<M8A39_review1>().count = 0;
            }
        }
    }

    public void ChoosePlayer(GameObject g) {
        
        controlAudio.PlayAudio(0);
        
        bg.SetActive(true);
        player.GetComponent<Image>().sprite = g.GetComponent<Image>().sprite;
        player.GetComponent<Image>().SetNativeSize();
        player.GetComponent<Image>().color = new Color32(255,255,255,255);
        choosePlayerGameobject.SetActive(false);
        LisExercises[0].gameObject.SetActive(true);
        LifesGameobject.SetActive(true);
        navBar.SetActive(false);
    }

    public void NextExercise(M3A100_managerSeleccionar g) {
        move = true;
        
        StartCoroutine(MoveBG(g));
    }

    public void NextExerciseWrong(M3A100_managerSeleccionar g)
    {
        StartCoroutine(MoveBGWrong(g));
    }

    IEnumerator MoveBGWrong(M3A100_managerSeleccionar g)
    {
        yield return new WaitForSeconds(1);
        int index = LisExercises.IndexOf(g);
        LisExercises[index].gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        if (index < LisExercises.Count - 1)
        {
            LisExercises[index + 1].gameObject.SetActive(true);
        }
        if (index == LisExercises.Count - 1)
        {
            print("ss");
            controlNavegacion.Forward(2.0f);
        }
    }

    IEnumerator MoveBG(M3A100_managerSeleccionar g) {
        
        navBar.SetActive(false);
        yield return new WaitForSeconds(1);
       
        int index = LisExercises.IndexOf(g);
        LisExercises[index].gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        move = false;        
        if (index < LisExercises.Count-1)
        {
            LisExercises[index + 1].gameObject.SetActive(true);
        }
        if (index == LisExercises.Count-1) {
            print("ss");
            controlNavegacion.Forward(2.0f);
        }
        
        navBar.SetActive(true);

    }

    public void ResetAll() {
        if (first) {
            foreach (var e in LisExercises)
            {
                e.gameObject.SetActive(false);
                e.ResetSeleccionarToggle();
            }
            lifes = 3;
            for (int i = 0; i < LifesGameobject.transform.childCount; i++)
            {
                LifesGameobject.transform.GetChild(i).gameObject.SetActive(true);
            }
            player.GetComponent<Image>().sprite = null;
            player.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            choosePlayerGameobject.SetActive(true);
            bg.SetActive(false);
            bg.GetComponent<RectTransform>().anchoredPosition = startPos;
            LifesGameobject.SetActive(false);


        }
        
    }

    public void Review() {
        foreach (var e in LisExercises)
        {
            e.gameObject.SetActive(false);
        }
        bg.SetActive(true);
        player.SetActive(true);
        startPos = bg.GetComponent<RectTransform>().anchoredPosition;
        LifesGameobject.SetActive(false);
        choosePlayerGameobject.SetActive(false);
        previousArrow.SetActive(false);
        review = true;
        reviewIMage.SetActive(true);
        LisExercises[0].gameObject.SetActive(true);
    }

    public void NextQuestion()
    {
        
        controlAudio.PlayAudio(0);
        
        if (count < LisExercises.Count)
        {
            count++;
            foreach (var item in LisExercises)
            {
                item.gameObject.SetActive(false);
            }
            if (count == LisExercises.Count)
            {
                //aplic.SetActive(false);
                //aplico2.SetActive(true);
                controlNavegacion.Forward();
                count = 4;
                LisExercises[count].gameObject.SetActive(true);
            }
            else {
                LisExercises[count].gameObject.SetActive(true);
            }
            
        }


    }
    public void PreviousQuestion()
    {
        controlAudio.PlayAudio(0);
        if (count > 0)
        {
            count--;
            foreach (var item in LisExercises)
            {
                item.gameObject.SetActive(false);
            }
            LisExercises[count].gameObject.SetActive(true);
        }
    }
}
