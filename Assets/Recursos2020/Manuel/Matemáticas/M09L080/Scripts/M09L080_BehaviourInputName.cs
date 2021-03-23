using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class M09L080_BehaviourInputName : MonoBehaviour,IPointerClickHandler
{
    M09L080_ManagerHelps _ManagerHelps;
    ControlAudio _controlAudio;
    Animator _animator;
    [Header("Key Mobile")] public GameObject _keyPad;

    [Header("Text Name Instructions")] public Text _nameFriend;

    void Start()
    {

        _ManagerHelps = FindObjectOfType<M09L080_ManagerHelps>();
        _controlAudio = FindObjectOfType<ControlAudio>();
        _animator = _keyPad.GetComponent<Animator>();

        _keyPad.GetComponent<BehaviourKeyBoard>()._InputName = GetComponent<InputField>().textComponent;

        if(Application.isMobilePlatform)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-240,288); 
        else
            GetComponent<RectTransform>().anchoredPosition = new Vector2(320,-131);
    }


    public void Update()
    {
        if (Application.isMobilePlatform)
        {
            GetComponent<InputField>().text = _keyPad.GetComponent<BehaviourKeyBoard>()._name;   
            _ManagerHelps.GetNameFriend(GetComponent<InputField>().text);
            _nameFriend.text = GetComponent<InputField>().text;
        }
            
            
    }
 
    IEnumerator DelayForDisableAnimator()
    {
        yield return new WaitForSeconds(7);
        _animator.enabled = false;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Application.isMobilePlatform)
        {
            _controlAudio.PlayAudio(0);
            _keyPad.SetActive(true);

            _animator.enabled = true;
            _animator.Play("KeyBoard_in");
            StartCoroutine(DelayForDisableAnimator());
        }
    }
}
