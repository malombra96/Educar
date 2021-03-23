using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M9A81_subCube : MonoBehaviour
{

    public bool review;
    public List<Sprite> imagenes;

    private void Update()
    {
        if (review)
        {
            GetComponent<Animator>().SetBool("x", true);
        }
    }

    public void ReviewSub()
    {
        print("entre sub");
        review = true;

    }
}
