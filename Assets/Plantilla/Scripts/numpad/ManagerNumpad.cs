using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerNumpad : MonoBehaviour
{
    
    public List<Button> _ToSymbols,_ToNumbers,_ToLetters;
    [Header("Button Close")] public Button _Close;

    GameObject _KeyBoardSymbols, _KeyBoardNumbers,_KeyBoardLetters;

    ControlAudio _controlAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();

        _KeyBoardNumbers = transform.GetChild(0).gameObject;
        _KeyBoardSymbols = transform.GetChild(1).gameObject;
        _KeyBoardLetters = transform.GetChild(2).gameObject;

        foreach (Button b in _ToLetters)
            b.onClick.AddListener(delegate{ChangeKeyboard(3);});

        foreach (Button b in _ToSymbols)
            b.onClick.AddListener(delegate{ChangeKeyboard(2);});

        foreach (Button b in _ToNumbers)
            b.onClick.AddListener(delegate{ChangeKeyboard(1);});


        _Close.onClick.AddListener(CerrarLightBox);
    } 
    
    public void ChangeKeyboard(int teclado)
    {
        _controlAudio.PlayAudio(0);

         _KeyBoardNumbers.SetActive(teclado == 1);
         _KeyBoardSymbols.SetActive(teclado == 2);
         _KeyBoardLetters.SetActive(teclado == 3);
    }

    public void CerrarLightBox()
    {
        _controlAudio.PlayAudio(0);
        transform.parent.parent.gameObject.GetComponent<Animator>().Play("NumPad_out");
        StartCoroutine(DelayCerrar());
    }

    IEnumerator DelayCerrar()
    {
        yield return  new WaitForSeconds(2);
        transform.parent.parent.gameObject.SetActive(false);
    }
}
