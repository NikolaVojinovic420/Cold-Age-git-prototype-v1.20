using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOfTheForsakenOne : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject onRightPath;
    public GameObject detour;

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
        eventScript.button1Txt.text = "A >= 8\nP >= 4\nC >= 3\nSend big campaign\nInsert <On right path>\nExhaust";
        eventScript.button2Txt.text = "A >= 6\nP >= 3\nSend campaign\nInsert <Detour>\nExhaust";
        eventScript.button3Txt.text = "Those are just rumors.\nNo draw.\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.A >= 8 && broker.P >= 4 && broker.C >= 3)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.A >= 6 && broker.P >= 3)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(onRightPath);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        Instantiate(detour, broker.HistoryDeck.transform);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCardWithoutDraw(gameObject);
    }
}
