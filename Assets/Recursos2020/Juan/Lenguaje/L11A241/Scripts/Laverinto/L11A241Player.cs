using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L11A241Player : MonoBehaviour
{
    public float speed;
    public bool moverme;
    public float maximoX, minimoX;
    public float maximoY, minimoY;
    Rigidbody2D rig;
    [HideInInspector] public Vector2 posDefault;
    L11A241Laverinto laverinto;
    // Start is called before the first frame update
    void Start()
    {
        posDefault = GetComponent<RectTransform>().anchoredPosition;
        moverme = true;
        rig = FindObjectOfType<Rigidbody2D>();
        laverinto = FindObjectOfType<L11A241Laverinto>();
    }
    private void OnEnable()
    {
        moverme = true;
    }    
    void FixedUpdate()
    {
        if (moverme)
        {
            if (!Application.isMobilePlatform)
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");

                rig.velocity = new Vector2(x * speed, y * speed);
            }
            else
            {
                float x = SimpleInput.GetAxis("Horizontal");
                float y = SimpleInput.GetAxis("Vertical");
                
                rig.velocity = new Vector2(x * speed, y * speed);
            }
        }
        Mathf.Clamp(GetComponent<RectTransform>().anchoredPosition.x, minimoX, maximoX);
        Mathf.Clamp(GetComponent<RectTransform>().anchoredPosition.y, minimoY, maximoY);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        moverme = false;
        StartCoroutine(laverinto.cambioDeActividad());
        GetComponent<RectTransform>().anchoredPosition = collision.GetComponent<RectTransform>().anchoredPosition;
        rig.velocity = Vector2.zero;
        collision.gameObject.SetActive(false);
    }
}
