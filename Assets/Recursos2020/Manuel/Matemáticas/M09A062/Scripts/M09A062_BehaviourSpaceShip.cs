using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09A062_BehaviourSpaceShip : MonoBehaviour
{
    [Header("SpaceShip Parametres")] 
    public int velocidad;
    
    [Header("Shoot")]
    public GameObject prefabShoot;
    public float intervalo;
    float tempIntervalo;

    [Header("Mobile Controls")] 
    public string shooting;

    void Update()
    {
        Limites(GetComponent<RectTransform>().anchoredPosition);
        Shooting();   
    }

    void Limites(Vector2 anchoredPosition)
    {
        if (anchoredPosition.x < -625)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-625,anchoredPosition.y);              
        }
            
        if (anchoredPosition.x > 330)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(330,anchoredPosition.y);
        }
        
        Movimiento();

    }

    void Movimiento()
    { 
        Vector2 translacion;
        Vector2 translacion2;
        
        translacion = velocidad * Time.smoothDeltaTime * new Vector2(Input.GetAxis("Horizontal"),0);
        translacion2 = velocidad * Time.smoothDeltaTime * new Vector2(SimpleInput.GetAxis("Horizontal"),0);

        if (Application.isMobilePlatform)
            transform.Translate(translacion2.x, translacion2.y, 0);
        else
            transform.Translate(translacion.x, translacion.y, 0);
        
    }

    
    void Shooting()
    {
        if (tempIntervalo > 0)
        {
            tempIntervalo -= Time.deltaTime;
        }
        else
        {
            if (Application.isMobilePlatform)
            {
                 if( SimpleInput.GetButtonDown(shooting))
                {
                    Vector3 p = transform.position;
                    GameObject temp =  Instantiate(prefabShoot,p,Quaternion.identity,transform.parent);
                    temp.transform.SetSiblingIndex(0);
                    tempIntervalo = intervalo;
                } 
            
            }
            else
            { 
                if (Input.GetKey(KeyCode.Space))
                {
                    Vector3 p = transform.position;
                    GameObject temp =  Instantiate(prefabShoot,p,Quaternion.identity,transform.parent);
                    temp.transform.SetSiblingIndex(0);
                    tempIntervalo = intervalo;
                }
                
            }

        }
    }
}
