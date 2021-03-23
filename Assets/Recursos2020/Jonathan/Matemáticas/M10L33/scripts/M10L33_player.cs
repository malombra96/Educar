using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M10L33_player : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;

    public bool canMove;
    public int x = 0;

    [SerializeField] M10L33_manager _Manager;

    M10L33_mouseLook _lookMouse;

    public GameObject joystick,resumen1,resumen2,BG,pause,desempeño;

    public List<M10L33_element> elementos_1;
    
    // Start is called before the first frame update

    private void Awake()
    {
        _lookMouse = GameObject.FindObjectOfType<M10L33_mouseLook>();
        if (Application.isMobilePlatform)
        {
            joystick.SetActive(true);
        }
        else {
            joystick.SetActive(false);
        }
        resumen1.SetActive(false);
        resumen2.SetActive(true);
    }
    void Start()
    {
        canMove = true;
        resumen1.GetComponent<Button>().onClick.AddListener(resumen_1);
        resumen2.GetComponent<Button>().onClick.AddListener(resumen_2);
        pause.GetComponent<Button>().onClick.AddListener(pausa);


    }

    // Update is called once per frame
    void Update()
    {
        
            if (canMove)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                _lookMouse.canMove = true;
                if (Application.isMobilePlatform)
                {
                    float x = SimpleInput.GetAxis("Horizontal1");
                    float z = SimpleInput.GetAxis("Vertical1");

                    Vector3 move = transform.right * x + transform.forward * z;

                    controller.Move(move * speed * Time.deltaTime);
                }
                else
                {
                    float x = Input.GetAxis("Horizontal");
                    float z = Input.GetAxis("Vertical");

                    Vector3 move = transform.right * x + transform.forward * z;

                    controller.Move(move * speed * Time.deltaTime);
                }



            }
            else
            {
                _lookMouse.canMove = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            transform.position = new Vector3(transform.position.x, 0.67f, transform.position.z);



        //if (x == 1)
        //{
        //    resumen1.GetComponent<Button>().interactable = true;
        //    resumen2.GetComponent<Button>().interactable = false;
        //    resumen1.SetActive(true);
        //    resumen2.SetActive(false);
        //}
        //else if (x==2){
        //    resumen1.GetComponent<Button>().interactable = false;
        //    resumen2.GetComponent<Button>().interactable = true;
        //    resumen1.SetActive(false);
        //    resumen2.SetActive(true);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        canMove = false;




        if (other.name == "diamond") {
            other.gameObject.SetActive(false);
            _Manager.GetElement(other.gameObject);
        }
        else if (other.name == "coin")
        {
            other.gameObject.SetActive(false);
            _Manager.GetElement(other.gameObject);
        }
        else if (other.name == "star")
        {
            other.gameObject.SetActive(false);
            _Manager.GetElement(other.gameObject);
        }


        
    }
    public void pausaElementos() {
        canMove = false;
    }
    public void resumenElementos() {
        canMove = true;
    }
    public void pausa() {
        desempeño.GetComponent<Button>().interactable = true;
        pause.GetComponent<Button>().interactable = false;
        BG.SetActive(true);
            for (int i = 0; i < elementos_1.Count; i++)
            {
                for (int j = 0; j < elementos_1[i].child.Count; j++)
                {
                    if (elementos_1[i].child[j].activeSelf)
                    {
                        print(elementos_1[i].child[j].name);
                        x = 1;
                        break;
                    }

                }
            }

        if (x==1)
        {
            canMove = false;
            resumen1.GetComponent<Button>().interactable = true;
            resumen2.GetComponent<Button>().interactable = false;
            resumen1.SetActive(true);
            resumen2.SetActive(false);
            print("x1");
        }
        if(x == 0){
            canMove = false;
            resumen2.SetActive(true);
            resumen1.GetComponent<Button>().interactable = false;
            resumen2.GetComponent<Button>().interactable = true;
            resumen1.SetActive(false);
            print("x0");
        }
        
        
    }

    public void resumen_1() {
        print("resume_1");
        BG.SetActive(false);
        desempeño.GetComponent<Button>().interactable = false;
        canMove = false;
        resumen1.GetComponent<Button>().interactable = false;
        resumen2.GetComponent<Button>().interactable = false;
        pause.GetComponent<Button>().interactable = true;
    }
    public void resumen_2()
    {
        print("resume_2");
        BG.SetActive(false);
        canMove = true;
        resumen1.GetComponent<Button>().interactable = false;
        resumen2.GetComponent<Button>().interactable = false;
        x = 0;
        desempeño.GetComponent<Button>().interactable = false;
        pause.GetComponent<Button>().interactable = true;

    }
}
