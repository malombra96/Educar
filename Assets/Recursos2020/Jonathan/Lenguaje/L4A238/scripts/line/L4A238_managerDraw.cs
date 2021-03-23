using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class L4A238_managerDraw : MonoBehaviour, IPointerClickHandler
{
    public L4A238_general general;
    public GameObject inicio;
    //public List<M6L101_frog> frogs;
    [HideInInspector] public ControlAudio _controlAudio;
    ControlNavegacion _controlNavegacion;
    ControlPuntaje _controlPuntaje;

    [Header("PrefabLine")] public GameObject _prefab;
    [HideInInspector] [Header("Toggles List")] public List<Toggle> _ListToggles;
    [HideInInspector] [Header("Lista de lineas creadas")] public List<Line> _lines;

    [Header("Para eliminar las lineas")] public Button _trash;
    [Header("Arrastre el boton validar")] public Button _validar;

    Toggle _A;
    Toggle _B;
    [HideInInspector] public GameObject x; // Objeto temporal de la linea que se esta dibujando

    public enum TypeValidation
    {
        inmediata,
        boton
    }
    [Header("Tipo de validacion")] public TypeValidation _typeValidation;

    #region Parametres Validacion Inmediata 
    int evaluadas = 0;
    int correctas = 0;

    #endregion

    public enum TypeCalification
    {
        Onlyline,
        LineToggles
    }
    [Header("Tipo de calificacion")] public TypeCalification _typeCalification;

    public enum TypeScoring
    {
        EachLine,
        GroupLine
    }

    [Header("Tipo de Puntuacion")] public TypeScoring _typeScoring;


    void Start()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();

        _trash.gameObject.SetActive(_typeValidation == TypeValidation.boton);
        _trash.onClick.AddListener(DeleteLines);
        StateBtnDelete();

        _validar.gameObject.SetActive(_typeValidation == TypeValidation.boton);
        _validar.onClick.AddListener(ValidarLines);
        StateBtnValidar();
    }

    #region SetupLineDraw

    /// <summary>
    /// Evalua si la linea comienza o termina
    /// </summary>
    /// <param name="t"></param>
    public void SetLine(Toggle t)
    {
        if (_A == null)
            CreateLine(t);
        else
            EndLine(t);
    }

    /// <summary>
    /// Instancia la linea con las coordenadas del toggle recibido y desactiva los del mismo tipo
    /// </summary>
    /// <param name="t"></param>
    void CreateLine(Toggle t)
    {
        _A = t;
        x = Instantiate(_prefab, transform);
        x.GetComponent<LineRenderer>().SetPosition(0, t.GetComponent<L4A238_toggleDraw>()._point);

    }
    /// <summary>
    /// Finaliza la linea con las coordenadas del toggle recibido
    /// </summary>
    /// <param name="t"></param>
    void EndLine(Toggle t)
    {
        if (_A != t)
        {
            _B = t;
            x.GetComponent<LineRenderer>().SetPosition(1, t.GetComponent<L4A238_toggleDraw>()._point);
            x.name = string.Concat(_A.name, "with", _B.name);

            _A.interactable = false;
            _B.interactable = false;

            AddLine();
        }

    }

    /// Elimina la linea que se estaba dibujando y reinicia estado de toggles seleccionados
    public void DeleteLineDraw()
    {
        if (_A != null)
            _A.isOn = false;

        if (_B != null)
            _B.isOn = false;

        if (x != null)
            Destroy(x);

        StateBtnDelete();
        ClearTemp();

    }

    /// <summary>
    /// Agrega la linea a lista
    /// </summary>
    void AddLine()
    {
        Line line = new Line { toggle = { [0] = _A, [1] = _B }, lineRender = x };
        _lines.Add(line);
        StateBtnDelete();
        StateBtnValidar();
        ClearTemp();

        if (_typeValidation == TypeValidation.inmediata)
            ValidarLineImmediate();
    }

    /// <summary>
    /// Limpia Temporales
    /// </summary>
    public void ClearTemp()
    {
        _A = null; _B = null; x = null;
    }

    #endregion

    /// Establece el estado del boton que permite eliminar las lineas
    void StateBtnDelete()
    {
        if (_typeValidation == TypeValidation.boton)
            _trash.gameObject.SetActive(_lines.Count > 0);
    }
    void StateBtnValidar()
    {
        if (_typeValidation == TypeValidation.boton)
            _validar.interactable = ((_lines.Count * 2) == _ListToggles.Count);

    }

    /// Elimina todas las lineas instanciadas y limpia la lista
    void DeleteLines()
    {
        _controlAudio.PlayAudio(0);

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

        StateBtnDelete();
        StateBtnValidar();

    }

    private void Update()
    {
        if (x != null && _B == null)
        {
            Vector2 m = Input.mousePosition;
            Vector3 w = Camera.main.ScreenToWorldPoint(m);
            float z = _A.GetComponent<L4A238_toggleDraw>()._point.z;
            w = new Vector3(w.x, w.y, z);
            x.GetComponent<LineRenderer>().SetPosition(1, w);
        }
    }

    /// <summary>
    /// Si da click fuera de toggle
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        DeleteLineDraw();
    }

    void ValidarLines()
    {
        _validar.interactable = false;
        _trash.gameObject.SetActive(false);
        _controlAudio.PlayAudio(0);
        int correct = 0;

        foreach (var line in _lines)
        {
            var x = line.toggle[0].GetComponent<L4A238_toggleDraw>();
            var y = line.toggle[1].GetComponent<L4A238_toggleDraw>();

            if (x._match.Contains(line.toggle[1]) || y._match.Contains(line.toggle[0]))
            {
                switch (_typeCalification)
                {
                    case TypeCalification.Onlyline: SetLineAnswer(line.lineRender.GetComponent<LineRenderer>(), true); break;

                    case TypeCalification.LineToggles:
                        SetLineAnswer(line.lineRender.GetComponent<LineRenderer>(), true);
                        SetTogglesAnswer(line.toggle, true);
                        break;

                }
                
                //y.frog.Move();
                correct++;
            }
            else
            {
                switch (_typeCalification)
                {
                    case TypeCalification.Onlyline: SetLineAnswer(line.lineRender.GetComponent<LineRenderer>(), false); break;

                    case TypeCalification.LineToggles:
                        SetLineAnswer(line.lineRender.GetComponent<LineRenderer>(), false);
                        SetTogglesAnswer(line.toggle, false);
                        break;

                }
            }

        }

        switch (_typeScoring)
        {
            case TypeScoring.EachLine: _controlPuntaje.IncreaseScore(correct); break;

            case TypeScoring.GroupLine:

                if (correct == _lines.Count)
                    _controlPuntaje.IncreaseScore();

                break;

        }

        if (correct == _lines.Count)
        {
            _controlAudio.PlayAudio(1);
            general.NextExcersise();

        }
        else
        {
            _controlAudio.PlayAudio(2);
            general.NextExcersise();
        }

        //_controlNavegacion.Forward(2);
    }

    void ValidarLineImmediate()
    {
        var x = _lines[evaluadas].toggle[0].GetComponent<L4A238_toggleDraw>();
        var y = _lines[evaluadas].toggle[1].GetComponent<L4A238_toggleDraw>();

        foreach (var toggle in _lines[evaluadas].toggle)
            toggle.interactable = false;



        if (x._match.Contains(_lines[evaluadas].toggle[1]) || y._match.Contains(_lines[evaluadas].toggle[0]))
        {
            switch (_typeCalification)
            {
                case TypeCalification.Onlyline: SetLineAnswer(_lines[evaluadas].lineRender.GetComponent<LineRenderer>(), true); break;

                case TypeCalification.LineToggles:
                    SetLineAnswer(_lines[evaluadas].lineRender.GetComponent<LineRenderer>(), true);
                    SetTogglesAnswer(_lines[evaluadas].toggle, true);
                    break;

            }

            correctas++;
            _controlAudio.PlayAudio(1);

        }
        else
        {
            switch (_typeCalification)
            {
                case TypeCalification.Onlyline: SetLineAnswer(_lines[evaluadas].lineRender.GetComponent<LineRenderer>(), false); break;

                case TypeCalification.LineToggles:
                    SetLineAnswer(_lines[evaluadas].lineRender.GetComponent<LineRenderer>(), false);
                    SetTogglesAnswer(_lines[evaluadas].toggle, false);
                    break;

            }

            _controlAudio.PlayAudio(2);
        }

        evaluadas++;

        if (evaluadas == _ListToggles.Count / 2)
        {
            if (_typeScoring == TypeScoring.GroupLine)
            {
                if (correctas == _ListToggles.Count / 2)
                    _controlPuntaje.IncreaseScore();
            }
            else
                _controlPuntaje.IncreaseScore(correctas);


            _controlNavegacion.Forward(2);

        }

    }

    void SetLineAnswer(LineRenderer line, bool state) => line.material.color = state ? line.GetComponent<BehaviourDrawLine>()._right : line.GetComponent<BehaviourDrawLine>()._wrong;

    void SetTogglesAnswer(Toggle[] _match, bool state)
    {
        foreach (var toggle in _match)
            toggle.GetComponent<Image>().sprite = state ? toggle.GetComponent<BehaviourSprite>()._right : toggle.GetComponent<BehaviourSprite>()._wrong;
    }

    public void ResetLines()
    {
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

        evaluadas = 0;
        correctas = 0;

        StateBtnDelete();
        StateBtnValidar();
        inicio.SetActive(true);
        //foreach (var f in frogs)
        //{
        //    f.e = false;
        //    f.transform.localPosition = f.inicio;
        //}
    }


}
