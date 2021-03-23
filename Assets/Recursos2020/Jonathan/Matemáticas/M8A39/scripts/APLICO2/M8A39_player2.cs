using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A39_player2 : MonoBehaviour
{

    public float speed = 4f;

    public Animator anim;

    Rigidbody2D rig;
    Vector2 mov;

    public bool canMove;

    public M8A39_manger3 _Mananager;

    public Vector2 initialPosition;

    public GameObject joystick;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        if (Application.isMobilePlatform) {
            joystick.SetActive(true);
        }
        else {
            joystick.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform) {
            if (canMove)
            {
                mov = new Vector2(
                    SimpleInput.GetAxisRaw("Horizontal"),
                    SimpleInput.GetAxisRaw("Vertical")
                );

                if (mov != Vector2.zero)
                {
                    anim.SetFloat("moveX", mov.x);
                    anim.SetFloat("moveY", mov.y);
                    anim.SetBool("walking", true);
                }
                else
                {
                    anim.SetBool("walking", false);
                }
            }
        }
        else {
            if (canMove)
            {
                mov = new Vector2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical")
                );

                if (mov != Vector2.zero)
                {
                    anim.SetFloat("moveX", mov.x);
                    anim.SetFloat("moveY", mov.y);
                    anim.SetBool("walking", true);
                }
                else
                {
                    anim.SetBool("walking", false);
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (canMove)
            rig.MovePosition(rig.position + mov * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "barrera")
        {
            collision.gameObject.SetActive(false);
            _Mananager.GetInputGroup(collision.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
