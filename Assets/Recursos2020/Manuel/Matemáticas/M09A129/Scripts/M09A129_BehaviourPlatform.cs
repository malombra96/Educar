using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M09A129_BehaviourPlatform : MonoBehaviour
{
    M09A129_BehaviourPlayer _BehaviourPlayer;
    PlatformEffector2D _effector;
    public float waitTime;
    public float v;
    

    void Start()
    {
        _BehaviourPlayer = FindObjectOfType<M09A129_BehaviourPlayer>();

        _effector = GetComponent<PlatformEffector2D>();
        _effector.rotationalOffset = 0f;
        waitTime = .5f;
    }

    void Update()
    {

        if (Application.isMobilePlatform)
            v = SimpleInput.GetAxis("Vertical");
        else
            v = Input.GetAxis("Vertical");

        if(v==0)
           waitTime = .5f;

        if(v<0) //&& _BehaviourPlayer._isClimbed
        {
            if (waitTime <= 0)
            {
                _effector.rotationalOffset = 180f;
                waitTime = .5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        
         
        if(v>0) //&& _BehaviourPlayer._isClimbed
            _effector.rotationalOffset = 0f;

        
    }
}
