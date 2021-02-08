using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterGroupOfFeralDisfigured : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject fightTrough;
    public GameObject trap;
    public GameObject infiltration;

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
        eventScript.button1Txt.text = "Mark 3 units.\nIf A highest\nInsert <Fight trough> into History\nExhaust";
        eventScript.button2Txt.text = "Mark 3 units.\nIf P highest\nInsert <We went into a trap> into History\nExhaust";
        eventScript.button3Txt.text = "Mark 3 units or TWO <Disfigured>.\nIf C highest or TWO <Disfigured> marked\n Insert <Infiltration successed> into History\nExhaust";
        eventScript.button4Txt.text = "No draw.\nExhaust";
    }
    void Answer1Update()
    {
        if (ReturnHighestA())
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (ReturnHighestP())
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    void Answer3Update()
    {
        if (ReturnHighestC())
            eventScript.button3.interactable = true;
        else eventScript.button3.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(fightTrough);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(trap);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(infiltration);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer4()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCardWithoutDraw(gameObject);
    }
    bool ReturnHighestA()
    {       
        if (broker.markedDeck.transform.childCount >= 3 && broker.A >= broker.P && broker.A >= broker.C)
            return true;
        return false;
    }
    bool ReturnHighestP()
    {
        if (broker.markedDeck.transform.childCount >= 3 && broker.P >= broker.A && broker.P >= broker.C)
            return true;
        return false;
    }
    bool ReturnHighestC()
    {
        int dis = 0;
        for (int i = 0; i < broker.markedDeck.transform.childCount; i++)
            if (broker.markedDeck.transform.GetChild(i).GetComponent<Unit>().Name == "Disfigured")
                dis++;
        if (dis >= 2 || (broker.markedDeck.transform.childCount >= 3 && broker.C >= broker.A && broker.C >= broker.P))
            return true;
        return false;
    }
}
