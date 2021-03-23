using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M08A125_SpaceShip : MonoBehaviour
{
    public Animator _animator;
    public ManagerDragDrop _managerDrag;

    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
        //_managerDrag = transform.parent.GetComponent<ManagerDragDrop>();
    }

    public void SetAnimation()
    {
        StartCoroutine(DelayAnimation());
    }

    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(.2f);

        int n = 0;

        foreach (var answers in _managerDrag.answers)
        {
            if (answers.Value)
                n++;
        }

        if(n == _managerDrag.answers.Count)
            _animator.SetBool("Fire",true);
    }

    public void ResetSpaceShip()
    {
        _animator.SetBool("Fire",false);
        _managerDrag.ResetDragDrop();
    }
}
