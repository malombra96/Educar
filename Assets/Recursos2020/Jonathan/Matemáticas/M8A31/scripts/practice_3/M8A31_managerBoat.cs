using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A31_managerBoat : MonoBehaviour
{
    public GameObject currentFish;
    private float speed = 10.0f;
    
    private Vector2 position;

    bool isMoving;

    public Image spriteCalificacion;

    public Sprite spriteRight,spriteWrong;

    public List<GameObject> _fishes;

    ControlAudio audio;

    ControlNavegacion navegacion;

    ControlPuntaje puntaje;
    bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        audio = GameObject.FindObjectOfType<ControlAudio>();
        puntaje = GameObject.FindObjectOfType<ControlPuntaje>();
        navegacion = GameObject.FindObjectOfType<ControlNavegacion>();
        isMoving = false;
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, currentFish.transform.position, step);
        }
        
    }

    public void QualifyFish(GameObject fish){
        for (int i = 0; i < _fishes.Count; i++) {
            _fishes[i].GetComponent<M8A31_fish>().isMoving = false;
            _fishes[i].GetComponent<M8A31_fish>().isEnabled = false;
        }
        currentFish = fish;
        if (fish.GetComponent<M8A31_fish>().isRight) {
            spriteCalificacion.gameObject.SetActive(true);
            spriteCalificacion.sprite = spriteRight;
            isMoving = true;
            audio.PlayAudio(1);
            puntaje.IncreaseScore();
            print("+1");
        }
        else {
            audio.PlayAudio(2);
            isMoving = true;
            spriteCalificacion.gameObject.SetActive(true);
            spriteCalificacion.sprite = spriteWrong;
        }
        navegacion.Forward(1);
    }

    public void ResetAll()
    {
        if (firstTime) {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].GetComponent<M8A31_fish>().isMoving = true;
                _fishes[i].GetComponent<M8A31_fish>().isEnabled = true;
                _fishes[i].GetComponent<SpriteRenderer>().sprite = _fishes[i].GetComponent<M8A31_fish>()._sprites[0];

                _fishes[i].GetComponent<M8A31_fish>().bg.GetComponent<BuoyancyEffector2D>().flowMagnitude = 2;
                _fishes[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

            isMoving = false;
            gameObject.transform.position = position;
            spriteCalificacion.gameObject.SetActive(false);
            puntaje.resetScore();
        }
        
    }
}
