using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marked : MonoBehaviour
{
    public Transform markedDeck;
    public Transform vigilantDeck;
    public Transform recoveringDeck;
    public int cardXScale;
    int checkForSorting = 0;

    // Start is called before the first frame update
    void Start()
    {
        checkForSorting = markedDeck.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkForSorting != markedDeck.childCount)
            SortMarkedDeck();
    }

    public void AddInMarkedDeck(Transform card)
    {
        Debug.Log($"{Time.time} {card.GetComponent<Unit>().Name} added into Marked from {card.transform.parent.name}.");
        card.SetParent(markedDeck);
        int i = markedDeck.childCount-1;
        card.transform.position = new Vector2(markedDeck.position.x + i * cardXScale, markedDeck.position.y);
    }

    public void SortMarkedDeck()
    {
        for (int i = 0; i < markedDeck.childCount; i++)
            markedDeck.GetChild(i).transform.position = new Vector2(markedDeck.transform.position.x + i * cardXScale, markedDeck.transform.position.y);
    }
}
