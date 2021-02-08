using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighbornDemandTribute : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject warWithHighborn;

    // Start is called before the first frame update
    void Start()
    {
        eventScript = gameObject.GetComponent<Event>();
        broker = GameObject.FindWithTag("AnswerCard").GetComponent<AnswerBroker>();
        ButtonConfiguration();
        SetTextsOfButtons();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (eventScript.active)
        {
            Answer1Update();
            Answer2Update();
        }
    }
    void ButtonConfiguration()
    {
        eventScript.button1.gameObject.SetActive(true);
        eventScript.button2.gameObject.SetActive(true);
        eventScript.button3.gameObject.SetActive(false);
        eventScript.button4.gameObject.SetActive(false);
    }
    void SetTextsOfButtons()
    {
        eventScript.button1Txt.text = "This one will serve.\nSacrifice 1 card.\nNote that only first marked will be sacrificed!";
        eventScript.button2Txt.text = "A >= 5\nWe will never submit!\nInsert <War with Highborn>.\nExhaust event.";
    }

    void Answer1Update()
    {
        if (broker.markedDeck.transform.childCount >=1)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.A >= 5)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }

    public void Answer1()
    {
        Debug.Log($"{Time.time} <{broker.markedDeck.transform.GetChild(0).GetComponent<Unit>().Name}> has been sacrificed.");
        broker.markedDeck.transform.GetChild(0).SetParent(broker.UnitsGarbageCan.transform);

        broker.ReturnMarkedToVigilant();

        broker.FinishingEventCard(gameObject);
    }

    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust
        broker.InstatiateEvent(warWithHighborn);

        broker.SendMarkedIntoRecovering();

        broker.FinishingEventCard(gameObject);
    }
}
