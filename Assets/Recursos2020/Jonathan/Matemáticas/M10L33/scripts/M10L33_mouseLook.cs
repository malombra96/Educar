using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M10L33_mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player;
    float xRotation = 0f;
    public bool canMove = false;
    public GameObject joystick;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
                float mouseX = SimpleInput.GetAxis("Horizontal2") * mouseSensitivity * Time.deltaTime;
                float mouseY = SimpleInput.GetAxis("Vertical2") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;


                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                player.Rotate(Vector3.up * mouseX);
            
        }
        
    }
}
