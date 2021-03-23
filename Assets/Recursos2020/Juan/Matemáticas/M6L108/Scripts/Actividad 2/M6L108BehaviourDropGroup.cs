using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L108BehaviourDropGroup : MonoBehaviour
{
    M6L108ManagerDragDrop _managerDragDrop;
    [Header("Right Drop Needed")] public int _needRight;
    [HideInInspector] [Header("Current Right Drop")] public int _currentRight;
    [HideInInspector] [Header("Current Wrong Drop")] public int _currentWrong;
    public List<Sprite> spritesDrags;
    public bool Confirmo;
    public bool porPares;

    public void Start()
    {
        _managerDragDrop = FindObjectOfType<M6L108ManagerDragDrop>();
        _managerDragDrop._groups.Add(this);
    }
    public void verificar(GameObject obj)
    {
        Confirmo = false;
        int i = 0;
        GameObject temp = null;
        GameObject temp2 = null;
        if (!porPares)
        {
            for (int x = 0; x < transform.childCount; x++)
            {
                var drop = transform.GetChild(x).GetComponent<M6L108BehaviourDrop>();
                if (drop._drag && drop._drag == obj)
                {
                    temp = drop.gameObject;
                    if (spritesDrags.Count > 0)
                    {
                        foreach (var spriteDrag in spritesDrags)
                        {
                            if (drop._drag.GetComponent<Image>().sprite == spriteDrag)
                                i++;
                        }
                    }
                    else
                        break;
                }
            }
        }
        else
        {
            for (int x = 0; x < transform.childCount; x++)
            {
                var drop = transform.GetChild(x).GetComponent<M6L108BehaviourDrop>();
                if (drop._drag && drop._drag == obj)
                {
                    temp = drop.gameObject;
                    if (drop.transform.GetSiblingIndex() % 2 == 0)
                    {
                        if (transform.GetChild(x + 1).GetComponent<M6L108BehaviourDrop>()._drag)
                        {
                            temp2 = transform.GetChild(x + 1).gameObject;
                            if (transform.GetChild(x + 1).GetComponent<M6L108BehaviourDrop>()._drag.GetComponent<Image>().sprite == drop._drag.GetComponent<Image>().sprite)
                            {                                                                
                                i++;
                            }                                
                            else
                                break;
                        }
                    }
                    else
                    {
                        if (transform.GetChild(x - 1).GetComponent<M6L108BehaviourDrop>()._drag)
                        {
                            temp2 = transform.GetChild(x - 1).gameObject;
                            if (transform.GetChild(x - 1).GetComponent<M6L108BehaviourDrop>()._drag.GetComponent<Image>().sprite == drop._drag.GetComponent<Image>().sprite)
                            {                                
                                i++; 
                            }
                            else
                                break;
                        }
                    }
                }
            }
        }
        Confirmo = (i == 0 ? true : false);

        temp.GetComponent<M6L108BehaviourDrop>()._drag.GetComponent<M6L108BehaviourDrag>()._DropRight.Clear();
        if(temp2)
            temp2.GetComponent<M6L108BehaviourDrop>()._drag.GetComponent<M6L108BehaviourDrag>()._DropRight.Clear();

        if (Confirmo)
        {           
            temp.GetComponent<M6L108BehaviourDrop>()._drag.GetComponent<M6L108BehaviourDrag>()._DropRight.Add(temp.GetComponent<M6L108BehaviourDrop>());
            if (temp2)
                temp2.GetComponent<M6L108BehaviourDrop>()._drag.GetComponent<M6L108BehaviourDrag>()._DropRight.Add(temp2.GetComponent<M6L108BehaviourDrop>());
        }        
    }

    public void remover(GameObject i)
    {
        spritesDrags.Clear();
        for (int x = 0; x < transform.childCount; x++)
        {
            var drop = transform.GetChild(x).GetComponent<M6L108BehaviourDrop>();

            if (drop._drag && drop._drag != i)
                spritesDrags.Add(drop._drag.GetComponent<Image>().sprite);
        }
    }
}
