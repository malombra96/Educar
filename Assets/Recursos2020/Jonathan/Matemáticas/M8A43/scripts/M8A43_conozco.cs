using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M8A43_conozco : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject punto1, punto2;
    public ControlAudio controlAudio;
    public List<Button> botones;
    public List<GameObject> lineas;
        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrazarLinea(GameObject punto) {

        if (punto1 == null && punto2 == null) {
            punto1 = punto;
        } else if (punto1!= null && punto2 == null) {
            punto2 = punto;
        }

        VerificarPuntos();

    }

    public void VerificarPuntos() {
        if (punto1!= null && punto2 != null) {
          
            if (punto1.GetComponent<M8A43_boton>().pareja.Contains (punto2) || punto2.GetComponent<M8A43_boton>().pareja.Contains(punto1))
            {
                controlAudio.PlayAudio(1);
                
                //foreach (var x in botones)
                //{
                //    x.interactable = false;
                //}
                if ((punto1.name == "v1" && punto2.name == "v2") || (punto2.name == "v1" && punto1.name == "v2")) {
                    lineas[0].SetActive(true);

                    botones[0].interactable = false;
                    botones[1].interactable = false;
                    StartCoroutine(x());

                }
                if ((punto1.name == "v1" && punto2.name == "m") || (punto2.name == "v1" && punto1.name == "m"))
                {
                    lineas[0].SetActive(true);
      
                    botones[0].interactable = false;
                    botones[1].interactable = false;
                    StartCoroutine(x());


                }
                if ((punto1.name == "v2" && punto2.name == "m") || (punto2.name == "v2" && punto1.name == "m"))
                {
                    lineas[0].SetActive(true);

                    botones[0].interactable = false;
                    botones[1].interactable = false;
                    StartCoroutine(x());

                }


                ///-------
                ///

                if ((punto1.name == "r1" && punto2.name == "r2") || (punto2.name == "r1" && punto1.name == "r2"))
                {
                    lineas[1].SetActive(true);
                    botones[2].interactable = false;
                    botones[3].interactable = false;
                    StartCoroutine(x());

                }
                if ((punto1.name == "r1" && punto2.name == "m") || (punto2.name == "r1" && punto1.name == "m"))
                {
                    lineas[2].SetActive(true);
                    botones[2].interactable = false;
                    botones[3].interactable = false;
                    StartCoroutine(x());

                }
                if ((punto1.name == "r2" && punto2.name == "m") || (punto2.name == "r2" && punto1.name == "m"))
                {
                    lineas[3].SetActive(true);
                    botones[2].interactable = false;
                    botones[3].interactable = false;
                    StartCoroutine(x());

                }




            }
            else {
                controlAudio.PlayAudio(2);
                foreach (var x in botones)
                {
                    x.interactable = true;
                }
                punto2 = null;
                punto1 = null;
            }
        }
    }

    IEnumerator x() {
        yield return new WaitForSeconds(0.1f);
        punto1 = null;
        punto2 = null;
    }

    public void ResetAll() {
        punto1 = null;
        punto2 = null;
        foreach (var x in lineas) {
            x.SetActive(false);
        }
        foreach (var x in botones)
        {
            x.interactable = true   ;
        }
    }
}

