using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M9A7_rana : MonoBehaviour
{
    // Start is called before the first frame update

    public List<RectTransform> target;
    public float speed;
    public bool canMove;
    public int x;
    public Vector2 posicionInitial;

    void Start()
    {
        posicionInitial = GetComponent<RectTransform>().anchoredPosition;
        initial();
        
    }

    public void initial() {
        GetComponent<RectTransform>().anchoredPosition = posicionInitial;
        canMove = false;
        x = 0;
        GetComponent<Animator>().Play("ranaIdle");
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(GetComponent<RectTransform>().anchoredPosition, target[x].anchoredPosition, step);
            if (GetComponent<RectTransform>().anchoredPosition == target[x].anchoredPosition) {
                canMove = false;
                GetComponent<Animator>().Play("ranaIdle");
            }
        }
        
    }

    public void Move(int a) {
        canMove = true;
        x = a;
        GetComponent<Animator>().Play("ranaMove");
    }
}
