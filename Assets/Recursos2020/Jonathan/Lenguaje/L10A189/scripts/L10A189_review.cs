using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10A189_review : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite sprite1, sprite2;
    public Image revision;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (GetComponent<BehaviourLayout>()._isEvaluated)
        {
            revision.sprite = sprite1;
        }
        else {
            revision.sprite = sprite2;
        }
    }
}
