using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancersScouts : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject necromancerIsAwareOfUs;

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
        eventScript.button1Txt.text = "C >= 2\nMisdirect them.";
        eventScript.button2Txt.text = "A >= 5\nKill those ghouls";
        eventScript.button3Txt.text = "Let them inform their master.\nInsert <Necromancer is aware of us>\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.C >= 2)
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
        broker.SendMarkedIntoRecovering();

        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.SendMarkedIntoRecovering();

        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
     
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(necromancerIsAwareOfUs);

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }

}
