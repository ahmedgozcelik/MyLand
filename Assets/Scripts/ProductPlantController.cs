using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    [SerializeField] private GameObject boxGO;
    private BagController bagController;

    private bool isReadyToPick;
    private Vector3 originalScale;
    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick) // Player'a temas ettiyse ve toplanmaya hazýrsa
        {
            bagController = other.GetComponent<BagController>();
            bagController.AddProductToBag(boxGO);
            isReadyToPick = false;
            StartCoroutine(ProductPicked());
        }
    }

    IEnumerator ProductPicked() // Fidenin boyutunu 3 e 1 oranýnda küçültüyoruz. Daha sonra 3 saniye bekliyoruz ve fidenin boyutunu büyütüyoruz. Fide toplanmak için hazýr hale geliyor.
    {
        float duration = 1f;
        float timer = 0f;
        Vector3 targetScale = originalScale / 3;

        // Parça parça küçültüyoruz
        while(timer < duration)
        {
            float t = timer / duration;
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t); // Lerp fonksiyonu -> originalScale ve targetScale arasýnda t miktarda interpolasyon
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        //Fidemiz küçüldü
        yield return new WaitForSeconds(5f);

        timer = 0f;
        float growBackDuration = 1f;

        // Parça parça büyütüyoruz
        while(timer < growBackDuration)
        {
            float t = timer / growBackDuration;
            Vector3 newScale = Vector3.Lerp(targetScale, originalScale, t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        isReadyToPick = true;
        yield return null;
    }
}
