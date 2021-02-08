using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSomeSpace : MonoBehaviour
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
        eventScript.button1Txt.text = "P >= 3\nDraw one extra from preparing";
        eventScript.button2Txt.text = "Discard random card from vigilant";
    }

    void Answer1Update()
    {
        if (broker.P >= 3)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }

    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();

        broker.preparing.GetComponent<Preparing>().DrawToVigilant();
        broker.preparing.GetComponent<Preparing>().DrawToVigilant();

        broker.FinishingEventCardWithoutDraw(gameObject);
    }

    public void Answer2()
    {
        broker.ReturnMarkedToVigilant();

        int random = Random.Range(0, broker.vigilantDeck.transform.childCount - 1);
        Debug.Log($"{Time.time} <{broker.vigilantDeck.transform.GetChild(random).GetComponent<Unit>().Name}> discarded.");
        broker.vigilantDeck.transform.GetChild(random).SetParent(broker.recoveringDeck.transform);
        
        broker.FinishingEventCard(gameObject);
    }
}
