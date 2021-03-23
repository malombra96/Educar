using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L85_managerBallon : MonoBehaviour
{
    public M6L85_puntaje _puntaje;
    // Start is called before the first frame update
    public int n,d;
    public List<Button> listBallon;
    public Button submitButton;
    public M6L85_globo ballon_1, ballon_2;
    public ControlAudio controlAudio;
    public ControlPuntaje controlPuntaje;
    public ControlNavegacion controlNavegacion;
    public GameObject numerator, denomitator;
    public int oportunities;
    public Text num, dem;
    public GameObject stars;

    public Texture2D cursorTexture, cursorTexture1;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool first;

    void Start()
    {
        submitButton.onClick.AddListener(Submit);
        StateInitial();
        first = true;
    }
    private void Update()
    {
        
    }
    public void StateInitial() {
        ballon_1 = null;
        ballon_2 = null;
        foreach (var b in listBallon)
        {
            b.interactable = true;
            b.GetComponent<Animator>().SetBool("op", false);
        }
        numerator.SetActive(false);
        denomitator.SetActive(false);
        num.text = "";
        dem.text = "";
        oportunities = 0;
        stars.transform.GetChild(0).gameObject.SetActive(true);
        stars.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void GetBallon(M6L85_globo currentBallon) {
        if (ballon_1 == null) {
            ballon_1 = currentBallon;
            num.text = currentBallon.number.ToString();
        }else  if (ballon_1 != null && ballon_2 == null) {
            ballon_2 = currentBallon;
            dem.text = currentBallon.number.ToString();
        }
       

        if (ballon_1 != null && ballon_2 != null) {
            foreach (var b in listBallon)
            {
                b.interactable = false;
            }
            StartCoroutine(delaySubmit());
        }
        
    }
    public void Submit() {
        submitButton.interactable = false;
        controlAudio.PlayAudio(0);
        

    }

    IEnumerator delaySubmit() {
        yield return new WaitForSeconds(1);

        if (ballon_1.isRight && (ballon_1.number == n))
        {
            numerator.SetActive(true);
            numerator.GetComponent<Image>().sprite = numerator.GetComponent<BehaviourSprite>()._right;
        }
        else {
            numerator.SetActive(true);
            numerator.GetComponent<Image>().sprite = numerator.GetComponent<BehaviourSprite>()._wrong;
        }

        if (ballon_2.isRight && ballon_2.number == d)
        {
            denomitator.SetActive(true);
            denomitator.GetComponent<Image>().sprite = denomitator.GetComponent<BehaviourSprite>()._right;
        }
        else
        {
            denomitator.SetActive(true);
            denomitator.GetComponent<Image>().sprite = denomitator.GetComponent<BehaviourSprite>()._wrong;
        }

        if (ballon_1.isRight && ballon_2.isRight && ballon_1.number == n && ballon_2.number == d)
        {
            controlAudio.PlayAudio(1);
            controlPuntaje.IncreaseScore();
            _puntaje.count += 15;
            _puntaje.textoPuntaje1.text = _puntaje.count.ToString();
            controlNavegacion.Forward(2.0f);
        }
        else {
            if (oportunities < 2 ) {
                oportunities++;
                stars.transform.GetChild(oportunities-1).gameObject.SetActive(false);
                controlAudio.PlayAudio(2);
                StartCoroutine(Refresh());
            }
            
        }

        if (oportunities == 2) {
            print("next");
            foreach (var b in listBallon)
            {
                b.GetComponent<M6L85_globo>().mover = false;
            }
            controlNavegacion.Forward(2.0f);
        }
        
    }


    public void Review() {
        print("review");
        foreach (var b in listBallon)
        {
            //b.GetComponent<RectTransform>().anchoredPosition = b.GetComponent<M6L85_globo>().positionReview;
            b.GetComponent<M6L85_globo>().mover = false;
        }
    }


    IEnumerator Refresh() {

        yield return new WaitForSeconds(1);
        if (oportunities < 2)
        {
            ballon_1 = null;
            ballon_2 = null;
            foreach (var b in listBallon)
            {
                b.interactable = true;
                b.GetComponent<Animator>().SetBool("op", false);
                b.GetComponent<Image>().sprite = b.GetComponent<BehaviourSprite>()._default;
            }
            numerator.SetActive(false);
            denomitator.SetActive(false);
            num.text = "";
            dem.text = "";
        }
        
    }

    public void ResetAll()
    {
        if (first) {
            controlPuntaje.resetScore();
            foreach (var b in listBallon)
            {
                b.interactable = true;
                b.GetComponent<Animator>().SetBool("op", false);
                b.GetComponent<Image>().sprite = b.GetComponent<BehaviourSprite>()._default;
                b.GetComponent<M6L85_globo>().mover = true;
            }
            StateInitial();
        }
        controlPuntaje.resetScore();
        _puntaje.count = 0;

    }
}

