using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5A257ManagerJuego : MonoBehaviour
{
    ControlAudio controlAudio;
    ControlNavegacion controlNavegacion;
    ControlPuntaje controlPuntaje;

    public bool cambioPoscion;
    public float tiempoCambio;
    public List<RectTransform> newRectTransforms;
    public Animator animatorPersonaje;
    public Animator enemigo;
    public Button comenzar;
    public GameObject placa;    
    public List<L5A257ManagerJuego> managerJuegos;
    public List<Toggle> Toggles;
    public List<Toggle> personajes;
    RectTransform target;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();

        foreach (var toggle in Toggles)
            toggle.onValueChanged.AddListener(delegate { StartCoroutine(seleccionToggle()); });

        if (comenzar)
        {
            comenzar.onClick.AddListener(definirPersonaje);
            comenzar.interactable = false;
            foreach (var personaje in personajes)
                personaje.onValueChanged.AddListener(delegate { seleccionAvatar(); });            
        }
    }   

    //seleccion personaje
    void definirPersonaje()
    {
        foreach(var toggle in personajes)
        {
            if (toggle.isOn)
            {
                controlAudio.PlayAudio(0);
                animatorPersonaje.runtimeAnimatorController = toggle.GetComponent<L5A257AnimatorPersonaje>().animator;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        foreach (var manager in managerJuegos)
            manager.animatorPersonaje.runtimeAnimatorController = animatorPersonaje.runtimeAnimatorController;
    }
    void seleccionAvatar()
    {
        controlAudio.PlayAudio(0);
        int i = 0;
        foreach(var personaje in personajes)
        {
            if (personaje.isOn)
            {
                i++;
                personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._selection;
            }
            else
                personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._default;
        }
        comenzar.interactable = (i > 0);
    }
    private void Update()
    {
        if (target)
        {
            if (Vector2.Distance(animatorPersonaje.GetComponent<RectTransform>().anchoredPosition, target.anchoredPosition) > 0.1f)
            {
                animatorPersonaje.GetComponent<RectTransform>().anchoredPosition =
                    Vector2.MoveTowards(animatorPersonaje.GetComponent<RectTransform>().anchoredPosition, target.anchoredPosition, 5);
            }
            else
            {
                if (newRectTransforms.Count == 1)
                {
                    target = null;
                    animatorPersonaje.Rebind();
                }
                else if (i < newRectTransforms.Count - 1)
                {
                    i++;
                    target = newRectTransforms[i];
                }
                else
                {
                    target = null;
                    animatorPersonaje.Rebind();
                }
            }                
        }
    }
    IEnumerator seleccionToggle()
    {
        bool right = false;
        controlAudio.PlayAudio(0);

        foreach (var toggle in Toggles)
        {
            toggle.interactable = false;
            if (toggle.isOn)
                toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._selection;
        }
        yield return new WaitForSeconds(1);

        foreach (var toggle in Toggles)
        {
            if (toggle.isOn)
            {
                right = toggle.GetComponent<L5A257Toggle>().esCorrecto;

                toggle.GetComponent<Image>().sprite = right ? toggle.GetComponent<BehaviourSprite>()._right : toggle.GetComponent<BehaviourSprite>()._wrong;
                placa.GetComponent<Image>().sprite = right ? placa.GetComponent<BehaviourSprite>()._right : placa.GetComponent<BehaviourSprite>()._wrong;

                if (right)
                    controlPuntaje.IncreaseScore();

                controlAudio.PlayAudio(right ? 1 : 2);
                break;
            }
        }
        
        if (!cambioPoscion)
        {
            if (right)
            {                
                animatorPersonaje.Play("Ataque");
                StartCoroutine(segundaAnimacion(enemigo, animatorPersonaje));
            }
            else
            {
                enemigo.Play("Ataque");
                StartCoroutine(segundaAnimacion(animatorPersonaje, enemigo));
            }
            tiempoCambio = animatorPersonaje.runtimeAnimatorController.animationClips.Length + enemigo.runtimeAnimatorController.animationClips.Length;
            tiempoCambio = 4;
        }
        else
        {
            if (right)
            {
                if(newRectTransforms.Count>1)
                    animatorPersonaje.Play("Saltar");
                else
                    animatorPersonaje.Play("Correr");

                target = newRectTransforms[i];
            }
            else
                animatorPersonaje.Play("Muerte");
            
            tiempoCambio = animatorPersonaje.runtimeAnimatorController.animationClips.Length;
        }
        controlNavegacion.Forward(tiempoCambio);
    }
    IEnumerator segundaAnimacion(Animator A,Animator b)
    {
        yield return new WaitForSeconds(1.5f);
        A.Play("Muerte");
        b.Rebind();
    }
    public void revision()
    {
        animatorPersonaje.gameObject.SetActive(false);
        if(enemigo)
            enemigo.gameObject.SetActive(false);

        if (comenzar)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }        
    }
    public void resetAll()
    {
        i = 0;
        animatorPersonaje.runtimeAnimatorController = null;
        placa.GetComponent<Image>().sprite = placa.GetComponent<BehaviourSprite>()._default;
        foreach (var toggle in Toggles)
        {
            toggle.isOn = false;
            toggle.interactable = true;
            toggle.GetComponent<Image>().sprite = toggle.GetComponent<BehaviourSprite>()._default;            
        }
        
        if (comenzar)
        {
            comenzar.interactable = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);

            foreach (var personaje in personajes)
            {
                personaje.isOn = false;
                personaje.GetComponent<Image>().sprite = personaje.GetComponent<BehaviourSprite>()._default;
            }
        }       
    }
}
