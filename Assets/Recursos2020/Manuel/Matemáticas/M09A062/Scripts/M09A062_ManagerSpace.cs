using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A062_ManagerSpace : MonoBehaviour
{
    [HideInInspector] public ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlPuntaje _controlPuntaje;
    [HideInInspector] public ControlAudio _controlAudio;

    [Header("Prefab Enemy")] public List<GameObject> _enemy;   
    [Header("Levels List")] public List<M09A062_BehaviourLevel> _levels; 

    BehaviourLayout currentLayout;

    [HideInInspector] public GameObject _spaceShip;

    [Header("Status Ship & Game")]

    public Image _barLife;

    [HideInInspector] public int _nlifes;


    // Start is called before the first frame update
    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();

        _spaceShip = transform.GetChild(0).GetChild(0).gameObject;

        _nlifes = 5;

        SetScore();
    }

    void Update()
    {
        if(_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
        {   
            transform.GetChild(0).gameObject.SetActive(((currentLayout._Layout == BehaviourLayout.TipoLayout.Actividad) && currentLayout.GetComponent<M09A062_BehaviourLevel>()));
            transform.GetChild(1).gameObject.SetActive(Application.isMobilePlatform && currentLayout.GetComponent<M09A062_BehaviourLevel>());
            
            if(currentLayout._isEvaluated)
                _spaceShip.GetComponent<M09A062_BehaviourSpaceShip>().enabled = false;
            
        }
    }

    public void SetScore()
    {
        float percent = ((float)_nlifes/5f);
        _barLife.fillAmount = percent;
    } 

    public IEnumerator EndGame()
    {
        StopAllCoroutines();
        _spaceShip.GetComponent<M09A062_BehaviourSpaceShip>().enabled = false;
        yield return new WaitForSeconds(2);
        _controlNavegacion.GoToLayout(8);

        print("EndGame");
    }

    public void ResetGeneral()
    {
        _spaceShip.GetComponent<M09A062_BehaviourSpaceShip>().enabled = true;

        foreach (var level in _levels)
            level.ResetLevel();

        _nlifes = 5;

        SetScore();

    }
}
