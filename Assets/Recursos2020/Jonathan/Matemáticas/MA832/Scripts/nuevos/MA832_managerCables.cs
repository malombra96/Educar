using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MA832_managerCables : MonoBehaviour
{
    // Start is called before the first frame update
    public ControlNavegacion ControlNavegacion;
    public List<MA832_managerInput> inputs;
    public GameObject bombs,life;
    public int boom= 0;
    public int count = 0;
    public int lifes;
    public GameObject explosion,bomba,marcador, cables;
    bool y;


    void Start()
    {
        lifes = life.transform.childCount;
        explosion.SetActive(false);
        bomba.SetActive(true);
        marcador.SetActive(true);
        cables.SetActive(true);
        y = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (count == inputs.Count)
        {
            ControlNavegacion.GoToLayout(7);
        }
        if (boom == bombs.transform.childCount)
        {
            
            explosion.SetActive(true);
            bomba.SetActive(false);
            marcador.SetActive(false);
            cables.SetActive(false);
            y = true;
            StartCoroutine(x());
        }
    }

    IEnumerator x() {
        if (y) {
            y = false;
            yield return new WaitForSeconds(2);
            ControlNavegacion.GoToLayout(7);
        }
        

    }

    public void GotoLayout()
    {
        
    }

    public void ActivateBombs(int x) {
       
        if (x< bombs.transform.childCount) {
            print("x");
            bombs.transform.GetChild(boom).gameObject.GetComponent<Image>().sprite = bombs.transform.GetChild(0).gameObject.GetComponent<BehaviourSprite>()._right;


        }
    }
}
