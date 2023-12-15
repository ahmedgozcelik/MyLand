using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnLockBakeryUnitControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;

    private int storedProductCount;
    void Start()
    {
        DisplayProductCount();
    }

    void Update()
    {
        
    }

    private void DisplayProductCount()
    {
        bakeryText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
    }

    public ProductType GetNeededProductType()
    {
        return productType;
    }

    public bool StoreProduct()
    {
        if(maxStoredProductCount == storedProductCount)
        {
            return false;
        }
        storedProductCount++;
        DisplayProductCount();
        return true;
    }
   
}
