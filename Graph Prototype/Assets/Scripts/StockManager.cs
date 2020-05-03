using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StockManager : MonoBehaviour
{
    #region Variables

    private Text _stock1Text, _stock2Text, _stock3Text, _stock4Text;
    private Text _stock1PriceText, _stock2PriceText, _stock3PriceText, _stock4PriceText;

    private float _stock1Price = 5.10f;
    private float _stock2Price = 1.00f;
    private float _stock3Price = 9.21f;
    private float _stock4Price = 40.02f;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        _stock1PriceText = GameObject.Find("price 1").GetComponent<Text>();        

        _stock1Text = GameObject.Find("Stock Name 1").GetComponent<Text>();
        _stock2Text = GameObject.Find("Stock Name 2").GetComponent<Text>();
        _stock3Text = GameObject.Find("Stock Name 3").GetComponent<Text>();
        _stock4Text = GameObject.Find("Stock Name 4").GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(StockUpdateMin(2));
        _stock1Text.text = "Test";
    }

    private void Update()
    {
        _stock1PriceText.text = "$ " + _stock1Price.ToString();
    }

    #endregion

    #region Custom Methods

    void RandomNumber(bool change, int number)
    {
        float newNumber = Random.Range(0, _stock1Price);
        float changeAmt = Random.Range(-5, 5);

        if (change)
        {
            newNumber = Mathf.Ceil(newNumber + changeAmt);
            _stock1Price = (newNumber / 2) + _stock1Price;
        }
        else if (!change)
        {
            
        }
    }

    #endregion

    #region IENumerators

    WaitForSeconds delay = new WaitForSeconds(1);
    IEnumerator StockUpdateMin (int seconds)                //Timer counting down from 60 seconds then restarting
    {
        int count = seconds;
        int min = 0;
        while (count > 0)
        {
            yield return delay;
            count--;
            if (count == 0)
            {
                min++;              //1 min
                if (min == 60)
                {
                    min = 0;
                    //1 hour 
                }
            }
            int mI = 1;
            int mA = 3;
            int number = Random.Range(mI, mA);          //Number = 1 or 2
            bool change = false;                        //change = false
            if (number % 2 == 0)                        //If (1 or 2) is an even number
            {
                mI = Random.Range(1, mI + 1);
                change = !change;                       //change = true
                RandomNumber(change, number);           //RamdomNumber = true, 2 
            }
            else if (number % 2 == 1)                   //If number is an odd number
            {
                RandomNumber(change, number);           //RandomNumber = false, 1
            }            
        }
        StartCoroutine(StockUpdateMin(60));
    }

    #endregion

}
