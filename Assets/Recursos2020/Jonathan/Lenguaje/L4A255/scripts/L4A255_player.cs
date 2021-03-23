using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class L4A255_player : MonoBehaviour
{
    public bool mover;
    public Rigidbody rigidbody2D;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;
    public Transform viewCam;
    public GameObject inputs;

    public bool review;
    public int count = 0;
    public List<GameObject> informacion;
    public GameObject nextArrow, previousArrow;

    [SerializeField] ControlAudio _Audio;

    public GameObject desempeño,revision,joystick;


    // Start is called before the first frame update
    void Start()
    {
        previousArrow.GetComponent<Button>().onClick.AddListener(PreviousQuestion);
        nextArrow.GetComponent<Button>().onClick.AddListener(NextQuestion);

        if (Application.isMobilePlatform)
        {
            joystick.SetActive(true);
            moveSpeed = 3f;
        }
        else {
            joystick.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mover) {
            if (Application.isMobilePlatform)
            {
                
                moveInput = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));

                Vector3 moveHorizontal = transform.up * -moveInput.x;
                Vector3 moveVertical = transform.right * moveInput.y;
                if (SimpleInput.GetAxis("Vertical") > 0)
                {
                    rigidbody2D.velocity = (moveHorizontal + moveVertical) * moveSpeed;
                }
            }
            else {
                moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                Vector3 moveHorizontal = transform.up * -moveInput.x;
                Vector3 moveVertical = transform.right * moveInput.y;
                if (Input.GetAxis("Vertical") > 0)
                {
                    rigidbody2D.velocity = (moveHorizontal + moveVertical) * moveSpeed;
                }
            }
        }

        if (review)
        {
            if (count == 0)
            {
                previousArrow.SetActive(false);
            }
            if (count < informacion.Count - 1 && count > 0)
            {
                nextArrow.SetActive(true);
                previousArrow.SetActive(true);
            }
        }

    }


    public void ActivateReview()
    {
        review = true;
        count = 0;
        foreach (var item in informacion)
        {
            item.SetActive(false);
        }

        informacion[0].SetActive(true);
        previousArrow.SetActive(false);
        mover = false;
        rigidbody2D.velocity = Vector3.zero;

    }
    public void NextQuestion()
    {
        count++;

        _Audio.PlayAudio(0);
        if (count == informacion.Count)
        {
            desempeño.SetActive(true);
            revision.SetActive(false);
        }
        else if (count <= informacion.Count)
        {
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count].SetActive(true);
        }
    }
    public void PreviousQuestion()
    {
        _Audio.PlayAudio(0);

        if (count > 0)
        {
            count--;
            foreach (var item in informacion)
            {
                item.SetActive(false);
            }
            informacion[count].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Coin") {
            if (collision.gameObject.GetComponent<L4A255_coin>().mal)
            {
            }
            else {
                collision.gameObject.SetActive(false);
                collision.gameObject.GetComponent<L4A255_coin>().ejercicio.SetActive(true);
                mover = false;
                rigidbody2D.velocity = Vector3.zero;
            }
            
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "montaña") {
            inputs.SetActive(true);
            mover = false;
            rigidbody2D.velocity = Vector3.zero;
        }
    }
}
