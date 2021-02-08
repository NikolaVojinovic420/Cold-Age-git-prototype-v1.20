using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatmantExperimentEvent : MonoBehaviour
{
    public Event eventScript;  //use for command of buttons
    public AnswerBroker broker; //use for aspect values



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
        if(eventScript.active)
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
        eventScript.button1Txt.text = "C >= 2\nChose TWO from Preparing";
        eventScript.button2Txt.text = "Failed experiment! Discard 2 random card from vigilant";
    } 

    void Answer1Update()
    {
        if (broker.C >= 2)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }

    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();
        broker.FinishingEventCardWithoutDraw(gameObject);

        broker.selector.GetComponent<UnitSelector>().SortUnitCards(2);//pick firs   
    }
   
    public void Answer2()
    {
        if (broker.vigilantDeck.transform.childCount <= 2)
            broker.msgBoxGameOver.SetActive(true);
        broker.ReturnMarkedToVigilant();

        int random = Random.Range(0, broker.vigilantDeck.transform.childCount - 1);
        Debug.Log($"{Time.time} {broker.vigilantDeck.transform.GetChild(random).GetComponent<Unit>().Name}> discarded!");
        Transform child1 = broker.vigilantDeck.transform.GetChild(random);
        child1.SetParent(broker.recoveringDeck.transform);

        if(broker.vigilantDeck.transform.childCount > 0)
        {
            random = Random.Range(0, broker.vigilantDeck.transform.childCount - 1);
            Transform child2 = broker.vigilantDeck.transform.GetChild(random);
            child2.SetParent(broker.recoveringDeck.transform);
            Debug.Log($"{Time.time} {broker.vigilantDeck.transform.GetChild(random).GetComponent<Unit>().Name}> discarded!");

        }

        broker.FinishingEventCard(gameObject);
    }

}
