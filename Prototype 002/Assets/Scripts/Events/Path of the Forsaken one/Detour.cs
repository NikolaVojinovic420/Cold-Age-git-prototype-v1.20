using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detour : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject doNotRush;
    public GameObject nowWhat;
    public GameObject intelGathering;
    public GameObject dispair;

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
            Answer3Update();
        }
    }
    void ButtonConfiguration()
    {
        eventScript.button1.gameObject.SetActive(true);
        eventScript.button2.gameObject.SetActive(true);
        eventScript.button3.gameObject.SetActive(true);
        eventScript.button4.gameObject.SetActive(true);
    }
    void SetTextsOfButtons()
    {
        eventScript.button1Txt.text = "P >= 4\nRegroup.\nInsert <Don't rush>\nExhaust";
        eventScript.button2Txt.text = "A >= 5\nRush.\nInsert <Now what?>\nExhaust";
        eventScript.button3Txt.text = "A >= 5\nC >= 2\nGo forward.\nInsert <Intel gathering>\nExhaust";
        eventScript.button4Txt.text = "No draw.\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.P >= 4)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.A >= 5)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    void Answer3Update()
    {
        if (broker.A >= 5 && broker.C >= 2)
            eventScript.button3.interactable = true;
        else eventScript.button3.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(doNotRush);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(nowWhat);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(intelGathering);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer4()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCardWithoutDraw(gameObject);

    }
}
