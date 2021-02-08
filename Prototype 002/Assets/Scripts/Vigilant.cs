using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vigilant : MonoBehaviour
{
    public GameObject preparing;
    public Transform prepDeck;
    public Transform recovDeck;
    public Transform markedDeck;
    public Transform vigilantDeck;
    public int startHandCount;
    public int maxHand;
    public int cardXScale;
    int checkForSorting;

    // Start is called before the first frame update
    void Start()
    {
        checkForSorting = vigilantDeck.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkForSorting != vigilantDeck.childCount)
            SortVigilantDeck();
    }

    public void AddInVigilantDeck(Transform card)
    {
        Debug.Log($"{Time.time} {card.GetComponent<Unit>().Name} added into Vigilant from {card.transform.parent.name}.");
        card.SetParent(vigilantDeck);
        int i = vigilantDeck.childCount-1;
        card.transform.position = new Vector2(vigilantDeck.position.x + i * cardXScale, vigilantDeck.position.y);
    }

    public void SortVigilantDeck()
    {
        for (int i = 0; i < vigilantDeck.childCount; i++)
        {
            vigilantDeck.GetChild(i).transform.position = new Vector2(vigilantDeck.transform.position.x + i * cardXScale, vigilantDeck.transform.position.y);
        }
    }
            

    public void StartingHand()
    {
        for (int i = 0; i < startHandCount-1; i++) //considering that u pull one with preparing event
            preparing.GetComponent<Preparing>().DrawToVigilant();
    }

}
