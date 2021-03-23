using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M06A087_ManagerTrail : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;

    [HideInInspector] public M06A087_BehaviourCarPlayer _car;

    [HideInInspector] public BehaviourLayout currentLayout;

    [Header("InputField")] public List<BehaviourLayout> _selectLayout;

    [Header("Carteles Questios")] public List<GameObject> _carteles;

    [Header("Background Trail")] public RectTransform bgTrail;
    [Header("Step Movement")] public int _step;
    [Header("Enabled Movement")] public bool _mov;

    [Header("Panels Information")] 
    public GameObject _crashCar;
    public GameObject _addOil;

    [Header("UI Stats")]

    [HideInInspector] public int _score;
    public Text _puntaje;
    public Image _combustible;
    public Image _velocidad;

    public Text _scoreFinal;
    
    [Header("List Elements")]

    public List<GameObject> _cars;
    public List<GameObject> _oils;


    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _mov = false;
        _score = 0;
        _velocidad.fillAmount = 1;
        _combustible.fillAmount = .2f;

        foreach (var car in _cars)
            car.SetActive(true);

        foreach (var oil in _oils)
            oil.SetActive(true);

        SetInput(false);
    }

    void Update()
    {
        if (_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(_selectLayout.Contains(currentLayout));

            for (int j = 0; j < _selectLayout.Count; j++)
                _carteles[j].SetActive(_selectLayout[j].isActiveAndEnabled);

            if(currentLayout._isEvaluated && _selectLayout.Contains(currentLayout))
            {
                int index = _selectLayout.IndexOf(currentLayout);
                
                bgTrail.anchoredPosition = index == 0 ? new Vector2(0,-1320) : 
                                           index == 1 ? new Vector2(0,-2850) :
                                           index == 2 ? new Vector2(0,-3995) :
                                           index == 3 ? new Vector2(0,-5145) :
                                           new Vector2(0,-6290);
                
            }

        }   
            
    }

    void FixedUpdate()
    {
        if (_mov)
            bgTrail.anchoredPosition = new Vector2(0, bgTrail.anchoredPosition.y - _step);

        _scoreFinal.text = _puntaje.text;
    }
    public void SetInput(int index, bool state)
    {
        _mov = false;
        GameObject input =  _selectLayout[index].gameObject;

        for (int i = 0; i < input.transform.childCount; i++)
            input.transform.GetChild(i).gameObject.SetActive(state);
    }

    public void SetInput(bool state)
    {
        foreach (BehaviourLayout input in _selectLayout)
            for (int i = 0; i < input.transform.childCount; i++)
                input.transform.GetChild(i).gameObject.SetActive(state);
    }

    public void SetMovementCar(string dir)
    {
        if(dir == "Right")
            _car.GetComponent<RectTransform>().anchoredPosition = new Vector2(125,-274);
        else if(dir == "Left")
            _car.GetComponent<RectTransform>().anchoredPosition = new Vector2(-115,-274);
        else
            _car.GetComponent<RectTransform>().anchoredPosition = new Vector2(3,-274);
    }

    public void SetMovement(bool state) => StartCoroutine(DelayXmovement(state));

    IEnumerator DelayXmovement(bool state)
    {
        yield return new WaitForSeconds(3);
        _mov = state;
    } 


    public IEnumerator AddOil()
    {
        _combustible.fillAmount += .16f; 
        _addOil.SetActive(true);
        yield return new WaitForSeconds(1);
        _addOil.SetActive(false);
    }

    public IEnumerator CrashCar()
    {
        _velocidad.fillAmount -= .33f;
        _crashCar.SetActive(true);
        yield return new WaitForSeconds(1);
        _crashCar.SetActive(false);

        if(_velocidad.fillAmount < 0.1)
            EndGame();
    }

    public void EndGame()
    {
        _mov = false;
        _controlNavegacion.GoToLayout(13,.5f);
        SetInput(5,true);
    }

    public void ResetManager()
    {
        _mov = false;
        SetInput(false);
        _velocidad.fillAmount = 1;
        _combustible.fillAmount = .2f;
        _score = 0;
        _puntaje.text = _score.ToString();
        _scoreFinal.text = _puntaje.text;

        foreach (var car in _cars)
            car.SetActive(true);

        foreach (var oil in _oils)
            oil.SetActive(true);

        bgTrail.anchoredPosition = Vector2.zero;

        
    }
}
