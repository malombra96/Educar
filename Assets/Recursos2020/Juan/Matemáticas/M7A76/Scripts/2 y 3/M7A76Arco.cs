using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M7A76Arco : MonoBehaviour
{
    public Camera camara;
    Vector3 MousePosicion, ObjetoPosicion;
    float angulo;
    M7A76GameControler gameControler;
    private void Start()
    {
        gameControler = FindObjectOfType<M7A76GameControler>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameControler.seguirDisparando)
        {
            MousePosicion = Input.mousePosition;
            ObjetoPosicion = Camera.main.WorldToScreenPoint(transform.position);

            angulo = Mathf.Atan2((MousePosicion.y - ObjetoPosicion.y), (MousePosicion.x - ObjetoPosicion.x)) * Mathf.Rad2Deg;
            GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        }
    }
}
