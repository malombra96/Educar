using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A81_card : MonoBehaviour
{

    public GameObject subCard, coupleCard;
    Button myButton;
    M9A81_managerCard _Manager;
    public ControlAudio audio;

    public List<Sprite> _sprite;

    public bool a, review;

    public Vector3 posicion, posicionSub;

    public List<GameObject> _TargetsList;


    // Start is called before the first frame update
    void Start()
    {
        a = false;
        
        _Manager = GameObject.FindObjectOfType<M9A81_managerCard>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(delegate { ShowSubCard(myButton); });
        _Manager.allCubes.Add(gameObject);
        posicion = GetComponent<RectTransform>().anchoredPosition;
        posicionSub = subCard.GetComponent<RectTransform>().anchoredPosition;

    }

    private void Update()
    {
        if (review)
        {
              subCard.GetComponent<Animator>().SetBool("x", true);
        }
        else
        {
              subCard.GetComponent<Animator>().SetBool("x", false);
        }
    }

    public void StartGame()
    {
        GetComponent<Image>().sprite = _sprite[1];
        GetComponent<Button>().interactable = false;
        subCard.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(hideCard());
    }

    IEnumerator hideCard()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Image>().sprite = _sprite[0];
        GetComponent<Button>().interactable = true;
        a = true;

    }



    public void ShowSubCard(Button button)
    {
        if (a)
        {
            audio.PlayAudio(0);
            GetComponent<Animator>().SetBool("x",true);
            _Manager.CompararCubos(gameObject);
            StartCoroutine(x());
        }


    }

    IEnumerator x()
    {
        yield return new WaitForSeconds(0.5f);
        subCard.GetComponent<Animator>().SetBool("x", true);
    }
}
