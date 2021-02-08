using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverNecromancerslair : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject necromancerAttack;
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
        eventScript.button1Txt.text = "A >= 7\nP >= 3\nC >= 2\nBurn lair to the ground.\nAdd <Cyborg> in recovery\nExhaust";
        eventScript.button2Txt.text = "Retutn home\nInsert <Necromancer attack>\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.A >= 7 && broker.P >= 3 && broker.C >= 2)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateUnit(cyborg);

        broker.FinishingEventCardWithoutDraw(gameObject);
    }
    public void Answer2()
    {

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(necromancerAttack);

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCardWithoutDraw(gameObject);
    }
}
