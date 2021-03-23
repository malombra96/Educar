using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaFinanciero_Platform : MonoBehaviour
{
    SistemaFinanciero_Player _BehaviourPlayer;
    PlatformEffector2D _effector;
    float waitTime;
    float v;
    

    void Start()
    {
        _BehaviourPlayer = FindObjectOfType<SistemaFinanciero_Player>();

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
