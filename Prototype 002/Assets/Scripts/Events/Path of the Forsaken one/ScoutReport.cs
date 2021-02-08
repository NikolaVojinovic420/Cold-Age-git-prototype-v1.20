using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutReport : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject onRightPath;
    public GameObject freeGhoul;

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
        eventScript.button3.gameObject.SetActive(true);
        eventScript.button4.gameObject.SetActive(false);
    }
    void SetTextsOfButtons()
    {
        eventScript.button1Txt.text = "A >= 6\nC >= 2\nRescue Cyborg.\nInsert <Free ghoul>\nExhaust";
        eventScript.button2Txt.text = "P >= 2\nC >= 2\ngo for Forsaken\nInsert <On right path>\nExhaust";
        eventScript.button3Txt.text = "Still no word from scouts. Wait next turn.";
    }
    void Answer1Update()
    {
        if (broker.A >= 6 && broker.C >= 2)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.P >= 2 && broker.C >= 2)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(freeGhoul);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        Instantiate(onRightPath, broker.HistoryDeck.transform);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }
}
