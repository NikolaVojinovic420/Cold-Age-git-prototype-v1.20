using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecruits : MonoBehaviour
{

    Event eventScript;  //use for command of buttons
    AnswerBroker broker; //use for aspect values

    public GameObject youngOne;
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
        eventScript.button1Txt.text = "Welcome\nInsert 2 <Young one> into Vigilant\nAdd Morale\nExhaust";
        eventScript.button2Txt.text = "Pass\nLose Morale\nExhaust";
    }
    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();
        broker.morale++;
        broker.InstatiateUnitOnLocation(youngOne, broker.vigilantDeck.transform);
        broker.InstatiateUnitOnLocation(youngOne, broker.vigilantDeck.transform);
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.SendMarkedIntoRecovering();
        broker.morale--;
        broker.FinishingEventCard(gameObject);
    }

}
