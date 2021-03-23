using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5A259_barco : MonoBehaviour
{
    public bool mover;
    public float speed;
    Vector3 Vec;
    public L5A259_mapa mapa;

    public float verticalInputAcceleration = 1;
    public float horizontalInputAcceleration = 20;

    public float maxSpeed = 10;
    public float maxRotationSpeed = 100;

    public float velocityDrag = 1;
    public float rotationDrag = 1;

    private Vector3 velocity;
    private float zRotationVelocity;

    public GameObject joystick;


    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            joystick.SetActive(true);
        }
        else {
            joystick.SetActive(false);
        }
    }
    private void Update()
    {
        if (mover) {
            if (Application.isMobilePlatform)
            {
                Vector3 acceleration = SimpleInput.GetAxis("Vertical") * verticalInputAcceleration * transform.up;
                velocity += acceleration * Time.deltaTime;

                // apply turn input
                float zTurnAcceleration = -1 * SimpleInput.GetAxis("Horizontal") * horizontalInputAcceleration;
                zRotationVelocity += zTurnAcceleration * Time.deltaTime;
            }
            else {
                
                Vector3 acceleration = Input.GetAxis("Vertical") * verticalInputAcceleration * transform.up;
                velocity += acceleration * Time.deltaTime;

                // apply turn input
                float zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration;
                zRotationVelocity += zTurnAcceleration * Time.deltaTime;
            }
            
        }
        // apply forward input
        
    }

    private void FixedUpdate()
    {
        if (mover) {
            // apply velocity drag
            velocity = velocity * (1 - Time.deltaTime * velocityDrag);

            // clamp to maxSpeed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            // apply rotation drag
            zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * rotationDrag);

            // clamp to maxRotationSpeed
            zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

            // update transform
            transform.position += velocity * Time.deltaTime;
            transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
        }
      
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Isla_1") {
            print("ss");
            if (collision.gameObject.GetComponent<L5A259_isla>().habilitado) {
                collision.gameObject.GetComponent<L5A259_isla>().Activar();
            }
        }
    }
}

