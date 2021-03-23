using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class L10A190_player : MonoBehaviour
{
    public float speed = 4f;

    public Animator anim;

    Rigidbody2D rig;
    Vector2 mov;

    public bool canMove;

    public L10A190_manager _Manager;

    public Vector2 initialPosition;

    public GameObject joystick;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        if (Application.isMobilePlatform)
        {
            joystick.SetActive(true);
        }
        else
        {
            joystick.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform)
        {
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
        else
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<L10A190_clue>())
        {
            collision.gameObject.SetActive(false);
            _Manager.GetClue(collision.gameObject);
            GetComponent<AudioSource>().Play();
        }

    }
}
