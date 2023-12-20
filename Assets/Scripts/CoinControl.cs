using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    CashManager cashManager;

    [SerializeField] private int coinPrice;
    void Start()
    {
        cashManager = CashManager.instance;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cashManager.AddCoin(coinPrice);
            Destroy(gameObject);
        }
    }
}
