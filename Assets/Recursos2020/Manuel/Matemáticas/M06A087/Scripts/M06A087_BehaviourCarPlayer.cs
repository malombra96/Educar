using System.Collections;
using UnityEngine;

public class M06A087_BehaviourCarPlayer : MonoBehaviour
{
    M06A087_ManagerTrail _ManagerTrail;

    
    // Start is called before the first frame update
    void Start()
    {
        _ManagerTrail = FindObjectOfType<M06A087_ManagerTrail>();
        _ManagerTrail._car = this;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(3,-274);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name.Contains("Question"))
            _ManagerTrail.SetInput(int.Parse(col.name.Split('_')[1]),true);
        else if(col.name.Contains("EndGame"))
            _ManagerTrail.SetInput(int.Parse(col.name.Split('_')[1]),true);
        else if(col.name == "Oil")
        {
            StartCoroutine(_ManagerTrail.AddOil());
            col.gameObject.SetActive(false);
        }
        else if(col.name == "Car")
        {
            StartCoroutine(_ManagerTrail.CrashCar());
            col.gameObject.SetActive(false);
        }
            
    }

    public void ResetCar()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(3,-274);
    }
}
