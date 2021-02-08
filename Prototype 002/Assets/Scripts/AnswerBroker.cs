using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnswerBroker : MonoBehaviour
{
    //trigger here if needed
    public GameObject selector;
    public GameObject markedCounter;
    public GameObject vigilantDeck;
    public GameObject vigilant;
    public GameObject markedDeck;
    public GameObject marked;
    public GameObject preparingDeck;
    public GameObject preparing;
    public GameObject recoveringDeck;
    public GameObject recovering;
    public GameObject FutureDeck;
    public GameObject Future;
    public GameObject HistoryDeck;
    public GameObject History;
    public GameObject EventGarbageCan;
    public GameObject UnitsGarbageCan;
    public GameObject msgBoxGameOver;
    public GameObject msgBoxVictory;
    public int A;
    public int P;
    public int C;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackAspectValues();
    }


    void TrackAspectValues()
    {
        A = markedCounter.GetComponent<SumMarked>().a;
        P = markedCounter.GetComponent<SumMarked>().p;
        C = markedCounter.GetComponent<SumMarked>().c;
    }

    public void MoveToGarbage(GameObject obj) => obj.transform.SetParent(EventGarbageCan.transform);

    public void FinishingEventCard(GameObject card) //use on the end of specific event card on the end of answer method
    {
        PutEventInHistoryOrExhaust(card);
        Future.GetComponent<Future>().DrawNextEventCard();
    }
    public void FinishingEventCardWithoutDraw(GameObject card)
    {
        PutEventInHistoryOrExhaust(card); //just exhaust this event 
        Future.GetComponent<Future>().DrawNextEventCardWithoutVigilant(); //just pull event without unit to prevent prevent next draw
    }

    public void PutEventInHistoryOrExhaust(GameObject card)
    {
        card.GetComponent<Event>().active = false;
        if (card.GetComponent<Event>().ExhaustableTriggerEvent)
        {
            card.transform.SetParent(EventGarbageCan.transform);
            Debug.Log($"{Time.time} <{card.GetComponent<Event>().Name}> moved into Garbage/Exhausted");
        }

        else
        {
            card.transform.SetParent(HistoryDeck.transform);
            Debug.Log($"{Time.time} <{card.GetComponent<Event>().Name}> moved into History");
        }
        
    }

    public void SendMarkedIntoRecovering()
    {
        int count = markedDeck.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"{Time.time} {markedDeck.transform.GetChild(0).GetComponent<Unit>().Name} sended into Recovering");
            markedDeck.transform.GetChild(0).SetParent(recoveringDeck.transform);          
        }
           
    }
    
    public void ReturnMarkedToVigilant()
    {
        int count = markedDeck.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"{Time.time} {markedDeck.transform.GetChild(0).GetComponent<Unit>().Name} moved from Marked into Vigilant");
            markedDeck.transform.GetChild(0).SetParent(vigilantDeck.transform);          
        }            
    }
    public void RestartGame() => SceneManager.LoadScene(0);
    public GameObject FindAndReturnUnit(GameObject card, GameObject location)
    {
        for (int i = 0; i <location.transform.childCount; i++)
            if (location.transform.GetChild(i).GetComponent<Unit>().Name == card.GetComponent<Unit>().Name)
                return location.transform.GetChild(i).gameObject;
        return null;
    }
    public void InstatiateEvent(GameObject eventCard)
    {
        GameObject newCard = Instantiate(eventCard, HistoryDeck.transform);
        Debug.Log($"{Time.time} <{newCard.GetComponent<Event>().Name}> has been instatieted into History.");
    }
    public void InstatiateUnit(GameObject unitCard)
    {
        GameObject newCard = Instantiate(unitCard, recoveringDeck.transform);
        Debug.Log($"{Time.time} *{newCard.GetComponent<Unit>().Name}* has been instatiated into Recovering");
    }

}
