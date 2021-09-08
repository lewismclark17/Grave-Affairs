using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPile : MonoBehaviour
{
    public GameObject book;

    public Item GetBook()
    {
        GameObject newBook = Instantiate(book);
        return newBook.GetComponent<Item>();
    }
}
