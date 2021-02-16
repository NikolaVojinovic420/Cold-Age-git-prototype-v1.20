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
            Answer2Update();
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
        eventScript.button1Txt.text = "P >= 3\nDraw two extra from preparing\nAdd Noise";
        eventScript.button2Txt.text = "Discard random card from vigilant if practical < 6";
    }

    void Answer1Update()
    {
        if (broker.P >= 3)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (CountPractical() >= 6)
            eventScript.button2.interactable = false;
        else eventScript.button2.interactable = true;
    }

    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();

        broker.preparing.GetComponent<Preparing>().DrawToVigilant();
        broker.preparing.GetComponent<Preparing>().DrawToVigilant();
        broker.noise++;
        broker.FinishingEventCard(gameObject);
    }

    public void Answer2()
    {
        broker.ReturnMarkedToVigilant();

        int random = Random.Range(0, broker.vigilantDeck.transform.childCount - 1);
        Debug.Log($"{Time.time} <{broker.vigilantDeck.transform.GetChild(random).GetComponent<Unit>().Name}> discarded.");
        broker.vigilantDeck.transform.GetChild(random).SetParent(broker.recoveringDeck.transform);
        
        broker.FinishingEventCard(gameObject);
    }
    int CountPractical()
    {
        int j = 0;
        for (int i = 0; i < broker.vigilantDeck.transform.childCount; i++)
            j += broker.vigilantDeck.transform.GetChild(i).gameObject.GetComponent<Unit>().Practical;
        for (int i = 0; i < broker.markedDeck.transform.childCount; i++)
            j += broker.markedDeck.transform.GetChild(i).gameObject.GetComponent<Unit>().Practical;
        return j;
    }
}
