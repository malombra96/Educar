using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08L029_BehaviourCarPlayer : MonoBehaviour
{
    M08L029_BehaviourTrail _ManagerTrail;

    [Header("Setup Movement")]
    [Range(1,5)] public float speedMovement;
    Rigidbody2D _rigibody;
    Animator _animator;

    [Header("Active Movement")] public bool _active;

    [Header("Mobile")] public GameObject _arrows;

    
    // Start is called before the first frame update
    void Start()
    {
        _ManagerTrail = FindObjectOfType<M08L029_BehaviourTrail>();
        _active = true;
        _rigibody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _arrows.SetActive(Application.isMobilePlatform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_active)
        {
            float h;
            h = Application.isMobilePlatform? SimpleInput.GetAxis("Horizontal") : Input.GetAxis("Horizontal");

            Limites(GetComponent<RectTransform>().anchoredPosition);
            SetMovement(h);
        }
    }
    
     /// Genera el movimiento del automovil
    void SetMovement(float x) => _rigibody.velocity = new Vector2(x*speedMovement, 0);

    void Limites(Vector2 anchoredPosition)
    {
        if (anchoredPosition.x < -240)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-240,anchoredPosition.y);              
            
        if (anchoredPosition.x > 70f)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(70,anchoredPosition.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name.Contains("Tunnel"))
        {
            _ManagerTrail.SetInput(int.Parse(col.name.Split('_')[1]),true);
        }
        else
        {
            _animator.SetBool("shock",true);
            _ManagerTrail._energy.fillAmount-=0.1f;
            
            if(_ManagerTrail._energy.fillAmount <= 0)
                _ManagerTrail.EndGame();
            else
                StartCoroutine(IdleState());
        }
    }

    IEnumerator IdleState()
    {
        yield return new WaitForSeconds(2);
        _animator.SetBool("shock",false);

    }

    public void ResetCar()
    {
        if(_animator)
            _animator.SetBool("shock",false);
    }

}
