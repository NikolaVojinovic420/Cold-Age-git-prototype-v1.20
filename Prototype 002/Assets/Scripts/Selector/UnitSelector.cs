using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    public GameObject preparingDeck;
    public GameObject vigilantDeck;
    public GameObject broker;
    public Button button;
    public int pickTimes;

    public void SortUnitCards(int cycles, GameObject deck)
    {
        if (cycles <= 0)
            return;
        if(cycles > deck.transform.childCount)
            cycles = deck.transform.childCount;
        pickTimes = cycles;
        gameObject.SetActive(true);
        float positionX = gameObject.transform.position.x;
        float positionY = gameObject.transform.position.y;
        
        if(deck.transform.childCount <= 0) //if preparing is empty
        {
            gameObject.SetActive(false);
            Debug.Log($"{Time.time} {deck.name} is empty no draw.");

        }
        for (int i = 0; i < deck.transform.childCount; i++)
        {
            Instantiate(button, gameObject.transform);
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.GetComponent<ButtonSelectorPick>().universalDeck = deck;
        }
        int j = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponentInChildren<Text>().text = $"{i + 1} {deck.transform.GetChild(i).GetComponent<Unit>().Name}";
            gameObject.transform.GetChild(i).transform.position = new Vector2(positionX + j * 200, positionY);
            j ++;
            if (i != 0 && i % 4 == 0) //new row
            {
                j=0;
                positionY -= 50;
            }               
        }       
    }
    public void ReorderUnitCards()
    {
        Destroy(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject); //destroy last button
        for (int i = 0; i < gameObject.transform.childCount; i++)//make new order
                gameObject.transform.GetChild(i).GetComponentInChildren<Text>().text = $"{i + 1} {preparingDeck.transform.GetChild(i).GetComponent<Unit>().Name}";
          
    }
    public void ClearUnitSelector()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            Destroy(gameObject.transform.GetChild(i).gameObject);
    }

}
