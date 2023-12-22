using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProductPlantController : MonoBehaviour
{
    [SerializeField] private ProductData productData;
    private BagController bagController;
    private AudioManager audioManager;

    private bool isReadyToPick;
    private Vector3 originalScale;
    void Start()
    {
        audioManager = AudioManager.instance;

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

            if (bagController.IsEmptySpace())
            {
                audioManager.PlayAudio(AudiClipType.grabClip);
                bagController.AddProductToBag(productData);
                isReadyToPick = false;
                StartCoroutine(ProductPicked());
            }
        }
    }



    IEnumerator ProductPicked() // Fidenin boyutunu 3 e 1 oranýnda küçültüyoruz. Daha sonra 3 saniye bekliyoruz ve fidenin boyutunu büyütüyoruz. Fide toplanmak için hazýr hale geliyor.
    {
        float duration = 1f;
        float timer = 0f;
        Vector3 targetScale = originalScale / 3;

        // Parça parça küçültüyoruz
        //while(timer < duration)
        //{
        //    float t = timer / duration;
        //    Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t); // Lerp fonksiyonu -> originalScale ve targetScale arasýnda t miktarda interpolasyon
        //    transform.localScale = newScale;
        //    timer += Time.deltaTime;
        //    yield return null;
        //}

        transform.DOScale(targetScale, duration).OnComplete(() => // bu hedefe ulaþtýðýmýzda çalýþtýracaðýmýz kodlar için oncomplete kullandýk.
        {
            
        });

        //Fidemiz küçüldü
        yield return new WaitForSeconds(5f);

        timer = 0f;
        float growBackDuration = 1f;

        // Parça parça büyütüyoruz
        //while(timer < growBackDuration)
        //{
        //    float t = timer / growBackDuration;
        //    Vector3 newScale = Vector3.Lerp(targetScale, originalScale, t);
        //    transform.localScale = newScale;
        //    timer += Time.deltaTime;
        //    yield return null;
        //}

        transform.DOScale(originalScale, growBackDuration).OnComplete(() =>
        {
            isReadyToPick = true;
        });

        
        yield return null;
    }
}