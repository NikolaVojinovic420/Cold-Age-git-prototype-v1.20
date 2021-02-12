using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAttack : MonoBehaviour
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
        eventScript.button1Txt.text = "A >= 8\nFight Aggressively\nExhaust\nAdd Morale and Noise";
        eventScript.button2Txt.text = "A >= 5\nFight defensively\nLose Morale\nAdd Noise";
        eventScript.button3Txt.text = "Game Over";
    }
    void Answer1Update()
    {
        if (broker.A >= 8)
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
        broker.morale++;
        broker.noise++;
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.FinishingEventCard(gameObject);

    }

    public void Answer2()
    {
        broker.SendMarkedIntoRecovering();
        broker.morale--;
        broker.noise++;
        broker.FinishingEventCard(gameObject);
    }

    public void Answer3() => broker.msgBoxGameOver.SetActive(true);

}
