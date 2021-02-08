using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheVault : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject forsaken;
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
        eventScript.button1Txt.text = "A >= 12\nFight security drones.\nAdd <Forsaken>\nExhaust";
        eventScript.button2Txt.text = "P >= 4\nC >= 2\nBypass security system.\nAdd <Forsaken\nExhaust>";
        eventScript.button3Txt.text = "It's more than we can handle\nInsert <Dispair>\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.A >= 12)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.P >= 4 && broker.C >= 2)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateUnit(forsaken);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateUnit(forsaken);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(dispair);

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }
}
