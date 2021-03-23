using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M04A113_BehaviourPlayer : MonoBehaviour
{
    
    [Header("Setup Movement")]
    [Range(1,5)] public float speedRun;
    Rigidbody2D _rigibody;
    Animator _animator;

    //AudioSource _audioSource;
    bool _facingRight;
    
    
    [Header("Setup Jumping")]
    float groundRadius = 0.02f;
    public LayerMask groundLayer;
    [Range(10,100)] public float forceJump;
    public Transform[] groundPoints = new Transform[3];
    public bool _isGrounded;
    bool _jump;

    [Header("Active Movement")] public bool _active;

    [Header("Mobile")]
	string jumpButton = "Jump";

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
        SetBehaviourPlayer(true);
        
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

            if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown( jumpButton ))
                _jump = true;


            SetJumping();
            SetScaleGravity();
            //SetSounds(h,v);
            ResetStates();
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

    void SetScaleGravity() =>  _rigibody.gravityScale = 1;   
       

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

    public void SetBehaviourPlayer(bool state) => _active = state;

    /* void SetSounds(float h,float v)
    {
        if(( Mathf.Abs(h) > 0.2f) && !_audioSource.isPlaying && _isGrounded)
            _audioSource.PlayOneShot(_soundWalk);
        else if(( Mathf.Abs(v) > 0.2f) && !_audioSource.isPlaying && _isClimbed)
            _audioSource.PlayOneShot(_soundClimb);
        else if((h==0 && _isGrounded) || (v == 0 && _isClimbed))
            GetComponent<AudioSource>().Stop(); 
    } */

    void ResetStates()
    {
        _jump = false;
    }

    public void ResetPlayer()
    {
        SetBehaviourPlayer(false);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-334,-244);
        GetComponent<Animator>().enabled = true;

    }
}
