using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10A38BehaviourDropGroup : MonoBehaviour
{
    M10A38ManagerDragDrop _managerDragDrop;
    [HideInInspector] public int x=0;
    public M10A38BehaviourDrop[] drop;
    public bool correcto;
    public bool[] DragDrop;
    //public Button validar;
    public void Start()
    {
        _managerDragDrop = FindObjectOfType<M10A38ManagerDragDrop>();
        _managerDragDrop._groups.Add(this);

    }
   
   
    public void validar()
    {
        bool pos1 =false;
        bool pos2 =false;
        if (DragDrop[0])
        {
            if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "4")
            {

                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "5")
                {

                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "110500")
                    {
                        correcto = true;
                    }
                }
            }
            else if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "6")
            {

                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "3")
                {

                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "116700")
                    {
                        correcto = true;
                    }
                }
            }
        }
        if (DragDrop[1])
        {
            if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "5")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "4")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "17800")
                    {
                        correcto = true;
                    }
                }
            }
            else if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "13")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "9")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "43200")
                    {
                        correcto = true;
                    }
                }
            }
        }
        if (DragDrop[2])
        {
            if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "44")
                    {
                        correcto = true;
                    }
                }
            }
            else if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "50")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "200")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "5800")
                    {
                        correcto = true;
                    }
                }
            }
        }
        if (DragDrop[3])
        {
            if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "3")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "2")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "4")
                    {
                        if (drop[3]._drag.GetComponent<M10A38BehaviourDrag>().soy == "43")
                        {
                            correcto = true;
                        }
                    }
                }
                else if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "5")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "2")
                    {
                        if (drop[3]._drag.GetComponent<M10A38BehaviourDrag>().soy == "53")
                        {
                            correcto = true;
                        }
                    }
                }
            }
            else if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "4")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "2")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
                    {
                        if (drop[3]._drag.GetComponent<M10A38BehaviourDrag>().soy == "36")
                        {
                            correcto = true;
                        }
                    }

                }
            }
        }
        if (DragDrop[4])
        {
            if (drop[0]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
            {
                if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
                    {
                        if (drop[3]._drag.GetComponent<M10A38BehaviourDrag>().soy == "20")
                        {
                            correcto = true;
                        }
                    }
                    else if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "-3")
                    {
                        if (drop[3]._drag.GetComponent<M10A38BehaviourDrag>().soy == "0")
                        {
                            correcto = true;
                        }
                    }

                }
                else if (drop[1]._drag.GetComponent<M10A38BehaviourDrag>().soy == "-1")
                {
                    if (drop[2]._drag.GetComponent<M10A38BehaviourDrag>().soy == "0")
                    {
                        if (drop[3]._drag.GetComponent<M10A38BehaviourDrag>().soy == "1")
                        {
                            correcto = true;
                        }
                    }
                }
            }
        }
        for (int x = 0; x < drop.Length; x++)
            calificar(x, correcto);
        //    drop[x]._drag.GetComponent<Image>().sprite = correcto ?
        //    drop[x]._drag.GetComponent<BehaviourSprite>()._right
        //    : drop[x]._drag.GetComponent<BehaviourSprite>()._wrong;

    }

    void calificar(int x, bool respuesta)
    {
        drop[x]._drag.GetComponent<Image>().sprite = respuesta ?
                drop[x]._drag.GetComponent<BehaviourSprite>()._right
                : drop[x]._drag.GetComponent<BehaviourSprite>()._wrong;   
    }

}


