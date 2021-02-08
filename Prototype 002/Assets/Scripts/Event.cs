using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public string Name;
    public bool ExhaustableTriggerEvent;
    public Text nameText;
    public Button button1;
    public Text button1Txt;
    public Button button2;
    public Text button2Txt;
    public Button button3;
    public Text button3Txt;
    public Button button4;
    public Text button4Txt;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{Time.time} <{Name}> has been activated.");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Activate();
    }

    void Activate()
    {
        if(gameObject.transform.parent.tag == "AnswerCard" && !active)
            active = true;
        nameText.text = Name;
    }

}
