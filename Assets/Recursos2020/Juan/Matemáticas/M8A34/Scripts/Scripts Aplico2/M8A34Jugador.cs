using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A34Jugador : MonoBehaviour
{
    public float speed, maximo,minimo;
    float mover;
    [HideInInspector] public AudioSource audioSource;    

    public M8A34Manager pregunta;
    
    Animator animator;
    Rigidbody2D rig;
    bool puedoMoverme;

    [HideInInspector]public bool m = true;

    float mover2, velocidad, fuezaDeEmpuje = 1;

    // Start is called before the first frame update
    void Start()
    {
        pregunta = FindObjectOfType<M8A34Manager>();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("cambiodefuerza", 2, 3);
        audioSource = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {    
        
        if (puedoMoverme && pregunta.activar)
        {
            if (Application.isMobilePlatform)
            {
                velocidad = SimpleInput.GetAxis("Vertical");
                mover = SimpleInput.GetAxis("Horizontal");
                
                if (velocidad != 0 && m)
                {
                    print(mover);
                    audioSource.Play();
                    m = false;
                }
                else if (velocidad == 0 && !m)
                {
                    audioSource.Stop();
                    audioSource.time = 0;
                    m = true;
                }
            }
            else
            {
                mover = Input.GetAxis("Horizontal");
                velocidad = Input.GetAxis("Vertical");

                if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
                {
                    
                    audioSource.Play();
                    m = false;                   
                }
                else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                {
                    
                    audioSource.Stop();
                    audioSource.time = 0;
                    m = true;
                }
            }            

            if (velocidad > 0.5)
            {                
                if (mover != 0)
                {
                    mover2 = mover;                    
                }
                else
                {
                    StartCoroutine(cambioDeEmpuje());
                    mover2 = 0.5f * fuezaDeEmpuje;
                }
            }

            else
            {
                mover2 = 0;                
            }
           
        }

        else
        {
            mover = 0;
            velocidad = 0;
            audioSource.Stop();
            audioSource.time = 0;
        }

        if (mover > 0.2)
        {
           animator.SetBool("izquierda", false);
           animator.SetBool("derecha", true);
        }

        else if (mover < -0.2)
        {
           animator.SetBool("derecha", false);
           animator.SetBool("izquierda", true);
        }

        else
        {
           animator.SetBool("izquierda", false);
           animator.SetBool("derecha", false);
        }       

        rig.velocity = new Vector2(speed * mover2, rig.velocity.y);
        GetComponent<Transform>().position = new Vector2(Mathf.Clamp(GetComponent<Transform>().position.x, minimo, maximo),
            GetComponent<Transform>().position.y);

        GetComponent<Transform>().Rotate(GetComponent<Transform>().rotation.x, GetComponent<Transform>().rotation.y,
            Mathf.Clamp(GetComponent<Transform>().rotation.z, minimo, maximo));
            
    }
    IEnumerator cambioDeEmpuje()
    {
        yield return new WaitForSeconds(1);
        fuezaDeEmpuje *= -1;
    }    

    public void cambio()
    {
        puedoMoverme = true;     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        puedoMoverme = false;
        pregunta.ActivarPregunta();
        mover2 = 0;        
    }   

}
