using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedUnitControl : MonoBehaviour
{
    CashManager cashManager;

    [Header("Settings")]
    [SerializeField] private int price;

    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;

    private bool isPurchased;
    void Start()
    {
        cashManager = CashManager.instance;
        priceText.text = price.ToString();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            // �r�n� param yeterse a�
            UnLockUnit();
        }
    }

    private void UnLockUnit()
    {
        if (cashManager.TryByThisUnit(price)){
            Unlock();
        }
        // paras� var m� kontrol et, varsa �r�n� a�
    }

    private void Unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unlockedUnit.SetActive(true);
    }
}
