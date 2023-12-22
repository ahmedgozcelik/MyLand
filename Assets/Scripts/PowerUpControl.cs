using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControl : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] private PowerUpData powerUpData;
    [SerializeField] private int lockedUnitID;

    string powerUpStatusKey = "powerUpStatusKey";
    bool isPowerUpUsed;
    void Start()
    {
        audioManager = AudioManager.instance;

        isPowerUpUsed = GetPowerUpStatus();
    }

    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) //collider'dan çýkarken çalýþacak
    {
        if (other.CompareTag("Player")){
            if(powerUpData.powerUpType == PowerUpType.bagBooster && !isPowerUpUsed)
            {
                isPowerUpUsed = true;
                BagController bagController = other.GetComponent<BagController>();
                bagController.BoostBagCapacity(powerUpData.boostCount);
                audioManager.PlayAudio(AudiClipType.grabClip);
                PlayerPrefs.SetString(powerUpStatusKey, "used");
            }
        }
    }

    private bool GetPowerUpStatus()
    {
        string status = PlayerPrefs.GetString(powerUpStatusKey, "ready");
        if (status.Equals("ready"))
        {
            return false;
        }
        return true;
    }
}
