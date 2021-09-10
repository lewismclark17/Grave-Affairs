using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPile : MonoBehaviour
{
    public GameObject book;

    void Start()
    {
        StopHighlight();
    }

    public Item GetBook()
    {
        GameObject newBook = Instantiate(book);
        return newBook.GetComponent<Item>();
    }

    public void Highlight()
    {
        GetComponent<Outline>().enabled = true;
    }

    public void StopHighlight()
    {
        GetComponent<Outline>().enabled = false;
    }
}
