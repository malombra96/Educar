using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M6L85_sight : MonoBehaviour
{
    public GameObject _camera;
    Vector3 target;

    bool cursorIn;
    public bool review, x;


    void Update()
    {
        if (Application.isMobilePlatform)
        {
            GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        }
        else
        {
            if (!review)
            {
                target = _camera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                transform.position = new Vector2(target.x, target.y);
            }
            else
            {
               GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "globo")
        {
            cursorIn = false;
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._selection;
        }
    }
   
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "globo")
        {
            cursorIn = false;
            GetComponent<Image>().sprite = GetComponent<BehaviourSprite>()._default;
        }
    }

    public void Review()
    {
        review = false;
    }
}
