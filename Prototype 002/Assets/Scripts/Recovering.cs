using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recovering : MonoBehaviour
{
    public Transform recoveringDeck;
    public Transform preparingDeck;
    Text text;
    public GameObject msgBox;
    public Text msgText;

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

    public void SendAllToPreparingAndShuffle()
    {

    }
    void CountDeck() => text.text = $"{recoveringDeck.childCount}";

    public void SeeDeck()
    {
        if (recoveringDeck.childCount <= 0)
        {
            msgText.text = "Empty";
            msgBox.SetActive(true);
        }
        else
        {
            msgText.text = "";
            for (int i = 0; i < recoveringDeck.childCount; i++)
            {
                msgText.text += $"{i + 1} " + recoveringDeck.GetChild(i).GetComponent<Unit>().Name + "\n";
            }
            msgBox.SetActive(true);
        }
    }
}
