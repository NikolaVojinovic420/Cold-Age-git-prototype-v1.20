using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWasting : MonoBehaviour
{
    Event eventScript;  //use for command of buttons
    AnswerBroker broker; //use for aspect values

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
        eventScript.button1Txt.text = "C >= 1\nExhaust";
        eventScript.button2Txt.text = "Insert <Dispair>\nExhaust";
    }

    void Answer1Update()
    {
        if (broker.C >= 1)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    public void Answer1()
    {


        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.InstatiateEvent(dispair);

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }
}

