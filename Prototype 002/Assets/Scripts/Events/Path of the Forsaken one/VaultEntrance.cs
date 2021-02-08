using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultEntrance : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject enterTheVault;

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
        eventScript.button1Txt.text = "A >= 4\nP >= 3\nWe got this.\nInsert <Enter the vault>\nExhaust";
        eventScript.button2Txt.text = "Calm down. Wait another turn.";
    }
    void Answer1Update()
    {
        if (broker.A >= 4 && broker.P >= 3)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    public void Answer1()
    {
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(enterTheVault);

        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }
}
