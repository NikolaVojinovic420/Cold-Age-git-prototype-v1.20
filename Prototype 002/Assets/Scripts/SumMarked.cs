using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumMarked : MonoBehaviour
{
    public GameObject markedDeck;
    public Text title;
    public GameObject Ag;
    public GameObject Pr;
    public GameObject Cr;
    int checkCount;
    public int a = 0;
    public int p = 0;
    public int c = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CountAspects();
    }

    void CountAspects()
    {
        if (checkCount != markedDeck.transform.childCount)
        {
             a = 0;
             p = 0;
             c = 0;
            for (int i = 0; i < markedDeck.transform.childCount; i++)
            {
                a += markedDeck.transform.GetChild(i).GetComponent<Unit>().Agressive;
                p += markedDeck.transform.GetChild(i).GetComponent<Unit>().Practical;
                c += markedDeck.transform.GetChild(i).GetComponent<Unit>().Creative;
            }
            Ag.GetComponent<Text>().text = $"{a}";
            Pr.GetComponent<Text>().text = $"{p}";
            Cr.GetComponent<Text>().text = $"{c}";
            checkCount = markedDeck.transform.childCount;
            title.text = $"MARKED {markedDeck.transform.childCount}";
        }
    }
}
