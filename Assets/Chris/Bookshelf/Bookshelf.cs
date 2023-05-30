using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    public List<GameObject> bookObjects = new List<GameObject>();
    public List<GameObject> shelfLocations = new List<GameObject>();
    public GameObject bookstackEffect;
    public GameObject IncompleteCandles;
    public GameObject CompleteCandles;

    private bool booksReady = false;
    private bool BookshelfComplete = false;
    private int nextBookIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (bookObjects.Contains(other.gameObject))
        {
            int index = bookObjects.IndexOf(other.gameObject);
            GameObject book = bookObjects[index];
            GameObject shelfLocation = shelfLocations[nextBookIndex];

            book.transform.position = shelfLocation.transform.position;
            book.transform.rotation = shelfLocation.transform.rotation;
            book.GetComponent<Rigidbody>().isKinematic = true;

            // Disable the collider to make the book unmovable
            book.GetComponent<Collider>().enabled = false;

            bookObjects[index] = null;
            nextBookIndex++;

            CheckBooksReady();
        }
        else if (booksReady)
        {
            //bookstackEffect.SetActive(true);
            IncompleteCandles.SetActive(false);
            CompleteCandles.SetActive(true);
            BookshelfComplete = true;
        }
    }

    private void CheckBooksReady()
    {
        foreach (GameObject book in bookObjects)
        {
            if (book != null)
            {
                return;
            }
        }

        booksReady = true;
        // Perform any desired actions when all books are stacked on the bookshelf
    }
}
