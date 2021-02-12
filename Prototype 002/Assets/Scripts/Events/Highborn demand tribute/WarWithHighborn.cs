using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarWithHighborn : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject HighbornDemandTribute;

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
            Answer3Update();
        }
    }
    void ButtonConfiguration()
    {
        eventScript.button1.gameObject.SetActive(true);
        eventScript.button2.gameObject.SetActive(true);
        eventScript.button3.gameObject.SetActive(true);
        eventScript.button4.gameObject.SetActive(true);
    }
    void SetTextsOfButtons()
    {
        eventScript.button1Txt.text = "Try diplomacy.\nSacrifice first 2 from marked.\nInsert <Highborn demand tribue>.\nExhaust event.\nLose Noise and Morale";
        eventScript.button2Txt.text = "A >= 6\nKeep fighting.\nAdd Noise";
        eventScript.button3Txt.text = "A >= 12\nP>=5\nC>=3\nWe will crush you... TO GLORY!\nWin game.";
        eventScript.button4Txt.text = "Submit";
    }
    void Answer1Update()
    {
        if (broker.markedDeck.transform.childCount >= 2)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.A >= 6)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }

    void Answer3Update()
    {
        if (broker.A >= 12 && broker.P >= 5 && broker.C >= 3)
            eventScript.button3.interactable = true;
        else eventScript.button3.interactable = false;
    }

    public void Answer1()
    {
        Debug.Log($"{Time.time} <{broker.markedDeck.transform.GetChild(0).GetComponent<Unit>().Name}> " +
            $"and <{broker.markedDeck.transform.GetChild(1).GetComponent<Unit>().Name}> " +
            $"will serve us well.");

        Debug.Log($"{Time.time} {broker.markedDeck.transform.GetChild(0).gameObject.GetComponent<Unit>().Name} has been sacrificed.");
        broker.markedDeck.transform.GetChild(0).SetParent(broker.UnitsGarbageCan.transform);
        Debug.Log($"{Time.time} {broker.markedDeck.transform.GetChild(0).gameObject.GetComponent<Unit>().Name} has been sacrificed.");
        broker.markedDeck.transform.GetChild(0).SetParent(broker.UnitsGarbageCan.transform);

        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(HighbornDemandTribute);
        broker.morale--;
        broker.noise--;
        broker.ReturnMarkedToVigilant();

        broker.FinishingEventCard(gameObject);
    }

    public void Answer2()
    {
        broker.SendMarkedIntoRecovering();
        broker.noise++;
        broker.FinishingEventCard(gameObject);
    }

    public void Answer3() => broker.msgBoxVictory.SetActive(true);

    public void Answer4() => broker.msgBoxGameOver.SetActive(true);

}
