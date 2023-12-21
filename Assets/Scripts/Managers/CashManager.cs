using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;

    UIManager uiManager;

    private int coins;
    private string keyCoins = "keyCoins";

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
        LoadCash();
        DisplayCoins();
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
        SaveCash();
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

    private void LoadCash()
    {
        coins = PlayerPrefs.GetInt(keyCoins, 0);
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt(keyCoins, coins);
    }
}
