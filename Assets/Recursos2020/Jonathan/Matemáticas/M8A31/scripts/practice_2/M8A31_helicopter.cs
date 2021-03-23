using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class M8A31_helicopter : MonoBehaviour
{
    public float speed = 6f, jumpForce = 500f, speedAnim;
    //private float bounds = 27.8f;
    private Rigidbody2D myBody;
	public bool canMove,anim;
	
	M8A31_ManagerHelicopter _firemanManager;
	public Transform target;
	public Vector2 initialPosition;

	public GameObject jostick,buttonSeleccionar;
	private float xMin = -596f, xMax = 134f;



	ControlAudio audio;
    // Start is called before the first frame update
    void Start()
    {
		if (Application.isMobilePlatform)
		{
			jostick.SetActive(true);
		}
		else {
			jostick.SetActive(false);
		}
		audio = GameObject.FindObjectOfType<ControlAudio>();
		initialPosition = transform.position;
		_firemanManager = GameObject.FindObjectOfType<M8A31_ManagerHelicopter>();
		myBody = GetComponent<Rigidbody2D>();
		canMove = true;
    }

	private void Update()
	{
		
		if (anim) {
			float step = speedAnim * Time.deltaTime;

			// move sprite towards the target location
			myBody.bodyType = RigidbodyType2D.Kinematic;
			transform.position = Vector2.MoveTowards(transform.position, target.position, step);
		}
		
	}
	private void FixedUpdate()
    {
		if (canMove) {
			Movement();
		}
        
    }

	void Movement()
	{
		if (Application.isMobilePlatform) {
			float h = SimpleInput.GetAxis("Horizontal");

			if (h > 0)
			{
				

				myBody.velocity = new Vector2(speed * h, myBody.velocity.y);

			}
			else if (h < 0)
			{
				myBody.velocity = new Vector2(speed * h, myBody.velocity.y);
	
			}
			else
			{
				myBody.velocity = new Vector2(0, myBody.velocity.y);
			}
			if (Input.GetKey(KeyCode.Space))
			{
				myBody.bodyType = RigidbodyType2D.Dynamic;
			}
		}
		else { 
			float h = Input.GetAxisRaw("Horizontal");

		if (h > 0)
		{
			myBody.velocity = new Vector2(speed * h, myBody.velocity.y);
		}
		else if (h < 0)
		{
			myBody.velocity = new Vector2(speed * h, myBody.velocity.y);
		}
		else
		{
			myBody.velocity = new Vector2(0, myBody.velocity.y);
		
		}
		if (Input.GetKey(KeyCode.Space))
		{
			myBody.bodyType = RigidbodyType2D.Dynamic;
		}
		}
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Fireman") {
			audio.PlayAudio(0);
			canMove = false;
			_firemanManager.QualifyFireman(collision.gameObject, gameObject);
			collision.gameObject.GetComponent<SpriteRenderer>().sprite = collision.gameObject.GetComponent<M8A31_fireman>()._sprites[1];
			

		}
		print(collision.gameObject.name);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "collider") {
			transform.position = initialPosition;
		}
	}


	public void Space() { 
		myBody.bodyType = RigidbodyType2D.Dynamic;
		buttonSeleccionar.GetComponent<Button>().interactable = false;
	}
}
