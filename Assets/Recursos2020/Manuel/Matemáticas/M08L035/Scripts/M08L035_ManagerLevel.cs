using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M08L035_ManagerLevel : MonoBehaviour
{
    M08L035_GeneralSpaceManager _managerSpace;

    [Header("Nivel")] [Range(1,5)] public int _level;
     
    [Header("Sprites Options")] public List<Sprite> _options;
    [Header("Sprite Correct")] public Sprite _correct;
    [HideInInspector] public List<GameObject> _objects;

    // Start is called before the first frame update
    void Start()
    {
        _managerSpace = FindObjectOfType<M08L035_GeneralSpaceManager>();

        if(!GetComponent<BehaviourLayout>()._isEvaluated)    
            InstanciarObjetives();
        
        
    }

    void InstanciarObjetives()
    {   
        for (int i = 0; i < _options.Count; i++)
        {
            GameObject aux = Instantiate(_managerSpace._enemy[i], transform);
            aux.name = aux.name;
            aux.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-260, 600), Random.Range(600, 2000));
            aux.GetComponent<M08L035_BehaviourEnemy>()._step = _level;


            Transform answer = aux.transform.GetChild(0);
            answer.GetComponent<Image>().sprite = _options[i];
            answer.GetComponent<Image>().SetNativeSize();

            _objects.Add(aux);
        }

        SetStateObjectives();
    }

    void SetStateObjectives()
    {
        foreach (var objective in _objects)
            objective.GetComponent<M08L035_BehaviourEnemy>()._state = (objective.transform.GetChild(0).GetComponent<Image>().sprite == _correct);
                
    }


    public void StopObjectives()
    {
        foreach (var objective in _objects)
        {
            objective.GetComponent<BoxCollider2D>().enabled = false;
            objective.GetComponent<CircleCollider2D>().enabled = false;
            objective.GetComponent<M08L035_BehaviourEnemy>()._mov = false;
            objective.GetComponent<M08L035_BehaviourEnemy>().enabled = false;  
            
        }
    }

    void OnEnable()
    {
        if(GetComponent<BehaviourLayout>()._isEvaluated)
        {
            foreach (var obj in _objects)
                obj.transform.SetParent(transform.GetChild(1));

                StopObjectives();
        }
            
        
    }

    public void ResetLevel()
    {
        if (_managerSpace)
        {
            foreach (var obj in _objects)
                Destroy(obj);

            _objects.Clear();

            InstanciarObjetives();
        }

        
    }
}
