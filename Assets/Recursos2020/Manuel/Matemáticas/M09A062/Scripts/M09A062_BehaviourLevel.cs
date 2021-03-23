using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A062_BehaviourLevel : MonoBehaviour
{
    M09A062_ManagerSpace _managerSpace;
    /* [HideInInspector] */ public int _answers;
    [Header("Sprite Correct")] public List<Sprite> _correct;
    /* [HideInInspector] */ public List<GameObject> _objects;

    // Start is called before the first frame update
    void Start()
    {
        _managerSpace = FindObjectOfType<M09A062_ManagerSpace>();

        if(!GetComponent<BehaviourLayout>()._isEvaluated)    
            InstanciarObjetives();
        
    }

    void InstanciarObjetives()
    {   
        for (int i = 0; i < _managerSpace._enemy.Count; i++)
        {
            GameObject aux = Instantiate(_managerSpace._enemy[i], transform);
            aux.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 300), Random.Range(600, 2000));
            _objects.Add(aux);
        }

        SetStateObjectives();
    }

    void SetStateObjectives()
    {
        foreach (var objective in _objects)
                objective.GetComponent<M09A062_BehaviourEnemy>()._state = _correct.Contains(objective.GetComponent<Image>().sprite);
                
    }


    public void StopObjectives()
    {
        foreach (var objective in _objects)
        {
            objective.GetComponent<BoxCollider2D>().enabled = false;
            objective.GetComponent<CircleCollider2D>().enabled = false;
            objective.GetComponent<M09A062_BehaviourEnemy>()._mov = false;
            objective.GetComponent<M09A062_BehaviourEnemy>().enabled = false;  
            
        }
    }

    void OnEnable()
    {
        if(GetComponent<BehaviourLayout>()._isEvaluated)
        {
            foreach (var obj in _objects)
            {
                obj.transform.SetParent(transform.GetChild(1));
                obj.SetActive(true);
            }
                

            StopObjectives();
        }
            
        
    }

    public void ResetLevel()
    {
        _answers = 0;

        if (_managerSpace)
        {
            foreach (var obj in _objects)
                Destroy(obj);

            _objects.Clear();

            InstanciarObjetives();
        }

        
    }
}
