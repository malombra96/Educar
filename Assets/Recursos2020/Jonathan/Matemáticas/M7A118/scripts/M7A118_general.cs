using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M7A118_general : MonoBehaviour
{
    public int correctas;

    public List<GameObject> fichas1, fichas2, fichas3, fichas4, fichas5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Calificar(int x, bool value) {
        if (x == 1) {
            print(x+" " + value);
            if (value)
            {
                foreach (var f in fichas1) {
                    f.SetActive(true);
                }
            }
            else {
                foreach (var f in fichas1)
                {
                    f.SetActive(false);
                }
            }
        }
        if (x == 2)
        {
            print(x + " " + value);
            if (value)
            {
                foreach (var f in fichas2)
                {
                    f.SetActive(true);
                }
            }
            else
            {
                foreach (var f in fichas2)
                {
                    f.SetActive(false);
                }
            }
        }

        if (x == 3)
        {
            print(x + " " + value);
            if (value)
            {
                foreach (var f in fichas3)
                {
                    f.SetActive(true);
                }
            }
            else
            {
                foreach (var f in fichas3)
                {
                    f.SetActive(false);
                }
            }
        }

        if (x == 4)
        {
            print(x + " " + value);
            if (value)
            {
                foreach (var f in fichas4)
                {
                    f.SetActive(true);
                }
            }
            else
            {
                foreach (var f in fichas4)
                {
                    f.SetActive(false);
                }
            }
        }


        if (x == 5)
        {
            print(x + " " + value);
            if (value)
            {
                foreach (var f in fichas5)
                {
                    f.SetActive(true);
                }
            }
            else
            {
                foreach (var f in fichas5)
                {
                    f.SetActive(false);
                }
            }
        }

    }

    public void ResetAll()
    {
        foreach (var f in fichas5)
        {
            f.SetActive(false);
        }
        foreach (var f in fichas4)
        {
            f.SetActive(false);
        }
        foreach (var f in fichas3)
        {
            f.SetActive(false);
        }
        foreach (var f in fichas2)
        {
            f.SetActive(false);
        }
        foreach (var f in fichas1)
        {
            f.SetActive(false);
        }
    }
}
