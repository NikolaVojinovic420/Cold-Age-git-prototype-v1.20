using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string Name;
    public int Agressive;
    public int Practical;
    public int Creative;
    public Text text;
    public Transform vigilantDeck;
    public Transform markedDeck;
    public GameObject broker;
    public GameObject newButton;

    //private void Awake()  // when instance is made not when active == true
    //{
    //    Debug.Log($"*{Name}* has been instatiated.");
    //}

    // Start is called before the first frame update
    void Start()
    {
        text.text = $"{Name}\n\nAgressive: {Agressive}\nPractical: {Practical}\nCreative: {Creative}";
        broker = GameObject.FindWithTag("AnswerCard");
        vigilantDeck = broker.GetComponent<AnswerBroker>().vigilantDeck.transform;
        markedDeck = broker.GetComponent<AnswerBroker>().markedDeck.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendToAnotherDeck()
    {
        if (gameObject.transform.parent.tag == "Vigilant")
            markedDeck.GetComponentInParent<Marked>().AddInMarkedDeck(gameObject.transform);          
        else vigilantDeck.GetComponentInParent<Vigilant>().AddInVigilantDeck(gameObject.transform);
    }
}
