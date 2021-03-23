using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M9A130_managerToggle : MonoBehaviour
{
    public int contador;
    public bool op;
    public ControlPuntaje ControlPuntaje;
    public ControlNavegacion ControlNavegacion;
    public GameObject punto1, punto2;
    public List<Button> botones;
    public List<M9A130_managerSeleccionar> sel;
    public Color32 sele, ver, roj,azu;
    public void Next() {

        if (punto1.GetComponent<M9A130_punto>().x)
        {
            var newColorBlock = punto1.GetComponent<Button>().colors;
            newColorBlock.disabledColor = ver;
            punto1.GetComponent<Button>().colors = newColorBlock;
        }
        else
        {
            var newColorBlock = punto1.GetComponent<Button>().colors;
            newColorBlock.disabledColor = roj;
            punto1.GetComponent<Button>().colors = newColorBlock;
        }

        if (punto2 != null)
        {
            if (punto2.GetComponent<M9A130_punto>().x)
            {
                var newColorBlock = punto2.GetComponent<Button>().colors;
                newColorBlock.disabledColor = ver;
                punto2.GetComponent<Button>().colors = newColorBlock;
            }
            else
            {
                var newColorBlock = punto2.GetComponent<Button>().colors;
                newColorBlock.disabledColor = roj;
                punto2.GetComponent<Button>().colors = newColorBlock;
            }
        }

        if (contador == 1) {
            if (punto1.GetComponent<M9A130_punto>().correcto /*|| punto2.GetComponent<M9A130_punto>().correcto*/) {
                ControlPuntaje.IncreaseScore();
            }
            
            ControlNavegacion.Forward(1f);
        }
    }

    public void añadir(GameObject g) {

        if (punto1 != null && punto2 != null) {
            for (int i = 0; i < botones.Count; i++)
            {
                botones[i].interactable = false;
            }
        }
        if (punto1 == null) {
            punto1 = g;
//            punto1.GetComponent<Button>().interactable = false;
            punto1.GetComponent<M9A130_punto>().op = true;
            for (int i = 0; i < botones.Count; i++) {
                botones[i].interactable = false;
            }
            var newColorBlock = punto1.GetComponent<Button>().colors;
            newColorBlock.disabledColor = sele;
            punto1.GetComponent<Button>().colors = newColorBlock;
        } else if (punto2 == null) {
            punto2 = g;
            punto2.GetComponent<M9A130_punto>().op = true;
           // punto2.GetComponent<Button>().interactable = false;
            for (int i = 0; i < botones.Count; i++)
            {
                botones[i].interactable = false;
            }
            var newColorBlock = punto2.GetComponent<Button>().colors;
            newColorBlock.disabledColor = sele;
            punto2.GetComponent<Button>().colors = newColorBlock;
        }

    }

    public void resetear() {
        contador = 0;
        punto1 = null;
        punto2 = null;
        for (int i = 0; i < botones.Count; i++)
        {
            botones[i].interactable = true;
            botones[i].GetComponent<M9A130_punto>().op = false;
            var newColorBlock = botones[i].GetComponent<Button>().colors;
            newColorBlock.disabledColor = azu;
            botones[i].GetComponent<Button>().colors = newColorBlock;
        }

        foreach (var x in sel) {
            x.ResetSeleccionarToggle();
            x.gameObject.SetActive(false);
        }
    }
    public void activar() {
        //if (punto2 == null) {
        //    for (int i = 0; i < botones.Count; i++)
        //    {
        //        if (!botones[i].GetComponent<M9A130_punto>().op)
        //        {
        //            botones[i].interactable = true;
                   
        //        }

        //    }
        //}

       
        

    }

}
