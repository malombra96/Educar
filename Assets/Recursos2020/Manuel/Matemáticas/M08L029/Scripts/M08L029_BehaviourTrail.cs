using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L029_BehaviourTrail : MonoBehaviour
{
    [HideInInspector] public ControlNavegacion _controlNavegacion;

    BehaviourLayout currentLayout;

    [Header("InputField")] public List<BehaviourLayout> _inputLayout;
    [Header("Background Trail")] public RectTransform bgTrail;
    [Header("Tunel - Level")] public RectTransform tunnel;
    [Header("Step Movement")] public int _step;
    [Header("Enabled Movement")] public bool _mov;

    [Header("Energy")] public Image _energy;
    [Header("Time")] public Text _time;
    [Header("Score")] public Text _score;
    double temp;


    void Awake()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _mov = false;
        temp = -1;
        SetInput(false);
    }

    void Update()
    {
        if (_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(_inputLayout.Contains(currentLayout));


            if (currentLayout._isEvaluated || !_inputLayout.Contains(currentLayout))
            {
                _mov = false;

                if (currentLayout._isEvaluated)
                {
                    bgTrail.anchoredPosition = new Vector2(0, -22000);
                    tunnel.anchoredPosition = new Vector2(0, -10128);
                }
            }

            if(_inputLayout.Contains(currentLayout) && currentLayout._isEvaluated)
                SetInput(true);

        }   
            
    }

    void FixedUpdate()
    {
        if (_mov)
        {
            bgTrail.anchoredPosition = new Vector2(0, bgTrail.anchoredPosition.y - _step);
            tunnel.anchoredPosition = new Vector2(0,tunnel.anchoredPosition.y - _step);

            string s = System.TimeSpan.FromSeconds((int)Time.timeSinceLevelLoad).ToString();
            _time.text = s;

            if(temp==-1)
                temp = FindObjectOfType<ControlPuntaje>()._rightAnswers;
                

            _score.text = (FindObjectOfType<ControlPuntaje>()._rightAnswers - temp).ToString();
        }
    }
    public void SetInput(int index, bool state)
    {
        _mov = false;
        GameObject input =  _inputLayout[index].gameObject;

        for (int i = 0; i < input.transform.childCount; i++)
            input.transform.GetChild(i).gameObject.SetActive(state);

        if(!currentLayout._isEvaluated)
            _inputLayout[index].GetComponent<ManagerInputField>().InitializeState();
    }

    public void SetInput(bool state)
    {
        foreach (BehaviourLayout input in _inputLayout)
            for (int i = 0; i < input.transform.childCount; i++)
                input.transform.GetChild(i).gameObject.SetActive(state);
    }

    public void SetMovement(bool state) => StartCoroutine(DelayXmovement(state));

    IEnumerator DelayXmovement(bool state)
    {
        yield return new WaitForSeconds(2);
        _mov = state;
    } 

    public void EndGame()
    {
        _mov = false;
        _controlNavegacion.GoToLayout(13,2);
    }

    public void ResetManager()
    {
        _mov = false;
        SetInput(false);
        temp = -1;

        _energy.fillAmount = 1;
        _time.text = "00:00:00";

        bgTrail.anchoredPosition = Vector2.zero;
        tunnel.anchoredPosition = new Vector2(0,11874);

        
    }
}
