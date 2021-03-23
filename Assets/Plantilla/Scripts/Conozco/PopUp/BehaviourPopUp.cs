using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BehaviourPopUp : MonoBehaviour
{
    GameObject video;
    GameObject sound;
    GameObject image;
    
    public enum TypePopUp
    {
        none,
        image,
        sound,
        video
    }
    

    [Header("Tipo de pop-up")] public TypePopUp _type;

    private void OnEnable()
    {
        GameObject temp = transform.GetChild(0).gameObject;

        if (temp != null)
        {
            switch (_type)
            {
                case TypePopUp.image:
                    image = temp;
                    break;
                case TypePopUp.sound:
                    sound = temp;
                    break;
                case TypePopUp.video:
                    video = temp;
                    break;
            }
          
        }
            
    }

    void Update()
    {
        switch (_type)
        {
            case TypePopUp.none:
                
                ClearTemp();
                
                break;
            
            case TypePopUp.image:
                
                if (image == null)
                    image = Instantiate((Resources.Load("Prefabs/Imagen") as GameObject), transform);
                
                DestroyImmediate(sound);
                DestroyImmediate(video);
                sound = null;
                video = null;
                
                break;
            case TypePopUp.sound:
                
                if (sound == null)
                    sound = Instantiate((Resources.Load("Prefabs/ManagerAudio") as GameObject), transform);
                
                DestroyImmediate(image);
                DestroyImmediate(video);
                image = null;
                video = null;
                
                break;
            case TypePopUp.video:

                if(video == null )
                    video = Instantiate((Resources.Load("Prefabs/Video") as GameObject),transform);
                
                DestroyImmediate(image);
                DestroyImmediate(sound);
                image = null;
                sound = null;
                
                break;
            
        }
    }

    void ClearTemp()
    {
        DestroyImmediate(image);
        DestroyImmediate(sound);
        DestroyImmediate(video);
        image = null;
        sound = null;
        video = null;
    }
}
