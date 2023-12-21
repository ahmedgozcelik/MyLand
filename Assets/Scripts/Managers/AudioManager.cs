using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudiClipType
{
    grabClip,
    shopClip
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource audioSource;
    public AudioClip grabClip, shopClip;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayAudio(AudiClipType clipType)
    {
        if(audioSource != null)
        {
            AudioClip audioClip = null;
            
            if(clipType == AudiClipType.grabClip)
            {
                audioClip = grabClip;
            }
            else if(clipType == AudiClipType.shopClip)
            {
                audioClip = shopClip;
            }

            audioSource.PlayOneShot(audioClip, 0.6f);
        }
    }

    public void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
}
