using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerEventBroker : MonoBehaviour
{
    public AnswerBroker broker;
    public Transform vigilantDeck;
    public Transform markedDeck;
    public Transform futureDeck;
    public Transform futureShuffleDeck;
    public Transform historyDeck;
    public Transform preparingDeck;
    public Transform recoveryDeck;

    //trigger event card objects to instantiate
    public GameObject slaversAttention;
    public GameObject timeWasting;
    public GameObject highbornTribute;
    public GameObject newRecruits;
    public GameObject throng;
    public GameObject necromancerScout;
    bool necromancerScoutBool = false;
    public GameObject weExperimenting;

   ////Just put trigger event into HISTORY deck. Use return true if u want to get trigger event instantly////

    public bool CheckAllTriggerEvents()
    {
        if((vigilantDeck.childCount + markedDeck.childCount) >= vigilantDeck.GetComponentInParent<Vigilant>().maxHand - 1 && !CheckForExistingCopyInGame(slaversAttention) && broker.noise >= 8)
        {
            Debug.Log($"{Time.time} <Slavers attention> inserted into History");
            Instantiate(slaversAttention, broker.HistoryDeck.transform);    //just put into history deck
        }
        if (!CheckForExistingCopyInGame(timeWasting) && !CheckPracticalInVigilant())
        {
            Debug.Log($"{Time.time} You have no practical. <Low productivity> inserted into Future");
            Instantiate(timeWasting, broker.HistoryDeck.transform);
        }
        if (!CheckForExistingCopyInGame(highbornTribute) && broker.noise >= 6)
        {
            Debug.Log($"{Time.time} You have no practical. <Highborn demand tribute> inserted into Future");
            Instantiate(highbornTribute, broker.HistoryDeck.transform);
        }
        if (!CheckForExistingCopyInGame(newRecruits) && broker.noise >= 8 && (vigilantDeck.childCount + markedDeck.childCount) <= 3)
        {
            Debug.Log($"{Time.time} You have no practical. <New recruits> inserted into Future");
            Instantiate(newRecruits, broker.HistoryDeck.transform);
        }
        if (!CheckForExistingCopyInGame(throng) && broker.noise >= 10)
        {
            Debug.Log($"{Time.time} You have no practical. <Throng!> inserted into Future");
            Instantiate(throng, broker.HistoryDeck.transform);
        }
        if (!CheckForExistingCopyInGame(necromancerScout) && broker.noise >= 10 && !necromancerScoutBool)
        {
            necromancerScoutBool = true;
            Debug.Log($"{Time.time} You have no practical. <Necromancer's scouts spotted> inserted into Future");
            Instantiate(necromancerScout, broker.HistoryDeck.transform);
        }
        if (!CheckForExistingCopyInGame(weExperimenting) && broker.noise >= 6)
        {
            Debug.Log($"{Time.time} You have no practical. <New medical reserach> inserted into Future");
            Instantiate(weExperimenting, broker.HistoryDeck.transform);
        }

        return false;
    }

    bool CheckForExistingCopyInGame(GameObject obj)
    {
        for (int i = 0; i < futureDeck.childCount; i++)
            if (futureDeck.GetChild(i).GetComponent<Event>().Name == obj.GetComponent<Event>().Name)
                return true;
        for (int i = 0; i < historyDeck.childCount; i++)
            if (historyDeck.GetChild(i).GetComponent<Event>().Name == obj.GetComponent<Event>().Name)
                return true;
        for (int i = 0; i < broker.transform.childCount; i++)
            if (broker.transform.GetChild(i).GetComponent<Event>().Name == obj.GetComponent<Event>().Name)
                return true;

        return false;
    }

    bool CheckPracticalInVigilant()
    {
        for (int i = 0; i < vigilantDeck.childCount; i++)
            if (vigilantDeck.GetChild(i).gameObject.GetComponent<Unit>().Practical > 0)
                return true;
        for (int i = 0; i < markedDeck.childCount; i++)
            if (markedDeck.GetChild(i).gameObject.GetComponent<Unit>().Practical > 0)
                return true;

        return false;
    }

}
