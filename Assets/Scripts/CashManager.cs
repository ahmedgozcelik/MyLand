using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;

    UIManager uiManager;

    private int coins;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }

    public void AddCoin(int price)
    {
        coins += price;
        DisplayCoins();
    }

    private void DisplayCoins()
    {
        uiManager.ShowCoinCountOnScreen(coins);
    }

    private void SpendCoin(int price)
    {
        coins -= price;
        DisplayCoins();
    }

    public bool TryByThisUnit(int price)
    {
        if(GetCoins() > price)
        {
            // paraný harca
            SpendCoin(price);
            return true;
        }
        return false;
    }

    public int GetCoins()
    {
        return coins;
    }
}
