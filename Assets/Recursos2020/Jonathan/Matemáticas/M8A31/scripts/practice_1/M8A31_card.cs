using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_card : MonoBehaviour
{

    public GameObject subCard,coupleCard;
    Button myButton;
    M8A31_manager _Manager;
    ControlAudio audio;

    public List<Sprite> _sprite;

    public bool a;

    // Start is called before the first frame update
    void Start()
    {
        a = false;
        audio = GameObject.FindObjectOfType<ControlAudio>();
        _Manager = GameObject.FindObjectOfType<M8A31_manager>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(delegate { ShowSubCard(myButton); });
        _Manager.allCubes.Add(gameObject);
        

    }

    public void StartGame()
    {
        GetComponent<Image>().sprite = _sprite[1];
        GetComponent<Button>().interactable = false;
        subCard.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(hideCard());
    }

    IEnumerator hideCard() {
        yield return new WaitForSeconds(2.5f);
        GetComponent<Image>().sprite = _sprite[0];
        GetComponent<Button>().interactable = true;
        a = true;

    }



    public void ShowSubCard(Button button) {
        if (a) {
            audio.PlayAudio(0);
            GetComponent<Animator>().Play("cube_Rotate");
            _Manager.CompararCubos(gameObject);
            StartCoroutine(x());
        }
        
        
    }

    IEnumerator x() {
        yield return new WaitForSeconds(0.5f);
        subCard.GetComponent<Animator>().Play("subCube_Rotate"); 
    }
}
