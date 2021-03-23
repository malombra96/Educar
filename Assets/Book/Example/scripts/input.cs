using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input : MonoBehaviour
{
    public int id = 5;
    public Book book;
    public Vector2 posicionInicial;
    void Start()
    {
        book = FindObjectOfType<Book>();
        posicionInicial = GetComponent<RectTransform>().anchoredPosition;
        id = book.currentPage;
    }

}
