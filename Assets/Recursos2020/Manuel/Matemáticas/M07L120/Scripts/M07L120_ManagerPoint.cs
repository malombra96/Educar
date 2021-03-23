using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M07L120_ManagerPoint : MonoBehaviour
{
    [HideInInspector] public ControlAudio _controlAudio;
    [HideInInspector] public ControlNavegacion _controlNavegacion;
    [HideInInspector] public ControlPuntaje _controlPuntaje;
    
    [HideInInspector] [Header("Vector Points")] public List<Vector2> _positions;
    [HideInInspector] [Header("Coordenadas")] public List<Vector2> _coordenadas;

    [Header("Point")] public M07L120_BehaviourDragPoint _point;

    [Header("Line")] public Image _line;

    [Header("Text")] 
    public Text X;
    public Text Y;
    [HideInInspector] public List<float> difference;
    float delta = 69.5f;
    [Header("Right Point")] public Vector2 _rightPoint;

    [Header("States Color")]

    public Color32 _default;
    public Color32 _selection;
    public Color32 _right;
    public Color32 _wrong;

    [Header("Validar BTN")] public Button _validar;

    void Awake()
    {
        _controlAudio = FindObjectOfType<ControlAudio>();
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _controlPuntaje = FindObjectOfType<ControlPuntaje>();

        _validar.onClick.AddListener(Validar);
    }

    void Start()
    {
        InicializateManager();

        RectTransform _rect = _point.GetComponent<RectTransform>();
        
        for (int c = -6; c <= 8; c++)
        {
            for (int r = -4; r <= 4; r++)
            {
                Vector2 v = new Vector2(_rect.anchoredPosition.x + (delta*c),_rect.anchoredPosition.y + (delta*r));
                Vector2 p = new Vector2(c,r);
                _positions.Add(v);
                _coordenadas.Add(p);
            }
        }
    }
    
    void InicializateManager()
    {
        X.text = "0"; Y.text = "0";
        
        _point.GetComponent<Image>().color = _default;
        _line.color = Color.black;
        _point.GetComponent<M07L120_BehaviourDragPoint>().enabled = true;
        _point._current = Vector2.zero;
        _point.GetComponent<RectTransform>().anchoredPosition = _point._default;
        _validar.interactable = false;
        
    }

    void Validar()
    {
        _validar.interactable = false;
        _point.GetComponent<M07L120_BehaviourDragPoint>().enabled = false;

        _point.GetComponent<Image>().color = (_point._current == _rightPoint)? _right : _wrong;
        _line.color = (_point._current == _rightPoint)? _right : _wrong;

        _controlAudio.PlayAudio(_point._current == _rightPoint? 1 : 2);
        _controlPuntaje.IncreaseScore(_point._current == _rightPoint? 1 : 0);
        _controlNavegacion.Forward(2);
    }

    public void ResetManagerPoint()
    {
        InicializateManager();
    }
}
