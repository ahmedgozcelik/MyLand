using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAC;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ManageAnitamions(Vector3 move)
    {
        if(move.magnitude > 0)
        {
            PlayRunAnimation();

            playerAC.transform.forward = move.normalized; // Karakterimi istediðim tarafa doðru yönlendiriyorum
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayRunAnimation()
    {
        playerAC.Play("Run");
    }

    private void PlayIdleAnimation()
    {
        playerAC.Play("Idle");
    }
}
