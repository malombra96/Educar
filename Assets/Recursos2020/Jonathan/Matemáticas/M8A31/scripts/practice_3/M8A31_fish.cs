using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M8A31_fish : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bg;

    public bool isRight,isMoving,isEnabled;

    public List<Sprite> _sprites;
    M8A31_managerBoat _manager;
    ControlAudio audio;

    void Start()
    {
        audio = GameObject.FindObjectOfType<ControlAudio>();
        isEnabled = true;
        _sprites[0]= GetComponent<SpriteRenderer>().sprite;
        isMoving = true;
        _manager = GameObject.FindObjectOfType<M8A31_managerBoat>();
        _manager._fishes.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) {
            bg.GetComponent<BuoyancyEffector2D>().flowMagnitude = 0;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void OnMouseDown()
    {
        if (isEnabled) {
            audio.PlayAudio(0);
            Debug.Log(gameObject.name);
           gameObject.GetComponent<SpriteRenderer>().sprite = _sprites[1];
            isMoving = false;
            _manager.QualifyFish(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if (collision.gameObject.name == "Wall_1") {
            bg.GetComponent<BuoyancyEffector2D>().flowMagnitude = 2;
        }
        if (collision.gameObject.name == "Wall_2")
        {
            bg.GetComponent<BuoyancyEffector2D>().flowMagnitude = -2;
        }
    }
}
