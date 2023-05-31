using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    public List<GameObject> bookObjects = new List<GameObject>();
    public List<GameObject> shelfLocations = new List<GameObject>();
    public GameObject bookstackEffect;
    public GameObject IncompleteCandles;
    public GameObject CompleteCandles;

    private int filledShelfCount = 0;
    private bool BookshelfComplete = false;
    private int nextBookIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (bookObjects.Contains(other.gameObject))
        {
            int index = bookObjects.IndexOf(other.gameObject);

            if (index < shelfLocations.Count)
            {
                GameObject book = bookObjects[index];
                GameObject shelfLocation = shelfLocations[index];

                book.transform.position = shelfLocation.transform.position;
                book.transform.rotation = shelfLocation.transform.rotation;
                book.GetComponent<Rigidbody>().isKinematic = true;

                book.GetComponent<Collider>().enabled = false;

                bookObjects[index] = null;
                filledShelfCount++;

                CheckBooksReady();
            }
        }
        else if (BookshelfComplete)
        {
            //bookstackEffect.SetActive(true);
            //IncompleteCandles.SetActive(false);
            //CompleteCandles.SetActive(true);
        }
    }

    private void CheckBooksReady()
    {
        int placedBookCount = 0;

        foreach (GameObject book in bookObjects)
        {
            if (book == null)
            {
                placedBookCount++;
            }
        }

        if (placedBookCount == shelfLocations.Count)
        {
            BookshelfComplete = true;
            // Perform any desired actions when all books are placed on the shelf
            IncompleteCandles.SetActive(false);
            CompleteCandles.SetActive(true);
        }
    }


}
