using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L11A270_BehaviourPlayer : MonoBehaviour
{
    [Header("Camera Player")] 
    public Camera _camera;
    /* [Range(50,200)] */ public float sensitivity = 10f;
    float xRot = 0;

    [Header("Player")] 
    public Transform _player;
    [Range(0,1)] public float vel;

    [Header("Character Controller")] 
    public CharacterController controller;

    [Header("Objects Word")] public List<GameObject> _words;

    [Range(0,1)][Header("Size Line")] public float _valueZoom;
    bool stateHit;

    [Header("State Player")] public bool statePlayer;

    RaycastHit hit;

    [Header("Current Hit")] public GameObject currentHit;
    [Header("Word Last Hit")] public GameObject wordHit;
    string captureBtn = "Capture";

    [Header("Controllers Mobile")] public GameObject _mobileControllers;
    

    void Start()
    {
         /* if(!Application.isMobilePlatform)
            Cursor.lockState = CursorLockMode.Locked; */

        _mobileControllers.SetActive(Application.isMobilePlatform);
        statePlayer = false;
    }

    void FixedUpdate()
    {
        if (statePlayer)
        {
            SetRotationView();
            MovementPlayer();
            SetRayCastHit();
        }
    }

    void SetRayCastHit()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * _valueZoom, Color.red);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * _valueZoom, out hit, _valueZoom))
        {
            //print(hit.collider.name);

            currentHit = hit.collider.gameObject;

            if (hit.collider.gameObject.GetComponent<L11A270_BehaviourWord>())
            {
                wordHit = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<L11A270_BehaviourWord>().seen = true;

                if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown(captureBtn))
                    wordHit.GetComponent<L11A270_BehaviourWord>().selected = true;
            }

        }

        SetWordState();
    }

    void SetWordState()
    {
        if (wordHit)
        {
            wordHit.GetComponent<L11A270_BehaviourWord>().seen = (wordHit == currentHit);
            wordHit.GetComponent<L11A270_BehaviourWord>().SetSpriteState();
        }
    }

    void SetRotationView()
    {
        float viewX, viewY;

        if (Application.isMobilePlatform)
        {
            viewX = SimpleInput.GetAxis("ViewH") * sensitivity * Time.deltaTime;
            viewY = SimpleInput.GetAxis("ViewV") * sensitivity * Time.deltaTime;
        }
        else
        {
            viewX = Input.GetAxis("ViewH") * sensitivity * Time.deltaTime;
            viewY = Input.GetAxis("ViewV") * sensitivity * Time.deltaTime;
            //print($"X={Input.GetAxis("ViewH")},Y={Input.GetAxis("ViewV")}");
        }



        xRot -= viewY;
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        _camera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        _player.Rotate(Vector3.up * viewX);


    }

    void MovementPlayer()
    {
        float movX,movZ;

        if(Application.isMobilePlatform)
        {
            movX = SimpleInput.GetAxis("Horizontal");
            movZ = SimpleInput.GetAxis("Vertical");
        }
        else
        {
            movX = Input.GetAxis("Horizontal");
            movZ = Input.GetAxis("Vertical");
        }

        Vector3 move = (_player.right*movX) + (_player.forward*movZ);
        controller.Move(move*vel);


    } 

    public void ResetPlayer()
    {
        statePlayer = false;

        _player.transform.localPosition = new Vector3(0.88f,0.02f,-0.05f);
        _player.transform.localEulerAngles = new Vector3(0f,-90f,0f);
        _camera.transform.localEulerAngles = new Vector3(0f,0f,0f);


        foreach (GameObject word in _words)
        {
            word.GetComponent<MeshRenderer>().material.SetTexture("_MainTex",word.GetComponent<BehaviourSprite>()._default.texture);
            word.GetComponent<L11A270_BehaviourWord>().seen = false;
            word.GetComponent<L11A270_BehaviourWord>().selected = false;
            word.GetComponent<L11A270_BehaviourWord>().onlyOnce = true;
        }
    }
 
}
