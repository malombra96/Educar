using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08A125_Cohete : MonoBehaviour
{
    public Animator _animator;
    public ManagerInputField _managerInput;

    // Start is called before the first frame update
    void Start()
    {
        /* _animator = GetComponent<Animator>();
        _managerInput = transform.parent.GetComponent<ManagerInputField>(); */
    }

    public void SetAnimation()
    {
        StartCoroutine(DelayAnimation());
    }

    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(.2f);

        int n = 0;

        foreach (var listAnswers in _managerInput._listAnswers)
        {
           for (int i = 0; i < listAnswers.answers.Length; i++)
           {
               if(listAnswers.answers[i])
                    n++;
           } 
        } 

        if(n == _managerInput._listAnswers[0].answers.Length)
            _animator.SetBool("Fire",true);
    }

    public void ResetCohete()
    {
        _animator.SetBool("Fire",false);
        _managerInput.resetAll();
    }
}
