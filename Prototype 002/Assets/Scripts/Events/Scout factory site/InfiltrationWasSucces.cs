using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiltrationWasSucces : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject experience;
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
        eventScript.button1Txt.text = "P >= 5 or C >= 3\nAdd TWO <Experience> into History\nExhaust";
        eventScript.button2Txt.text = "Insert <Dispair> into History.\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.P >= 5 || broker.C >= 3)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(experience);
        broker.InstatiateEvent(experience);

        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.ReturnMarkedToVigilant();

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(dispair);

        broker.FinishingEventCard(gameObject);
    }
}
