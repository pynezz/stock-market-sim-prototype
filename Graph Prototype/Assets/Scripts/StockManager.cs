using JetBrains.Annotations;
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
        _stock1PriceText.text = "$ " + _stock1Price.ToString("F2"); //F2 is two decimals
    }

    #endregion

    #region Custom Methods

    void RandomNumber(bool change, int number)
    {
        float newNumber = Random.Range(0, _stock1Price);
        float changeAmt = Random.Range(-5, 5);
        float oldPrice = _stock1Price;

        

        if (change)
        {
            float oneMinPrice = oldPrice;
            float oneHourPrice = oneMinPrice;
        }
        else if (!change)
        {
            if (changeAmt < 0 && number == 1)
            {
                float randomAmt = Random.Range(0f, 2f);
                float maxPrice = Mathf.Clamp(_stock1Price, randomAmt * _stock1Price * Mathf.Sqrt((_stock1Price -randomAmt)+randomAmt) + _stock1Price - randomAmt, randomAmt * 30);
                if (maxPrice != 0)
                {
                    float randomFloat = Random.Range(0f, 2f);
                    Debug.Log("Random float: " + randomFloat);
                    _stock1Price += randomFloat;
                    if (randomFloat < randomAmt + 0.5f)
                    {
                        float randFloat = Random.Range(0f, 5f);
                        randFloat = Mathf.Sqrt(randFloat);
                        _stock1Price -= _stock1Price - randFloat;
                    }
                }
                else if (maxPrice == 0)
                {
                    maxPrice = _stock1Price;
                }
                Debug.Log(_stock1Price);
            }
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
                    RandomNumber(true, 0);
                    //1 hour 
                }
            }
            int mI = 1;
            int mA = 4;
            int number = Random.Range(mI, mA);          //Number = 1 or 2
            if (number % 3 == 0)                        //If number is dividable with 3
            {
                mA = Random.Range(1, mA + 1);
                RandomNumber(false, number);            //RamdomNumber = false, X 
            }
            else if (number % 2 == 1)                   //If number is an odd number
            {
                mA = Random.Range(mI, mA);
                RandomNumber(false, number);            //RandomNumber = false, X
            }            
        }
        StartCoroutine(StockUpdateMin(60));
    }

    #endregion

}
