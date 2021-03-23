using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M6L85_globo : MonoBehaviour
{
    // Start is called before the first frame update

    public Button button;
    public ControlAudio controlAudio;
    public int number;
    public bool isRight;
    public M6L85_managerBallon manager;
    public GameObject roof,floor;
    public float movespeed;
    public Vector3 inicial,positionReview;
    public bool mover, isFirst;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectioBallon);
        
        RandomSpeed();

    }


    public void RandomSpeed() {
        movespeed = Random.Range(0.004f,0.009f);
    }
    void Update()
    {
        if (mover)
        {

            movespeed += 0.0000001f;
            transform.position = new Vector3(transform.position.x, transform.position.y + movespeed);
            GetComponent<Animator>().SetBool("play", true);
        }
        else
        {
            movespeed = 0;
            GetComponent<Animator>().SetBool("play", false);
        }
    }

    public void SelectioBallon() {
        controlAudio.PlayAudio(0);
        manager.GetBallon(this);
        button.interactable = false;
        GetComponent<Animator>().SetBool("op",true);
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "floor") {
        //    GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-603,531), roof.GetComponent<RectTransform>().anchoredPosition.y); ;
        //}
        if (collision.name == "roof")
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector3(Random.Range(-603, 531), floor.GetComponent<RectTransform>().anchoredPosition.y);
            RandomSpeed();
        }

    }

}
