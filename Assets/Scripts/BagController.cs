using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    [SerializeField] TextMeshPro maxText;
    public List<ProductData> productDataList;
    private Vector3 productSize;
    int maxBagCapacity;
    void Start()
    {
        maxBagCapacity = 5;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                Destroy(bag.transform.GetChild(i).gameObject);
                productDataList.RemoveAt(i);
            }
            ControlBagCapacity();
        }
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
}
