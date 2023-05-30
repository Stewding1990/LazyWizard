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
            GameObject book = bookObjects[index];
            GameObject shelfLocation = shelfLocations[nextBookIndex];

            book.transform.position = shelfLocation.transform.position;
            book.transform.rotation = shelfLocation.transform.rotation;
            book.GetComponent<Rigidbody>().isKinematic = true;

            book.GetComponent<Collider>().enabled = false;

            bookObjects[index] = null;
            nextBookIndex++;

            CheckBooksReady();
        }
        else if (BookshelfComplete)
        {
            //bookstackEffect.SetActive(true);
            IncompleteCandles.SetActive(false);
            CompleteCandles.SetActive(true);
        }
    }

    private void CheckBooksReady()
    {
        filledShelfCount++;

        if (filledShelfCount == shelfLocations.Count)
        {
            BookshelfComplete = true;
            // Perform any desired actions when all three game objects are filled
        }
    }
}
