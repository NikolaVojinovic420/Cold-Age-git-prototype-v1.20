using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrough : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject disfigured;
    public GameObject cyborg;

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
        eventScript.button1Txt.text = "A >= 6\nP >= 4\nAdd <Cyborg> into Recovering\nExhaust";
        eventScript.button2Txt.text = "A >= 5\nC >= 2\nAdd <Disfigured> into Recovering\nExhaust";
        eventScript.button3Txt.text = "No draw.\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.A >= 6 && broker.P >= 4)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.A >= 5 && broker.C >= 2)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    public void Answer1()
    {

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateUnit(cyborg);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {


        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateUnit(disfigured);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCardWithoutDraw(gameObject);
    }
}
