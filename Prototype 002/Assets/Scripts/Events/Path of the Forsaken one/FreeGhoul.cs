using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeGhoul : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject cyborg;
    public GameObject helpinghand;


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
        eventScript.button1Txt.text = "A >= 5\nP >= 4\nSave Cyborg.\nInsert <Helping hand>\nAdd <Cyborg>\nExhaust\nAdd Morale and Noise";
        eventScript.button2Txt.text = "Exhaust\nLose Morale";
    }
    void Answer1Update()
    {
        if (broker.A >= 5 && broker.P >= 4)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(helpinghand);
        broker.InstatiateUnit(cyborg);
        broker.morale++;
        broker.noise++;
        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.morale--;
        broker.ReturnMarkedToVigilant(); ;
        broker.FinishingEventCard(gameObject);
    }
}
