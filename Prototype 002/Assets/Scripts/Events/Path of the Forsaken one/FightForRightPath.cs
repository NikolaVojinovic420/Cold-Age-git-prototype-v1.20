using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightForRightPath : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject onRightpath;
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
        eventScript.button1Txt.text = "A >= 5\nInsert <On right path>\nAdd <Experience>\nExhaust\nAdd Morale";
        eventScript.button2Txt.text = "Insert <Dispair>\nExhaust";
    }
    void Answer1Update()
    {
        if (broker.A >= 5)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(onRightpath);
        broker.InstatiateEvent(experience);
        broker.morale++;
        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(dispair);

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }
}
