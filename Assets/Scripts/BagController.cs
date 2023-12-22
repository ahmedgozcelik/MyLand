using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    [SerializeField] TextMeshPro maxText;

    CashManager cashManager;
    AudioManager audioManager;

    public List<ProductData> productDataList;
    private Vector3 productSize;
    int maxBagCapacity;

    private string bagCapacityKey = "bagCapacityKey";
    void Start()
    {
        audioManager = AudioManager.instance;
        cashManager = CashManager.instance;

        maxBagCapacity = LoadBagCapacity();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            PlayShopSound();
            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                SellProductsToShop(productDataList[i]);
                Destroy(bag.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
            }
            ControlBagCapacity();
        }

        if (other.CompareTag("UnLockBakeryUnit"))
        {
            UnLockBakeryUnitControl bakeryUnit = other.GetComponent<UnLockBakeryUnitControl>();

            ProductType neededType = bakeryUnit.GetNeededProductType();

            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                if (productDataList[i].productType == neededType)
                {
                    if(bakeryUnit.StoreProduct() == true)
                    {
                        Destroy(bag.transform.GetChild(i).gameObject);
                        productDataList.RemoveAt(i);
                    }
                }
            }
            StartCoroutine(PutProductsInOrder());
            ControlBagCapacity();
            
        }
    }

    private void SellProductsToShop(ProductData productData)
    {
        // cashManager'a söyle ürün satýldý.
        cashManager.ExchangeProduct(productData);
    }

    public void AddProductToBag(ProductData productData)
    {
        GameObject boxProduct = Instantiate(productData.productPrefab, Vector3.zero, Quaternion.identity);

        boxProduct.transform.SetParent(bag, true);

        float yPositoion = CalculateNewYPositionOfBox();
        
        CalculateObjectSize(boxProduct);
        boxProduct.transform.localRotation = Quaternion.identity;
        //boxProduct.transform.localPosition = Vector3.zero;
        boxProduct.transform.localPosition = new Vector3(0, yPositoion, 0);

        productDataList.Add(productData);
        
        ControlBagCapacity();
    }

    private float CalculateNewYPositionOfBox()
    {
        // ürünün sahnedeki yükseliði * ürünün adedi
        float newYPos = productSize.y * productDataList.Count;
        return newYPos;
    }

    private void CalculateObjectSize(GameObject gameObject)
    {
        if(productSize == Vector3.zero)
        {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            productSize = renderer.bounds.size;
        }
    }

    private void ControlBagCapacity()
    {
        if(productDataList.Count == maxBagCapacity)
        {
            SetMaxOn();
            // max yazýsýný çýkar ve daha fazla ürün alýnmasýný engelle
        }
        else
        {
            SetMaxOff();
        }
    }

    private void SetMaxOn()
    {
        if (!maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(true);
        }
    }

    private void SetMaxOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false);
        }
    }

    public bool IsEmptySpace()
    {
        if(productDataList.Count < maxBagCapacity)
        {
            return true;
        }
        return false;
    }

    private IEnumerator PutProductsInOrder()
    {
        yield return new WaitForSeconds(0.15f);
        for (int i = 0; i < bag.childCount; i++)
        {
            float newYPos = productSize.y * i;
            bag.GetChild(i).transform.localPosition = new Vector3(0, newYPos, 0);
        }
    }

    private void PlayShopSound()
    {
        if(productDataList.Count > 0)
        {
            audioManager.PlayAudio(AudiClipType.shopClip);
        }
    }

    public void BoostBagCapacity(int boostCount)
    {
        maxBagCapacity += boostCount;
        PlayerPrefs.SetInt(bagCapacityKey, maxBagCapacity);
        ControlBagCapacity();
    }

    private int LoadBagCapacity()
    {
        int maxBag = PlayerPrefs.GetInt(bagCapacityKey, 5);
        return maxBag;
    }
}
