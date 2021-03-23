using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M09L094_BehaviourGolem : MonoBehaviour
{
    Animator _animator;
    float tempIntervalo;
    [Header("Intervalo Lanzamiento")] public float intervalo;

    [Header("Prefab Rock")] public GameObject _prefabRock;

    void OnEnable()
    {
        _animator = GetComponent<Animator>();
        ShootingRock();
    }

    public void ShootingRock()
    {
        _animator.SetBool("stateRock", true);
        
        if(gameObject.activeSelf)
            StartCoroutine(SetRock());

    }

    IEnumerator SetRock()
    {
        yield return new WaitForSeconds(1.2f);

        Vector3 p = transform.position;
        p.x += 0.2f;
        GameObject temp = Instantiate(_prefabRock, p, Quaternion.identity, transform.parent);
        temp.transform.SetSiblingIndex(0);
        temp.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 1.7f, ForceMode2D.Impulse);
        temp.name = "Rock";
        _animator.SetBool("stateRock",false);

        yield return new WaitForSeconds(5);
        

        ShootingRock();
    }

    public void DestroyGolem()
    {
        _animator.SetBool("dead",true);
        CancelInvoke();
    }

    public void SetStateBehaviour(bool state)
    {
        if(!state)
        {
            _animator.SetBool("stateRock",false);
            StopAllCoroutines();
        }
    }

    public void ResetStates()
    {   
        if(_animator)
        {
            _animator.SetBool("stateRock",false);
            _animator.SetBool("dead",false);
        }
            
        StopAllCoroutines();
        CancelInvoke("ShootingRock");
    }
}
