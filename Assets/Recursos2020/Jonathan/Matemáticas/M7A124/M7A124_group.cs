using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M7A124_group : MonoBehaviour
{
    public M7A124_managerDrag _M7A124_managerDrag;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;

    public List<string> respuestas;
    public List<M7A124_drop> drops;
    public int c;
    public bool op,grupo;

    public void Start()
    {
        _M7A124_managerDrag = FindObjectOfType<M7A124_managerDrag>();
        _M7A124_managerDrag._groups.Add(this);

    }

    public void EvaluateGroup()
    {
        if (grupo) {
            List<string> temporary = new List<string>(respuestas);

            for (int i = 0; i < drops.Count; i++)
            {
                for (int j = 0; j < temporary.Count; j++)
                {
                    if (temporary.Contains(drops[i]._drag.name))
                    {
                        if (drops[i]._drag.name == temporary[j])
                        {
                            c++;
                            drops[i].GetComponent<Image>().sprite = drops[i].GetComponent<BehaviourSprite>()._right;
                            _M7A124_managerDrag._controlPuntaje.IncreaseScore();
                            temporary.RemoveAt(j);
                            break;
                        }
                    }
                    else
                    {
                        drops[i].GetComponent<Image>().sprite = drops[i].GetComponent<BehaviourSprite>()._wrong;
                        break;
                    }

                }
            }

            if (c == drops.Count)
            {
                op = true;
            }

        }

    }

}
