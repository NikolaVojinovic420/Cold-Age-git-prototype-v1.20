using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throng : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons
    public GameObject disfigured;

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
        eventScript.button1Txt.text = "Purge them!\nA >= 12\nAdd morale\nExhaust";
        eventScript.button2Txt.text = "Purge them!\nA >= 12 and 2 <Disfigured> engaged\nInsert <Disfigured> into Preparing\nAdd morale\nExhaust";
        eventScript.button3Txt.text = "C >= 3\nP >= 2\nDrown them\nLose Noise\nExhaust";
        eventScript.button4Txt.text = "Lose";
    }
    void Answer1Update()
    {
        if (broker.A >= 12)
            eventScript.button1.interactable = true;
        else
            eventScript.button1.interactable = false;
    }
    void Answer2Update()
    {
        int j = 0;
        for (int i = 0; i < broker.markedDeck.transform.childCount; i++)
            if (broker.markedDeck.transform.GetChild(i).gameObject.GetComponent<Unit>().Name == "Disfigured")
                j++;
        if (broker.A >= 12 && j>1)
            eventScript.button2.interactable = true;
        else
            eventScript.button2.interactable = false;


    }
    void Answer3Update()
    {

        if (broker.C >= 3 && broker.P >= 2)
            eventScript.button3.interactable = true;
        else
            eventScript.button3.interactable = false;

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
        broker.morale++;
        broker.InstatiateUnit(disfigured);
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        broker.SendMarkedIntoRecovering();
        broker.noise--;
        broker.FinishingEventCard(gameObject);
    }
    public void Answer4() => broker.msgBoxGameOver.SetActive(true);
  
}
