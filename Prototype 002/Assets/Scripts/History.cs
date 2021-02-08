using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    public Transform historyDeck;
    Text text;
    public GameObject msgBoxHistory;
    public Text msgTextHistory;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountDeck();
    }

    void CountDeck() => text.text = $"{historyDeck.childCount}";

    public void SeeDeck()
    {
        if (historyDeck.childCount <= 0)
        {
            msgTextHistory.text = "Empty";
            msgBoxHistory.SetActive(true);
            return;
        }
        msgTextHistory.text = "";
        for (int i = 0; i < historyDeck.childCount; i++)
        {
            msgTextHistory.text += " | " + $"{i + 1} " + historyDeck.GetChild(i).GetComponent<Event>().Name;
        }
        msgBoxHistory.SetActive(true);
    }
}
