using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInputs : MonoBehaviour
{
    public input[] inputs;
    private Book book;
    void Start()
    {
        book = FindObjectOfType<Book>();
    }

    public void setActive() {
        inputs = FindObjectsOfType<input>();
        foreach (var input in inputs)
        {
            input.gameObject.SetActive(false);
        }
    }

    public void endPage()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].gameObject.SetActive( inputs[i].id == book.currentPage );
        }
    }
}
