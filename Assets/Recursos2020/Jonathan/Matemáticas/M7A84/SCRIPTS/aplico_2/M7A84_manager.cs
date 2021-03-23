using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;


public class M7A84_manager : MonoBehaviour
{

    public GameObject navbar,inicio;
    public bool op,e,es,ev,first;
    public int counterPoints, count;
    public List<GameObject> puntos;
    public List<Toggle> toggles;
    public Button validar,Coordenadas,Segmentos,borrar;
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;
    [Header("PrefabLine")] public GameObject _prefabLine;
    [Header("Lista de lineas creadas")] public List<Line> _lines;
    public GameObject puntoInicio;
    public Sprite spritesUbicar,spritesSegmentos;
    public List<string> nombreCoo;


    Toggle _A;
    Toggle _B;
    [HideInInspector] public GameObject x; // Objeto temporal de la linea que se esta dibujando
    public int cxi,cxf, cyi,cyf;

    private void OnEnable()
    {
        navbar.SetActive(false);
    }
    private void OnDisable()
    {
        navbar.SetActive(true);
    }

    void Start()
    {
        
        inicio.SetActive(true);
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion= FindObjectOfType<ControlNavegacion>();
        controlPuntaje= FindObjectOfType<ControlPuntaje>();
        puntos.Capacity = counterPoints;
        for (int i = 0; i < counterPoints; i++) {
            GameObject n = new GameObject();
            n.name = "null";
            puntos.Add(n);

        }
        validar.onClick.AddListener(ValidarPuntos);
        Coordenadas.onClick.AddListener(Ubicar);
        Segmentos.onClick.AddListener(Crear);
        borrar.onClick.AddListener(Delete);
        
        Coordenadas.interactable = true;
        Segmentos.interactable = false;
        foreach (var t in toggles)
        {
            t.interactable = false;
            t.GetComponent<Image>().color = new Color32(255,255,255,100);
        }
        validar.interactable = false;
        borrar.interactable = false;
        first = true;
        int a = 0;

        //for (int i = cxi; i <= cxf; i++)
        //{
        //    for (int j = cyi; j >= cyf; j--)
        //    {
        //        nombreCoo.Add( i + "," + j );
        //    }
        //}

        //for (int i = 0; i < toggles.Count; i++)
        //{
        //    toggles[i].name = nombreCoo[i];
        //    string[] spearator = {"," };
        //    string[] strlist = toggles[i].name.Split(spearator,StringSplitOptions.None);
        //    toggles[i].GetComponent<M7A84_toggleA2>().x = int.Parse( strlist[0]);
        //    toggles[i].GetComponent<M7A84_toggleA2>().y = int.Parse(strlist[1]);
        //    //toggles[i].GetComponent<M7A84_toggleA2>()._point = new Vector3(int.Parse(strlist[0]), int.Parse(strlist[1]), 0.1f);
        //}

    }

    private void Update()
    {
        
        if (e)
        {
            if (count != counterPoints)
            {
                foreach (var t in toggles)
                {
                    t.interactable = true;
                }
            }
            if (op)
            {
                if (x != null && _B == null)
                {
                    Vector2 m = Input.mousePosition;
                    Vector3 w = Camera.main.ScreenToWorldPoint(m);
                    float z = _A.GetComponent<M7A84_toggleA2>()._point.z;
                    w = new Vector3(w.x, w.y, z);
                    x.GetComponent<LineRenderer>().SetPosition(1, w);
                }
            }

            if (_lines.Count == counterPoints)
            {
                if (ev)
                {
                    validar.interactable = true;
                    borrar.interactable = true;
                }
                else {
                    validar.interactable = false;
                    borrar.interactable = false;
                }
                    
                
                
            }
            else
            {
                if (ev)
                {
                    validar.interactable = false;
                    borrar.interactable = true;
                }
                else {
                    validar.interactable = false; 
                    borrar.interactable = true;
                }
                
                borrar.interactable = false;
            }

            if (count == counterPoints && es)
            {
                Segmentos.interactable = true;
            }
            else
            {
                Segmentos.interactable = false;
            }
        }


        
        
    }

    public void AddPoints(GameObject point) {
        if (count < counterPoints)
        {
            if (puntos[count].name == "null")
            {
                puntos[count] = point;
            }
            count++;


        }
        if (count == counterPoints)
        {
            foreach (var t in toggles)
            {
                t.interactable = false;
            }
            for (int i = 0; i < puntos.Count; i++)
            {
                puntos[i].GetComponent<Toggle>().interactable = true;
            }
            //Segmentos.interactable = true;
            es = true;
            ev = true;
        }
        else {
            
        }
    }

    public void DeletePoint(GameObject point) {
        if (puntos.Contains(point)) {
            int p = puntos.IndexOf(point);
            puntos.RemoveAt(p);
            count--;
            GameObject n = new GameObject();
            n.name = "null";
            puntos.Add(n);
            es = false;
        }
    }

    public void ValidarPuntos() {
        
        controlAudio.PlayAudio(0);
        ev = false;
        validar.interactable = false;
        borrar.interactable = false;
        int r = 0;
        for (int i = 0; i < puntos.Count; i++)
        {
            if (puntos[i].GetComponent<M7A84_toggleA2>().isRight)
            {
                puntos[i].GetComponent<Image>().sprite = puntos[i].GetComponent<BehaviourSprite>()._right;

                if (puntos[i].GetComponent<M7A84_toggleA2>().puntosRights.Count > 1) {
                    if ((puntos[i].GetComponent<M7A84_toggleA2>().puntosRights.Contains(puntos[i].GetComponent<M7A84_toggleA2>().puntoA)) &&
                        (puntos[i].GetComponent<M7A84_toggleA2>().puntosRights.Contains(puntos[i].GetComponent<M7A84_toggleA2>().puntoB)))
                    {

                        _lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._right;
                        r++;
                    }
                    else
                    {
                        _lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._wrong;
                    }
                }

                

                if (puntos[i].GetComponent<M7A84_toggleA2>().imagenCoordenada !=null) {
                    puntos[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<Image>().sprite = puntos[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<BehaviourSprite>()._right;
                }
               
                
               
                print(r);
            }
            else {
                if (puntos[i].GetComponent<M7A84_toggleA2>().imagenCoordenada != null)
                {
                    puntos[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<Image>().sprite = puntos[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<BehaviourSprite>()._wrong;
                }
                //puntos[i].GetComponent<Image>().sprite = puntos[i].GetComponent<BehaviourSprite>()._wrong;
                _lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._wrong;
            }
        }

        if (r == puntos.Count)
        {
            controlAudio.PlayAudio(1);
            controlPuntaje.IncreaseScore();

        }
        else {
            controlAudio.PlayAudio(2);
        }
        controlNavegacion.Forward(2);
    }

    public void Ubicar() {
        e = true;
        controlAudio.PlayAudio(0);
        foreach (var t in toggles)
        {
            t.interactable = true;
            t.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        Coordenadas.interactable = false;
        ;

    }

    public void Crear() {
        es = false;
        controlAudio.PlayAudio(0);
        Segmentos.interactable = false;
        foreach (var t in toggles)
        {
            t.interactable = false;
            //t.gameObject.SetActive(false);
        }

        for (int i = 0; i < puntos.Count; i++)
        {
            puntos[i].GetComponent<Toggle>().gameObject.SetActive(true);
            puntos[i].GetComponent<Toggle>().interactable = true;
            //puntos[i].GetComponent<Toggle>().isOn = false;
            puntos[i].GetComponent<M7A84_toggleA2>().op = true;
        }
        op = true;

        StartCoroutine(y());
    }

    IEnumerator y() {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < puntos.Count; i++)
        {
            puntos[i].GetComponent<Toggle>().isOn = false;
            puntos[i].GetComponent<Image>().sprite = spritesSegmentos;
            puntos[i].GetComponent<BehaviourSprite>()._default = spritesSegmentos;
        }
    }
    
    public void SetLine(Toggle t)
    {
        if (_A == null)
            CreateLine(t);
        else
            EndLine(t);
    }

    void CreateLine(Toggle t)
    {
        _A = t;
        x = Instantiate(_prefabLine, transform);
        x.GetComponent<LineRenderer>().SetPosition(0, t.GetComponent<M7A84_toggleA2>()._point);

    }

    void EndLine(Toggle t)
    {
        if (_A != t)
        {
            _B = t;
            x.GetComponent<LineRenderer>().SetPosition(1, t.GetComponent<M7A84_toggleA2>()._point);
            x.name = string.Concat(_A.name, "with", _B.name);



            if (puntoInicio == null)
            {
                puntoInicio = _A.gameObject;
                _A.GetComponent<M7A84_toggleA2>().puntoInicio = true;

                if (_A.GetComponent<M7A84_toggleA2>().puntoA == null && _B.GetComponent<M7A84_toggleA2>().puntoA == null)
                {
                    _A.GetComponent<M7A84_toggleA2>().puntoA = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoA = _A.gameObject;
                }
            }

            else {

                if (_A.GetComponent<M7A84_toggleA2>().puntoA == null && _B.GetComponent<M7A84_toggleA2>().puntoA == null)
                {
                    //print("if");
                    _A.GetComponent<M7A84_toggleA2>().puntoA = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoA = _A.gameObject;
                }

                else if ((_A.GetComponent<M7A84_toggleA2>().puntoA != null && _B.GetComponent<M7A84_toggleA2>().puntoB == null &&
                          _A.GetComponent<M7A84_toggleA2>().puntoB == null && _B.GetComponent<M7A84_toggleA2>().puntoA != null
                          && !_B.GetComponent<M7A84_toggleA2>().puntoInicio && !_A.GetComponent<M7A84_toggleA2>().puntoInicio))
                {
                    print("1else");


                    _A.GetComponent<M7A84_toggleA2>().puntoB = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoB = _A.gameObject;

                } else if ((_A.GetComponent<M7A84_toggleA2>().puntoA == null && _A.GetComponent<M7A84_toggleA2>().puntoB != null &&
                            _B.GetComponent<M7A84_toggleA2>().puntoA != null && _B.GetComponent<M7A84_toggleA2>().puntoB == null 
                            && !_B.GetComponent<M7A84_toggleA2>().puntoInicio && !_A.GetComponent<M7A84_toggleA2>().puntoInicio))
                {
                    print("2else");
                    _A.GetComponent<M7A84_toggleA2>().puntoA = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoB = _A.gameObject;
                }

                else if ((_A.GetComponent<M7A84_toggleA2>().puntoA != null && _A.GetComponent<M7A84_toggleA2>().puntoB == null &&
                          _B.GetComponent<M7A84_toggleA2>().puntoA == null && _B.GetComponent<M7A84_toggleA2>().puntoB != null
                          && !_B.GetComponent<M7A84_toggleA2>().puntoInicio && !_A.GetComponent<M7A84_toggleA2>().puntoInicio))
                {
                    print("3else");


                    _A.GetComponent<M7A84_toggleA2>().puntoB = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoA = _A.gameObject;

                }

                else if ((_A.GetComponent<M7A84_toggleA2>().puntoB == null && _B.GetComponent<M7A84_toggleA2>().puntoB == null))
                {
                    print("4else");
                    _A.GetComponent<M7A84_toggleA2>().puntoB = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoB = _A.gameObject;

                }

                else if ((_A.GetComponent<M7A84_toggleA2>().puntoA == null && _A.GetComponent<M7A84_toggleA2>().puntoB != null &&
                            _B.GetComponent<M7A84_toggleA2>().puntoA != null && _B.GetComponent<M7A84_toggleA2>().puntoB == null
                            && _B.GetComponent<M7A84_toggleA2>().puntoInicio && !_A.GetComponent<M7A84_toggleA2>().puntoInicio))
                {
                    print("5else");
                    _A.GetComponent<M7A84_toggleA2>().puntoA = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoB = _A.gameObject;
                }

                else if ((_A.GetComponent<M7A84_toggleA2>().puntoA != null && _A.GetComponent<M7A84_toggleA2>().puntoB == null &&
                            _B.GetComponent<M7A84_toggleA2>().puntoA == null && _B.GetComponent<M7A84_toggleA2>().puntoB != null
                            && !_B.GetComponent<M7A84_toggleA2>().puntoInicio && _A.GetComponent<M7A84_toggleA2>().puntoInicio))
                {
                    print("6else");
                    _A.GetComponent<M7A84_toggleA2>().puntoB = _B.gameObject;
                    _B.GetComponent<M7A84_toggleA2>().puntoA = _A.gameObject;
                }

            }
            AddLine();            
        }

    }

    IEnumerator q(GameObject a) {
        yield return new WaitForSeconds(0.3f);
        print(a);

        for (int i = 0; i < _lines.Count; i++)
        {
            if (_lines[i].lineRender.name == a.name)
            {
                _lines.RemoveAt(i);
            }
        }
        Destroy(a);
    }

    // lucia perez cod:1206  bono. PARQUEADERO SHERATON


    public void ClearTemp()
    {
        _A = null; _B = null; x = null;
    }

    void AddLine()
    {
        Line line = new Line { toggle = { [0] = _A, [1] = _B }, lineRender = x };
        bool w = false;

        for (int i = 0; i < _lines.Count; i++)
        {
            if (_lines[i].lineRender.name == line.lineRender.name)
            {
              //  print("repetido");
                w = true;
                break;
            }
            
        }

        if (!w)
        {
            _lines.Add(line);
            if (_A.GetComponent<M7A84_toggleA2>().puntoB == _A.GetComponent<M7A84_toggleA2>().puntoA && _B.GetComponent<M7A84_toggleA2>().puntoB == _B.GetComponent<M7A84_toggleA2>().puntoA)
            {
                StartCoroutine(q(line.lineRender));
                _A.GetComponent<M7A84_toggleA2>().puntoB = null;
                _B.GetComponent<M7A84_toggleA2>().puntoB = null;
            }

            ClearTemp();
            //_A.isOn = false;
            //_B.isOn = false;

            StartCoroutine(a());

        }
        else {
            line.toggle[0].GetComponent<M7A84_toggleA2>().puntoB = null;
            line.toggle[1].GetComponent<M7A84_toggleA2>().puntoB = null;
            Destroy(line.lineRender.gameObject);
            ClearTemp();
            //_A.isOn = false;
            //_B.isOn = false;

            StartCoroutine(a());
        }

        ClearTemp();
        //_A.isOn = false;
        //_B.isOn = false;

        StartCoroutine(a());
    }

    IEnumerator a() {
        //print("sa!");
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < puntos.Count; i++)
        {
            puntos[i].GetComponent<Toggle>().isOn = false;
        }
    }

    public void Delete() {
        ev = false;
        es = false;
        validar.interactable = false;
        borrar.interactable = false;
        puntos.Clear();
        puntos.Capacity = counterPoints;
        puntoInicio = null;
        controlAudio.PlayAudio(0);
        for (int i = 0; i < _lines.Count; i++)
        {
            foreach (var toggle in _lines[i].toggle)
            {
                toggle.isOn = false;
                toggle.interactable = true;
            }

            Destroy(_lines[i].lineRender);
        }

        _lines.Clear();

        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].GetComponent<Toggle>().isOn = false;
            toggles[i].GetComponent<Toggle>().interactable = false;
            toggles[i].GetComponent<M7A84_toggleA2>().puntoA = null;
            toggles[i].GetComponent<M7A84_toggleA2>().puntoB = null;
            toggles[i].GetComponent<M7A84_toggleA2>().puntoInicio = false;
            toggles[i].GetComponent<M7A84_toggleA2>().op = false;
            toggles[i].GetComponent<Image>().sprite = spritesUbicar;
            toggles[i].GetComponent<BehaviourSprite>()._default = spritesUbicar;

            if (toggles[i].GetComponent<M7A84_toggleA2>().imagenCoordenada != null)
            {
                toggles[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<Image>().sprite = toggles[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<BehaviourSprite>()._default;
            }
        }
        _A = null;
        _B = null;
        op = false;
        count = 0;
        e = false;

        Destroy(null);
        for (int i = 0; i < counterPoints; i++)
        {
            GameObject n = new GameObject();
            n.name = "null";
            puntos.Add(n);

        }

        //for (int i = 0; i < puntos.Count; i++)
        //{

        //    //puntos[i].GetComponent<Image>().sprite = puntos[i].GetComponent<BehaviourSprite>()._default;
        //    _lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._default;
        //    //_lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._wrong;

        //}
        Coordenadas.interactable = true;
        Segmentos.interactable = false;
    }
    public void ResetLines()
    {
        if (first) {
            inicio.SetActive(true);
            ev = false;
            es = false;
            validar.interactable = false;
            borrar.interactable = false;
            puntos.Clear();
            puntos.Capacity = counterPoints;
            puntoInicio = null;

            for (int i = 0; i < _lines.Count; i++)
            {
                foreach (var toggle in _lines[i].toggle)
                {
                    toggle.isOn = false;
                    toggle.interactable = true;
                }

                Destroy(_lines[i].lineRender);
            }

            _lines.Clear();

            for (int i = 0; i < toggles.Count; i++)
            {
                toggles[i].GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                toggles[i].GetComponent<Toggle>().isOn = false;
                toggles[i].GetComponent<Toggle>().interactable = false;
                toggles[i].GetComponent<M7A84_toggleA2>().puntoA = null;
                toggles[i].GetComponent<M7A84_toggleA2>().puntoB = null;
                toggles[i].GetComponent<M7A84_toggleA2>().puntoInicio = false;
                toggles[i].GetComponent<M7A84_toggleA2>().op = false;
                toggles[i].GetComponent<Image>().sprite = spritesUbicar;
                toggles[i].GetComponent<BehaviourSprite>()._default = spritesUbicar;

                if (toggles[i].GetComponent<M7A84_toggleA2>().imagenCoordenada != null)
                {
                    toggles[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<Image>().sprite = toggles[i].GetComponent<M7A84_toggleA2>().imagenCoordenada.GetComponent<BehaviourSprite>()._default;
                }
            }
            _A = null;
            _B = null;
            op = false;
            count = 0;
            e = false;

            Destroy(null);
            for (int i = 0; i < counterPoints; i++)
            {
                GameObject n = new GameObject();
                n.name = "null";
                puntos.Add(n);

            }

            //for (int i = 0; i < puntos.Count; i++)
            //{

            //    //puntos[i].GetComponent<Image>().sprite = puntos[i].GetComponent<BehaviourSprite>()._default;
            //    _lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._default;
            //    //_lines[i].lineRender.GetComponent<LineRenderer>().material.color = _lines[i].lineRender.GetComponent<BehaviourDrawLine>()._wrong;

            //}
            Coordenadas.interactable = true;
            Segmentos.interactable = false;
            //controlPuntaje.resetScore();
        }


    }
}
