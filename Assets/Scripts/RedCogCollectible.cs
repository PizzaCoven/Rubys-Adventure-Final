using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCogCollectible : MonoBehaviour
{ public AudioClip collectedClip;
    public GameObject twinkleEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        Instantiate(twinkleEffect,transform.position,transform.rotation);

        
        if (controller != null)
        {
            if (controller.currentRedAmmo < controller.maxRedAmmo)
            {
            	
              
              controller.PlaySound(collectedClip);
              Destroy(gameObject);
              controller.ChangeRedAmmo(1);
            	
            }
        }

    }
   
}