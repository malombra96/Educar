using System.Collections;
using System.Collections.Generic;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M8A43_managerDraw : MonoBehaviour
{
    public int lineN;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;
    ControlPuntaje _controlPuntaje;

    [Header("PrefabLine")] public GameObject _prefab;
    [Header("Toggles List")] public List<Toggle> _ListToggles;
    [Header("Lista de lineas creadas")] public List<GameObject> _lines;

    [Header("Para eliminar las lineas")] public Button _trash;
    [Header("Arrastre el boton validar")] public Button _validar;

    Toggle _A;
    Toggle _B;
    [HideInInspector] public GameObject x; // Objeto temporal de la linea que se esta dibujando

    public GameObject firstToggle,secondToggle,thirdToggle;

    private LineRenderer lineRenderer;
    public List<RectTransform> checkPoints = new List<RectTransform>();
    public Color color1Right;
    public Color color1Wrong;
    public Material m;
    public GameObject pregunta;
    public bool op,first;
    public GameObject linea;
    [Header("Size Available Lines")] [Range(1, 2)] public int mov;

    int movTemp;




    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _validar.onClick.AddListener(Graficar);
        first = true;

        movTemp = mov;


        for (int c = 0; c < 13; c++) {
            for (int f = 0; f < 18; f++) {
                int n = (f + (18 * c));

                var x = transform.GetChild(n);

                x.name = "(" + (f - 8) + "," + (6 - c) + ")";

                x.GetComponent<M8A43_toggle>()._point = new Vector3((f - 8), (6 - c), 0.1f);
            }
        }

    }

    private void Update()
    {
        if (firstToggle != null && secondToggle != null && thirdToggle != null)
        {
            if (!op)
            {
                _validar.interactable = true;
            }
            else {
                _validar.interactable = false;
            }
            
        }
        else {
            if (op || !op) {
                _validar.interactable = false;
            }
            
        }
        
    }


    public void AddToggle(GameObject t) {

        if (firstToggle == null) {
            firstToggle = t.gameObject;
            checkPoints.Add(firstToggle.GetComponent<RectTransform>());
        } else if (firstToggle != null && secondToggle == null) {
            secondToggle = t.gameObject;
            checkPoints.Add(secondToggle.GetComponent<RectTransform>());
        } else if (firstToggle != null && secondToggle != null && thirdToggle == null) {
            thirdToggle= t.gameObject;
            checkPoints.Add(thirdToggle.GetComponent<RectTransform>());
        }
    }

    void DrawLine()
    {

        float y0 = _lines[0].GetComponent<LineRenderer>().GetPosition(0).y;
        float y1 = _lines[0].GetComponent<LineRenderer>().GetPosition(2).y;
        float x0 = _lines[0].GetComponent<LineRenderer>().GetPosition(0).x;
        float x1 = _lines[0].GetComponent<LineRenderer>().GetPosition(2).x;
        print (x0 + " / " + x1 + " / " + y0 + " / " + y1);

        float dx = (x1) - (x0);
        float dy = (y1) - (y0);

        if (dx < 0 || dx > 0)
        {
            float m = dy / dx;

            print(dx + " / " + dy + " / " + m);

            float b = ((-1) * (m * x0)) + y0;

            print(b);

            float y_0 = m * (8) + b;
            float y_1 = m * (-8) + b;

            float x_0 = (8 - b) / m;
            float x_1 = (-8 - b) / m;


        

        print("y: " + y_0 + " y_1: " + y_1 + " x: " + x_0 + " x_1: " + x_1);

        

        if (m == 0)
        {
            _lines[0].GetComponent<LineRenderer>().SetPosition(0, new Vector3(8, y0, 0));
            _lines[0].GetComponent<LineRenderer>().SetPosition(2, new Vector3(-8, y1, 0));
        }
        else if (m < 0 || m > 0)
        {

            if (y_0 > 8)
            {

                _lines[0].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x_0, 8, 0));

            }
            else if (y_0 < -8)
            {

                _lines[0].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x_1, -8, 0));

            }
            else
            {
                _lines[0].GetComponent<LineRenderer>().SetPosition(0, new Vector3(8, y_0, 0));
            }

            if (y_1 > 8)
            {

                _lines[0].GetComponent<LineRenderer>().SetPosition(2, new Vector3(x_0, 8, 0));

            }
            else if (y_1 < -8)
            {

                _lines[0].GetComponent<LineRenderer>().SetPosition(2, new Vector3(x_1, -8, 0));

            }
            else
            {
                _lines[0].GetComponent<LineRenderer>().SetPosition(2, new Vector3(-8, y_1, 0));
            }

        }
    }
        else
        {

            _lines[0].GetComponent<LineRenderer>().SetPosition(0, new Vector3(x0, 8, 0));
            _lines[0].GetComponent<LineRenderer>().SetPosition(2, new Vector3(x1, -8,0));

        }

    }

    public void Graficar() {
        if (op == false) {
            op = true;

            _validar.interactable = false;
            foreach (var x in _ListToggles)
            {
                x.interactable = false;
            }

            if (firstToggle.GetComponent<M8A43_toggle>().istrue &&
                secondToggle.GetComponent<M8A43_toggle>().istrue &&
                thirdToggle.GetComponent<M8A43_toggle>().istrue)
            {
                _controlAudio.PlayAudio(1);

                GameObject lineObject = new GameObject();
                GameObject w = Instantiate(_prefab, gameObject.transform);
                w.transform.SetParent(transform);
                w.transform.localScale = Vector3.one;
                this.lineRenderer = w.GetComponent<LineRenderer>();
                this.lineRenderer.startWidth = 3f;
                this.lineRenderer.endWidth = 3f;
                this.lineRenderer.GetComponent<LineRenderer>().startColor = color1Right;
                this.lineRenderer.GetComponent<LineRenderer>().endColor = color1Right;
                this.lineRenderer.name = "linea" + lineN.ToString();
                _lines.Add(this.lineRenderer.gameObject);
                linea = this.lineRenderer.gameObject;


                Vector3[] checkPointArray = new Vector3[checkPoints.Count];
                for (int i = 0; i < this.checkPoints.Count; i++)
                {
                    Vector3 checkPointpos = this.checkPoints[i].GetComponent<M8A43_toggle>()._point;
                    checkPointArray[i] = new Vector3(checkPointpos.x, checkPointpos.y, 0);
                }

                lineRenderer.SetPositions(checkPointArray);
                pregunta.SetActive(true);
                this.lineRenderer.alignment = LineAlignment.TransformZ;
               DrawLine();


            }
            else
            {
                _controlAudio.PlayAudio(2);
                GameObject lineObject = new GameObject();
                GameObject w = Instantiate(_prefab, gameObject.transform);
                w.transform.SetParent(transform);
                w.transform.localScale = Vector3.one;
                this.lineRenderer = w.GetComponent<LineRenderer>();
                this.lineRenderer.startWidth = 3f;
                this.lineRenderer.endWidth = 3f;
                this.lineRenderer.GetComponent<LineRenderer>().startColor = color1Wrong;
                this.lineRenderer.GetComponent<LineRenderer>().endColor = color1Wrong;
                this.lineRenderer.name = "linea" + lineN.ToString();
                //_lines.Add(this.lineRenderer.gameObject);
                linea = this.lineRenderer.gameObject;


                Vector3[] checkPointArray = new Vector3[checkPoints.Count];
                for (int i = 0; i < this.checkPoints.Count; i++)
                {
                    Vector3 checkPointpos = this.checkPoints[i].GetComponent<M8A43_toggle>()._point;
                    checkPointArray[i] = new Vector3(checkPointpos.x, checkPointpos.y, 0);
                }

                lineRenderer.SetPositions(checkPointArray);
                
                this.lineRenderer.alignment = LineAlignment.TransformZ;
                StartCoroutine(q());
                //               _controlAudio.PlayAudio(2);
                //               op = false;
                //               GameObject lineObject = new GameObject();
                //               GameObject w = Instantiate(_prefab, gameObject.transform);
                //               w.transform.SetParent(transform.parent);
                //               w.transform.localScale = Vector3.one;
                //               this.lineRenderer = w.GetComponent<LineRenderer>();
                //               this.lineRenderer.startWidth = 1f;
                //               this.lineRenderer.endWidth = 1f;

                //               //this.lineRenderer.GetComponent<LineRenderer>().startColor = color1Right;
                //               //this.lineRenderer.GetComponent<LineRenderer>().endColor = color1Right;
                //               this.lineRenderer.name = "linea" + lineN.ToString(); ;
                //               linea = this.lineRenderer.gameObject;
                //               this.lineRenderer.alignment = LineAlignment.TransformZ;
                //this.lineRenderer.GetComponent<LineRenderer>().startColor = color1Wrong;
                //               this.lineRenderer.GetComponent<LineRenderer>().endColor = color1Wrong;
                //               Vector3[] checkPointArray = new Vector3[checkPoints.Count];
                //               for (int i = 0; i < this.checkPoints.Count; i++)
                //               {
                //                   Vector3 checkPointpos = this.checkPoints[i].GetComponent<RectTransform>().anchoredPosition;
                //                   checkPointArray[i] = new Vector3(checkPointpos.x, checkPointpos.y, 0);
                //               }

                //               lineRenderer.SetPositions(checkPointArray);


                //              
            }
        }
        
    }

    
    IEnumerator q() {
        yield return new WaitForSeconds(3f);
        firstToggle = null;
        secondToggle= null;
        thirdToggle = null;
        foreach (var x in _ListToggles)
        {
            x.interactable = true;
            x.GetComponent<Image>().sprite =x.GetComponent<BehaviourSprite>()._default;
            x.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

        }

        Destroy(linea);
        op = false;

    }

    public void ResetAll()
    {
        if (first) {
            Destroy(linea);
            linea = null;
            op = false;
            pregunta.GetComponent<ManagerSeleccionarToggle>().ResetSeleccionarToggle();
            pregunta.SetActive(false);

            foreach (var x in _ListToggles)
            {
                x.interactable = true;
                x.GetComponent<Image>().sprite = x.GetComponent<BehaviourSprite>()._default;
                x.GetComponent<Image>().color = new Color32(255, 255, 255, 100);

            }
            _lines.Clear();
            firstToggle = null;
            secondToggle = null;
            thirdToggle = null;
            checkPoints.Clear();
        }



    }
}



