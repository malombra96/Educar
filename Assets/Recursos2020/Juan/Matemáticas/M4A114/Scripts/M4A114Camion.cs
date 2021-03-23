using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;

public class M4A114Camion : MonoBehaviour
{
    M4A114ControlSeleccionYTexto ControlSeleccionYTexto;
    Rigidbody2D rig2D;
    public GameObject controlMovil;
    [HideInInspector] public Vector2 defaulPos;
    public float speed, maximoX, minimoX, maximoY, minimoY;
    public bool mover, horizontal;
    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        ControlSeleccionYTexto = FindObjectOfType<M4A114ControlSeleccionYTexto>();
        defaulPos = GetComponent<RectTransform>().anchoredPosition;
        controlMovil.SetActive(Application.isMobilePlatform);        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mover)
        {
            float m = 0;
            if (!Application.isMobilePlatform)
            {                
                if (horizontal)
                {
                    m = Input.GetAxis("Horizontal");
                    rig2D.velocity = new Vector2(m * speed, 0);
                }
                else
                {
                    m = Input.GetAxis("Vertical");
                    rig2D.velocity = new Vector2(0, m * speed);
                } 
               
            }
            else
            {
                if (horizontal)
                {
                    m = SimpleInput.GetAxis("Horizontal");
                    rig2D.velocity = new Vector2(m * speed, 0);
                }
                else
                {
                    m = SimpleInput.GetAxis("Vertical");
                    rig2D.velocity = new Vector2(0, m * speed);
                }
            }
        }
        else
            rig2D.velocity = Vector2.zero;
        GetComponent<RectTransform>().anchoredPosition = 
            new Vector2(Mathf.Clamp(GetComponent<RectTransform>().anchoredPosition.x, minimoX, maximoX),
            Mathf.Clamp(GetComponent<RectTransform>().anchoredPosition.y, minimoY, maximoY));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        mover = false;
        StartCoroutine(ControlSeleccionYTexto.activarActividad());
    }
}
