using UnityEngine;
using UnityEngine.UI;

public class ControlRevision : MonoBehaviour
{
    ControlNavegacion _controlNavegacion;
    [Header("Objecto Revision")] public GameObject _revision;
    [Header("State Review")] public bool _state;

    [Header("Sprite Review")]
    
    public Sprite _SpriteDefault;
    public Sprite _SpriteToWordSearch;
    
    [Header("Seleccion que tipo de layout que requiere revisión, adicional a Actividad:")] public BehaviourLayout.TipoLayout _TipoLayoutRevision;
    
    Button _next,_back,_endReview;

    GameObject x;
    int? firstAplico;
    int currentAplico;

    void Start()
    {
        _controlNavegacion = FindObjectOfType<ControlNavegacion>();
        _back = _revision.transform.GetChild(0).GetComponent<Button>();
        _next = _revision.transform.GetChild(1).GetComponent<Button>();
        _endReview = _revision.transform.GetChild(2).GetComponent<Button>();
        _state = false;
        firstAplico = GetIndexAplico();


    }

    void Update()
    {
        if (_state)
        {
            x = _controlNavegacion.GetLayoutActual(); // Get active element

            if (x != null)
            {
                if (x.GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Actividad || x.GetComponent<BehaviourLayout>()._Layout == _TipoLayoutRevision)
                {
                    currentAplico = _controlNavegacion._Layouts.IndexOf(x.GetComponent<BehaviourLayout>());
                    SetObjectRevision(true);
                }
                else
                {
                    SetObjectRevision(false);
                }
                
                SetSprite(x.GetComponent<Manager_WordSearch>());

            }
        }

    }

    void SetObjectRevision(bool state)
    {
        _back.gameObject.SetActive(firstAplico != currentAplico);
        _revision.SetActive(state);
    }

    void SetSprite(bool state) => _revision.GetComponent<Image>().sprite = state? _SpriteToWordSearch : _SpriteDefault;

    public void SetLayoutStateReview()
    {
        foreach (var layout in _controlNavegacion._Layouts)
                layout.GetComponent<BehaviourLayout>()._isEvaluated = true;
    }  

    int? GetIndexAplico()
    {
        for (int i = 0; i < _controlNavegacion._Layouts.Count; i++)
            if (_controlNavegacion._Layouts[i].GetComponent<BehaviourLayout>()._Layout == BehaviourLayout.TipoLayout.Actividad)
                return i;
        
        return null;
    }
}
