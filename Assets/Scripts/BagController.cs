using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<GameObject> productList;
    private Vector3 productSize;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            AddProductToBag(other.gameObject);
        }
    }

    public void AddProductToBag(GameObject product)
    {
        GameObject boxProduct = Instantiate(product, Vector3.zero, Quaternion.identity);

        boxProduct.transform.SetParent(bag, true);

        float yPositoion = CalculateNewYPositionOfBox();
        
        CalculateObjectSize(boxProduct);
        boxProduct.transform.localRotation = Quaternion.identity;
        boxProduct.transform.localPosition = Vector3.zero;
        boxProduct.transform.localPosition = new Vector3(0, yPositoion, 0);

        productList.Add(boxProduct);
    }

    private float CalculateNewYPositionOfBox()
    {
        // ürünün sahnedeki yükseliði * ürünün adedi
        float newYPos = productSize.y * productList.Count;
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
}
