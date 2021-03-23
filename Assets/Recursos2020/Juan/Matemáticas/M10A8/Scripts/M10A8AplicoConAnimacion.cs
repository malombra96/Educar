using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.Video;

public class M10A8AplicoConAnimacion : MonoBehaviour
{
    public bool activarVideo;
    public string dirreccion;
   
    private void OnEnable()
    {
        if (gameObject.activeSelf && activarVideo)
        {
            GetComponent<M10A8VideoControler>().PlayWebGL(dirreccion);            
            activarVideo = false;
        }
        //else if(!activarVideo)
        //    transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ResetAll()
    {
        GetComponent<M10A8VideoControler>().Restart();
        activarVideo = true;
        GetComponent<VideoPlayer>().playOnAwake = true;
        if (transform.GetChild(2).GetComponent<M10A8ManagerDragDrop>())
        {
            transform.GetChild(2).GetComponent<M10A8ManagerDragDrop>().padre = gameObject;
            transform.GetChild(2).GetComponent<M10A8ManagerDragDrop>().ResetDragDrop();
        }
        else if (transform.GetChild(2).GetComponent<M10A8ManagerSeleccionarToggle>())
        {
            transform.GetChild(2).GetComponent<M10A8ManagerSeleccionarToggle>().padre = gameObject;
            transform.GetChild(2).GetComponent<M10A8ManagerSeleccionarToggle>().ResetSeleccionarToggle();
        }
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void revision()
    {
        Invoke("apagar", 1f);
    }
    void apagar()
    {
        activarVideo = false;
        print(transform.GetChild(1).gameObject);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

}