﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectorPick : MonoBehaviour
{
    public GameObject vigilantDeck;
    public GameObject universalDeck;
    GameObject unitSelector;
    // Start is called before the first frame update
    void Start()
    {
        unitSelector = gameObject.transform.parent.gameObject;   
        vigilantDeck = unitSelector.GetComponent<UnitSelector>().vigilantDeck;
    }

    public void InsertIntoVigilant()
    {
        string stringNumber = $"{gameObject.GetComponentInChildren<Text>().text}";
        Debug.Log($"{Time.time} *{universalDeck.transform.GetChild(int.Parse(ReturnStringNumber(stringNumber)) - 1).gameObject.GetComponent<Unit>().Name}* " +
            $"is picked and moved from {universalDeck.name} to Vigilant.");
        universalDeck.transform.GetChild(int.Parse(ReturnStringNumber(stringNumber))-1).SetParent(vigilantDeck.transform);

        unitSelector.GetComponent<UnitSelector>().pickTimes--;
       
        if(unitSelector.GetComponent<UnitSelector>().pickTimes > 0)
            unitSelector.GetComponent<UnitSelector>().ReorderUnitCards();
        else
        {
            unitSelector.SetActive(false);
            unitSelector.GetComponent<UnitSelector>().ClearUnitSelector();
        }
    }

    string ReturnStringNumber(string fullName)
    {
        string tmp = "";
        for (int i = 0; i < fullName.Length; i++)
        {
            if (fullName[i] != ' ')
                tmp += fullName[i];
            else break;
        }
        return tmp;    
    }
}
