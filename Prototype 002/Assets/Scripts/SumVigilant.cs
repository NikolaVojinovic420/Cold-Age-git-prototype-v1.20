﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumVigilant : MonoBehaviour
{
    public GameObject vigilantDeck;
    public Text title;
    public GameObject A;
    public GameObject P;
    public GameObject C;
    public GameObject warning;
    public Text warningtext;
    public AnswerBroker broker;
    int checkCount;
    // Start is called before the first frame update
    void Start()
    {
        broker = GameObject.FindWithTag("AnswerCard").GetComponent<AnswerBroker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountAspects();
        Warnings();
        if (Time.time % 0.4 == 0) //refresh
            checkCount--;

    }

    void CountAspects()
    {
        if (checkCount != vigilantDeck.transform.childCount)
        {
            int a = 0;
            int p = 0;
            int c = 0;
            for (int i = 0; i < vigilantDeck.transform.childCount; i++)
            {
                a += vigilantDeck.transform.GetChild(i).GetComponent<Unit>().Agressive;
                p += vigilantDeck.transform.GetChild(i).GetComponent<Unit>().Practical;
                c += vigilantDeck.transform.GetChild(i).GetComponent<Unit>().Creative;
            }
            A.GetComponent<Text>().text = $"{a}";
            P.GetComponent<Text>().text = $"{p}";
            C.GetComponent<Text>().text = $"{c}";
            checkCount = vigilantDeck.transform.childCount;
            title.text = $"VIGILANT {vigilantDeck.transform.childCount}";
        }
    }

    //warnings
    void Warnings()
    {
        //BewareOfSlavers();   // maybe activate later on if we want
    }
    public void BewareOfSlavers()
    {
        if (vigilantDeck.transform.childCount >= 9 && broker.noise >= 8)
        {
            warningtext.text = "Care for Slavers";
            warning.SetActive(true);
        }
        else warning.SetActive(false);
    }

}
