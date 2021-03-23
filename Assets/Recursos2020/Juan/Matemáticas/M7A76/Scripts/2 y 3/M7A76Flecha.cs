using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M7A76Flecha : MonoBehaviour
{
    void destruir()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
