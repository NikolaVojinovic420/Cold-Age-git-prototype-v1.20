using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispair : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject youngOne;
    public GameObject guard;
    bool foundGuard = false;

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
            Answer2Update();
            Answer3Update();
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
        eventScript.button1Txt.text = "Sacrifice random card from vigilant.\nExhaust\nLose Morale";
        eventScript.button2Txt.text = "Sacrifice <Guard>\nAdd <Young one>\nExhaust\nLose Morale";
        eventScript.button3Txt.text = "P >= 3\nC >= 2\nLeadership\nExhaust";
    }
    void Answer2Update()
    {
        if (broker.FindAndReturnUnit(guard, broker.vigilantDeck) != null && !foundGuard)
        {
            eventScript.button2.interactable = true;
            foundGuard = true;
        }
        if (broker.FindAndReturnUnit(guard, broker.vigilantDeck) == null)
            eventScript.button2.interactable = false;         
    }
    void Answer3Update()
    {
        if (broker.P >= 3 && broker.C >= 2)
            eventScript.button3.interactable = true;
        else eventScript.button3.interactable = false;
    }
    public void Answer1()
    {
        broker.ReturnMarkedToVigilant();

        int random = Random.Range(0, broker.vigilantDeck.transform.childCount - 1);
        Debug.Log($"{Time.time} <{broker.vigilantDeck.transform.GetChild(random).GetComponent<Unit>().Name}> has been sacrificed.");
        broker.vigilantDeck.transform.GetChild(random).SetParent(broker.UnitsGarbageCan.transform);
        broker.morale--;
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.ReturnMarkedToVigilant();
        Debug.Log($"{Time.time} Guard destroyed, Young one instantiated into Recovering.");
        Destroy(broker.FindAndReturnUnit(guard, broker.vigilantDeck));
        Instantiate(youngOne, broker.recoveringDeck.transform);
        broker.morale--;
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCard(gameObject);
    }

    
}
