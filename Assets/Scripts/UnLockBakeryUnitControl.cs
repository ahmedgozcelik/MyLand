using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnLockBakeryUnitControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;

    [SerializeField] private int useProductInSeconds = 10;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGO;

    [SerializeField] private ParticleSystem smokeParticle;

    private float time;
    private int storedProductCount;
    void Start()
    {
        DisplayProductCount();
    }

    void Update()
    {
        if(storedProductCount > 0)
        {
            time += Time.deltaTime;  

            if(time >= useProductInSeconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
    }

    private void DisplayProductCount()
    {
        bakeryText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
        ControlSmokeEffect();
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

    private void UseProduct()
    {
        storedProductCount--;
        DisplayProductCount();
        CreateCoin();
    }

    private void CreateCoin()
    {
        Vector3 position = Random.insideUnitSphere * 1f; // belirlenen yerin �evresinde random bir noktas�nda olu�ur
        Vector3 InstantiatePos = coinTransform.position + position;

        Instantiate(coinGO, InstantiatePos, Quaternion.identity);
    }

    private void ControlSmokeEffect()
    {
        if(storedProductCount == 0)
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();
            }
        }
        else
        {
            if(smokeParticle.isStopped)
            {
                smokeParticle.Play(); 
            }
        }
    }
   
}
