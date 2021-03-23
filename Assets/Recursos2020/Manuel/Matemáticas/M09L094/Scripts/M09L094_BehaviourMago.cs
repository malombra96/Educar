using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09L094_BehaviourMago : MonoBehaviour
{
    [Header("Setup Movement")]
    [Range(1,5)] public float speedRun;
    Rigidbody2D _rigibody;
    Animator _animator;

    //AudioSource _audioSource;
    [HideInInspector] public bool _facingRight;
    
    [Header("Setup Jumping")]
    float groundRadius = 0.02f;
    public LayerMask groundLayer;
    [Range(10,100)] public float forceJump;
    public Transform[] groundPoints = new Transform[3];

    public bool _isGrounded;
    bool _jump;

    bool _knock;

    [Header("Prefab Power")] public GameObject _prefabPower;

    float tempIntervalo;
    public float intervalo;


    [Header("Active Movement")] public bool _active;

    [Header("Mobile")]
	string jumpButton = "Jumping";
    string shooting = "Shooting";

    [Header("Setup Audio")]
    public AudioClip _soundWalk;
    public AudioClip _soundJump;

    public AudioClip _soundClimb;

   
    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //_audioSource = GetComponent<AudioSource>();
        _facingRight = true;
        _active = true;
        
    }

    void FixedUpdate()
    {
        if (_active)
        {
             float h,v;

            if(Application.isMobilePlatform)
            {
                h = SimpleInput.GetAxis("Horizontal");
                v = SimpleInput.GetAxis("Vertical");
            }
            else
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }

            
            SetRunning(h, v);
            SetFacingTo(h);
            SetAnimator(Mathf.Abs(h), Mathf.Abs(v));

            _isGrounded = GetIsGrounded();
            

            if (Input.GetKeyDown(KeyCode.UpArrow)  || SimpleInput.GetButtonDown( jumpButton ))
                _jump = true;


            
            Shooting(); 
            SetJumping();
            //SetSounds(h,v);
            //ResetStates();
            _jump = false;
        }
    }

    /// Genera el movimiento del personaje
    void SetRunning(float x,float y)
    {
        _rigibody.velocity = new Vector2(x*speedRun, _rigibody.velocity.y);
    }

    void SetAnimator(float x,float y)
    {
        _animator.SetFloat("isRun",x); 
    }  

    /// Determina la orientacion del personaje de acuerdo a donde se mueve
    void SetFacingTo(float x)
    {
        if(_isGrounded)
        {
            if(x > 0 && !_facingRight || x < 0 && _facingRight)
            {
            _facingRight = !_facingRight;

            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            
            }
        }
    }

    void SetJumping()
    {
        if(_isGrounded && _jump)
        {
            _isGrounded = false;
            _rigibody.AddForce(new Vector2(0,forceJump));
            //_audioSource.PlayOneShot(_soundJump);
            _animator.SetBool("isJump",true);
            
        }
            
    }

    bool GetIsGrounded()
    {
        if(_rigibody.velocity.y <= 0.1f)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position,groundRadius,groundLayer);

                foreach (var collider in colliders)
                {
                    if(collider.gameObject != gameObject)
                    {
                        _animator.SetBool("isJump",false);
                        return true;
                    }
                }  
                        
            }
        }

        return false;
    }

    void Shooting()
    {
        if (tempIntervalo > 0)
        {
            tempIntervalo -= Time.deltaTime;
        }
        else
        {
            if (SimpleInput.GetButtonDown(shooting) || Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(SetAnimationPower());

                if (_facingRight)
                {
                    Vector3 p = transform.position;
                    p.x += 0.1f;
                    GameObject temp = Instantiate(_prefabPower, p, Quaternion.identity, transform.parent);
                    temp.transform.localScale = new Vector3(1, 1, 1);
                    temp.transform.SetSiblingIndex(0);
                    tempIntervalo = intervalo;
                }
                else
                {
                    Vector3 p = transform.position;
                    p.x -= 0.1f;
                    GameObject temp = Instantiate(_prefabPower, p, Quaternion.identity, transform.parent);
                    temp.transform.localScale = new Vector3(-1, 1, 1);
                    temp.transform.SetSiblingIndex(0);
                    tempIntervalo = intervalo;

                }

            }
        }
    }

    IEnumerator SetAnimationPower()
    {
        _animator.SetBool("isPower",true);
        yield return new WaitForSeconds(.5f);
        _animator.SetBool("isPower",false);
    }

    public void KnockRock()
    {
        transform.parent.GetComponent<M09L094_World>().SetLife();
        _animator.SetBool("isKnock",true);
        StartCoroutine(SetKnockAnimation());
    }

    IEnumerator SetKnockAnimation()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("isKnock",false);
    }

    /* void SetSounds(float h,float v)
    {
        if(( Mathf.Abs(h) > 0.2f) && !_audioSource.isPlaying && _isGrounded)
            _audioSource.PlayOneShot(_soundWalk);
        else if(( Mathf.Abs(v) > 0.2f) && !_audioSource.isPlaying)
            _audioSource.PlayOneShot(_soundClimb);
        else if((h==0 && _isGrounded) || (v == 0))
            GetComponent<AudioSource>().Stop(); 
    } */

    public void SetStateBehaviour(bool state)
    {
        if(!state)
        {
            _animator.SetBool("isJump",false);
            _animator.SetBool("isPower",false);
            _animator.SetBool("isKnock",false);
            StopAllCoroutines();
        }   
            
        
        this.enabled = state;
    }

    public void ResetStates()
    {
        //_jump = false;
        _knock = false;

        if(_animator)
        {
            _animator.SetBool("isJump",false);
            _animator.SetBool("isPower",false);
            _animator.SetBool("isKnock",false);
        }

        this.enabled = true; 
        StopAllCoroutines();

        GetComponent<RectTransform>().anchoredPosition = new Vector2(-620,-235);
    }
}
