using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedUnitControl : MonoBehaviour
{
    AudioManager audioManager;
    CashManager cashManager;

    [Header("Settings")]
    [SerializeField] private int price;
    [SerializeField] private int id;

    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;

    private bool isPurchased;
    private string keyUnit = "keyUnit";
    void Start()
    {
        audioManager = AudioManager.instance;
        cashManager = CashManager.instance;
        priceText.text = price.ToString();
        LoadUnit();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            // ürünü param yeterse aç
            UnLockUnit();
        }
    }

    private void UnLockUnit()
    {
        if (cashManager.TryByThisUnit(price)){
            audioManager.PlayAudio(AudiClipType.shopClip);
            Unlock();
            SaveUnit();
        }
        // parasý var mý kontrol et, varsa ürünü aç
    }

    private void Unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unlockedUnit.SetActive(true);
    }

    private void SaveUnit()
    {
        string key = keyUnit + id.ToString();
        PlayerPrefs.SetString(key, "saved");
    }

    private void LoadUnit()
    {
        string key = keyUnit + id.ToString();
        string status = PlayerPrefs.GetString(key);

        if (status.Equals("saved"))
        {
            Unlock();
        }
    }
}
