using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preparing : MonoBehaviour
{
    public GameObject Recovering;
    public AnswerBroker broker;
    public Transform preparingDeck;
    public Transform vigilantDeck;
    public Transform ShuffleDeck;
    public Transform recoveringDeck;
    public bool shuffled = false;
    Text text;
    public GameObject msgBoxPreparing;
    public Text msgBoxTextPreparing;

    // Start is called before the first frame update
    void Start()
    {
        broker = GameObject.FindWithTag("AnswerCard").GetComponent<AnswerBroker>();
        text = gameObject.GetComponent<Text>();
        ShufflePreparing();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountDeck();
    }

    void CountDeck() => text.text = $"{preparingDeck.childCount}";

    public void SeeDeck()
    {
        if (preparingDeck.childCount <= 0)
        {
            msgBoxTextPreparing.text = "Empty";
            msgBoxPreparing.SetActive(true);
            return;
        }
        msgBoxTextPreparing.text = "";
        for (int i = 0; i < preparingDeck.childCount; i++)
        {
            msgBoxTextPreparing.text += $"{i+1} " + preparingDeck.GetChild(i).GetComponent<Unit>().Name + "\n";
        }
        msgBoxPreparing.SetActive(true);
    }
    
    public void ShufflePreparing()
    {
        if (!shuffled)
        {
            shuffled = true;
            int count = preparingDeck.childCount;
            for (int i = 0; i < count; i++)
                preparingDeck.GetChild(Random.Range(0, preparingDeck.childCount)).SetParent(ShuffleDeck);
            for (int i = 0; i < count; i++)
                ShuffleDeck.GetChild(Random.Range(0, ShuffleDeck.childCount)).SetParent(preparingDeck);
            shuffled = false;
        }
    }
    public void DrawToVigilant()
    {
        if (vigilantDeck.parent.gameObject.GetComponent<Vigilant>().maxHand > vigilantDeck.childCount)
        {
            if (preparingDeck.childCount <= 0)
            {
                Debug.Log($"{Time.time} Shuffled from Recovering to Preparing");
                int count = recoveringDeck.childCount;
                for (int i = 0; i < count; i++)
                    recoveringDeck.GetChild(0).SetParent(preparingDeck);
                ShufflePreparing();
                //check morale and insert none or 2
                if (broker.morale >= 12)
                {
                    Debug.Log("Morale is 12, double draw");
                    Debug.Log($"{Time.time} {preparingDeck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                    preparingDeck.GetChild(0).SetParent(vigilantDeck);
                    if (vigilantDeck.childCount < 10)
                    {
                        Debug.Log($"{Time.time} {preparingDeck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                        preparingDeck.GetChild(0).SetParent(vigilantDeck);
                    }
                    else Debug.Log("Vigilant is at maximum");
                    
                    return;
                }
                if(broker.morale <= 3)
                {
                    Debug.Log("Morale is less then 3 no draw");
                        return;
                }
                Debug.Log($"{Time.time} {preparingDeck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                preparingDeck.GetChild(0).SetParent(vigilantDeck);
            }
            else
            {
                //check morale and insert none or 2
                if (broker.morale >= 12)
                {
                    Debug.Log("Morale is 12, double draw");
                    Debug.Log($"{Time.time} {preparingDeck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                    preparingDeck.GetChild(0).SetParent(vigilantDeck);
                    if (vigilantDeck.childCount < 10)
                    {
                        Debug.Log($"{Time.time} {preparingDeck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                        preparingDeck.GetChild(0).SetParent(vigilantDeck);
                    }
                    else Debug.Log("Vigilant is at maximum");


                    return;
                }
                if (broker.morale <= 3)
                {
                    Debug.Log("Morale is less then 3 no draw");
                    return;
                }
                Debug.Log($"{Time.time} {preparingDeck.GetChild(0).GetComponent<Unit>().Name} inserted into Vigilant");
                preparingDeck.GetChild(0).SetParent(vigilantDeck);
            }
        }
    }
}
