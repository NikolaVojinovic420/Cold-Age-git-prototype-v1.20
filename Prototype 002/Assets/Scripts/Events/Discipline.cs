using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discipline : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject disfigured;
    public GameObject cyborg;
    public GameObject dispair;

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
        eventScript.button1Txt.text = "C >= 2\nDraw <Cyborg>";
        eventScript.button2Txt.text = "P >= 3\nDraw <Disfigured>";
        eventScript.button3Txt.text = "A >= 4\nP >= 2\nC >= 1\nGo trough.";
        eventScript.button4Txt.text = "Degrading discipline\nInsert <Despair> into History";
    }
    void Answer1Update()
    {
        if (broker.C >= 2 && broker.FindAndReturnUnit(cyborg, broker.preparingDeck) != null)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        if (broker.P >= 3 && broker.FindAndReturnUnit(disfigured, broker.preparingDeck) != null)
            eventScript.button2.interactable = true;
        else eventScript.button2.interactable = false;
    }
    void Answer3Update()
    {
        if (broker.A >= 4 && broker.P >= 2 && broker.C >= 1)
            eventScript.button3.interactable = true;
        else eventScript.button3.interactable = false;
    }
    public void Answer1()
    {
        broker.SendMarkedIntoRecovering();
        

        GameObject foundCyborg = broker.FindAndReturnUnit(cyborg, broker.preparingDeck);
        if(foundCyborg != null)
        {
            Debug.Log($"{Time.time} {foundCyborg.GetComponent<Unit>().Name} instantiated into Vigilant.");
            foundCyborg.transform.SetParent(broker.vigilantDeck.transform);
        }
       
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.SendMarkedIntoRecovering();
        GameObject foundDisfigured = broker.FindAndReturnUnit(disfigured, broker.preparingDeck);
        if(foundDisfigured != null)
        {
            Debug.Log($"{Time.time} {foundDisfigured.GetComponent<Unit>().Name} instantiated into Vigilant.");
            foundDisfigured.transform.SetParent(broker.vigilantDeck.transform);
        }
       
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        broker.SendMarkedIntoRecovering();
        
        broker.FinishingEventCard(gameObject);
    }
    public void Answer4()
    {
        broker.ReturnMarkedToVigilant();

        broker.InstatiateEvent(dispair);

        broker.FinishingEventCard(gameObject);
    }
}
