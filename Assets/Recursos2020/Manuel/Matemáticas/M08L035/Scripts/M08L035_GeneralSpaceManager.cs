using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L035_GeneralSpaceManager : MonoBehaviour
{
    [HideInInspector] public ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlPuntaje _controlPuntaje;
    [HideInInspector] public ControlAudio _controlAudio;

    [Header("Prefab Objetive")] public List<GameObject> _enemy;   
    [Header("Levels List")] public List<M08L035_ManagerLevel> _levels; 

    BehaviourLayout currentLayout;

    [HideInInspector] public GameObject _spaceShip;

    [Header("Status Ship & Game")]
    public Text _lifes;
    public Text _levelText;
    public Transform _destroyedEnemies;

    [HideInInspector] public int _nlifes;
    [HideInInspector] public int _nEnemy;
    


    // Start is called before the first frame update
    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();

        _spaceShip = transform.GetChild(0).GetChild(0).gameObject;

        _nlifes = 3;
        _nEnemy = -1;
    }

    void Update()
    {
        if(_controlNavegacion.GetLayoutActual())
            currentLayout = _controlNavegacion.GetLayoutActual().GetComponent<BehaviourLayout>(); // Get active element

        if (currentLayout != null)
        {   
            transform.GetChild(0).gameObject.SetActive(((currentLayout._Layout == BehaviourLayout.TipoLayout.PopUp) && currentLayout.GetComponent<M08L035_ManagerLevel>()));
            transform.GetChild(1).gameObject.SetActive(Application.isMobilePlatform && currentLayout.GetComponent<M08L035_ManagerLevel>());
            
            if(currentLayout.GetComponent<M08L035_ManagerLevel>())
            {
                _levelText.text = currentLayout.GetComponent<M08L035_ManagerLevel>()._level.ToString();
                _lifes.text = _nlifes.ToString();

            }

            if(currentLayout._isEvaluated)
            {   
                _spaceShip.GetComponent<RectTransform>().anchoredPosition = new Vector2(200,-200);
                _spaceShip.GetComponent<M08L035_BehaviourSpaceShip>().enabled = false;
            }
            
        }
        
        EnemyDestroy();
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0);
        _spaceShip.GetComponent<M08L035_BehaviourSpaceShip>().enabled = false;
        yield return new WaitForSeconds(2);
        _controlNavegacion.GoToLayout(9);
    }

    public void EnemyDestroy()
    {
        for (int i = 0; i < _destroyedEnemies.childCount; i++)
            _destroyedEnemies.GetChild(i).gameObject.SetActive(i<=_nEnemy);
    }

    public void ResetGeneral()
    {
        _spaceShip.GetComponent<M08L035_BehaviourSpaceShip>().enabled = true;

        foreach (var level in _levels)
            level.ResetLevel();

        _nlifes = 3;
        _nEnemy = -1;

        EnemyDestroy();

    }
}
