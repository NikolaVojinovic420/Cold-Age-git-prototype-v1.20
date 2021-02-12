using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaversAttention : MonoBehaviour
{
    Event eventScript;  //use for command of buttons
    AnswerBroker broker; //use for aspect values


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
        eventScript.button1Txt.text = "A >= 9\nGive them hell!\nExhaust.\nAdd Morale";
        eventScript.button2Txt.text = "A >= 5\nSkirmish. Kill random marked <Unit>.\nExhaust.\nLose Morale";
        eventScript.button3Txt.text = "Submit";
    }

    void Answer1Update()
    {
        if (broker.A >= 9)
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
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        int random = Random.Range(0, broker.markedDeck.transform.childCount - 1);
        Debug.Log($"{Time.time} <{broker.markedDeck.transform.GetChild(random).GetComponent<Unit>().Name}> unit has been killed.");
        broker.markedDeck.transform.GetChild(random).SetParent(broker.UnitsGarbageCan.transform);
        broker.morale--;
        broker.SendMarkedIntoRecovering();

        broker.FinishingEventCard(gameObject);
    }
    public void Answer3() => broker.msgBoxGameOver.SetActive(true);
}
