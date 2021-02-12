using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    AnswerBroker broker; //use for aspect values
    Event eventScript;  //use for command of buttons

    public GameObject youngOne;
    public GameObject guard;
    public GameObject engineer;
    public GameObject tinkerer;
    bool foundYoungOne = false;
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
            if (broker.FindAndReturnUnit(youngOne, broker.vigilantDeck) != null && !foundYoungOne)
                foundYoungOne = true;
            if (broker.FindAndReturnUnit(guard, broker.vigilantDeck) != null && !foundGuard)
                foundGuard = true;
            Answer1Update();
            Answer2And3Update();
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
        eventScript.button1Txt.text = "Sacrifice <Young one>\nAdd <Guard>\nExhaust\nAdd Morale";
        eventScript.button2Txt.text = "Sacrifice <Guard>\nAdd <Engineer>\nExhaust\nAdd Morale and Noise";
        eventScript.button3Txt.text = "Sacrifice <Guard>\nAdd <Tinkerer>\nExhaust\nAdd Morale and Noise";
        eventScript.button4Txt.text = "Exhaust\nDouble Noise lose";
    }
    void Answer1Update()
    {
        if (foundYoungOne)
            eventScript.button1.interactable = true;
        else eventScript.button1.interactable = false;
    }
    void Answer2And3Update()
    {
        if (foundGuard)
        {
            eventScript.button2.interactable = true;
            eventScript.button3.interactable = true;
        }
        else
        {
            eventScript.button2.interactable = false;
            eventScript.button3.interactable = false;
        }
    }
    public void Answer1()
    {
        broker.ReturnMarkedToVigilant();

        Destroy(broker.FindAndReturnUnit(youngOne, broker.vigilantDeck));
        Instantiate(guard, broker.recoveringDeck.transform);
        Debug.Log($"{Time.time} Young one destroyed, Guard instantiated into Recovering.");
        broker.morale++;
        broker.FinishingEventCard(gameObject);
    }
    public void Answer2()
    {
        broker.ReturnMarkedToVigilant();

        Destroy(broker.FindAndReturnUnit(guard, broker.vigilantDeck));
        Instantiate(engineer, broker.recoveringDeck.transform);
        Debug.Log($"{Time.time} Guard destroyed, Engineer instantiated into Recovering.");
        broker.morale++;
        broker.noise++;
        broker.FinishingEventCard(gameObject);
    }
    public void Answer3()
    {
        broker.ReturnMarkedToVigilant();

        Destroy(broker.FindAndReturnUnit(guard, broker.vigilantDeck));
        Instantiate(tinkerer, broker.recoveringDeck.transform);

        Debug.Log($"{Time.time} Guard destroyed, Tinkerer instantiated into Recovering.");
        broker.FinishingEventCard(gameObject);
    }
    public void Answer4()
    {
        broker.ReturnMarkedToVigilant();
        broker.noise--;
        broker.noise--;
        broker.FinishingEventCard(gameObject);
    }

}
