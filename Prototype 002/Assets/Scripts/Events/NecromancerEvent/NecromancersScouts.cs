using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancersScouts : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject necromancerIsAwareOfUs;
    public GameObject necromancersLair;

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
            Answer4Update();
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
        eventScript.button1Txt.text = "C >= 2\nMisdirect them.\nAdd Morale";
        eventScript.button2Txt.text = "A >= 5\nKill those ghouls\nAdd Noise\nGo for Necromancer's lair\nExhaust";
        eventScript.button3Txt.text = "Let them inform their master.\nInsert <Necromancer is aware of us>\nExhaust";
        eventScript.button4Txt.text = "If 2 <Cyborg> in Vigilant they will ignore us\nLose Noise";
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
    void Answer4Update()
    {
        if (CheckCyborgs() > 1)
            eventScript.button4.interactable = true;
        else eventScript.button4.interactable = false;
    }
    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();
        broker.morale++;
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.SendMarkedIntoRecovering();
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(necromancersLair);
        broker.FinishingEventCard(gameObject);
        broker.noise++;
    }
    public void Answer3()
    {
     
        gameObject.GetComponent<Event>().ExhaustableTriggerEvent = true; //exhaust event
        broker.InstatiateEvent(necromancerIsAwareOfUs);

        broker.ReturnMarkedToVigilant();
        broker.FinishingEventCard(gameObject);
    }
    public void Answer4()
    {
        broker.ReturnMarkedToVigilant();
        broker.noise--;
        broker.FinishingEventCard(gameObject);
    }
    int CheckCyborgs()
    {
        int j = 0;
        for (int i = 0; i < broker.vigilantDeck.transform.childCount; i++)
            if (broker.vigilantDeck.transform.GetChild(i).gameObject.GetComponent<Unit>().Name == "Cybor")
                j++;
        for (int i = 0; i < broker.markedDeck.transform.childCount; i++)
            if (broker.markedDeck.transform.GetChild(i).gameObject.GetComponent<Unit>().Name == "Cybor")
                j++;

        return j;
    }

}
