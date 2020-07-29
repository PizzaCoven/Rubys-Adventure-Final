using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCollectible : MonoBehaviour
{ 
    public AudioClip collectedClip;
    public GameObject twinkleEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        Instantiate(twinkleEffect,transform.position,transform.rotation);

        
        if (controller != null)
        //{
            if (controller.currentAmmo < controller.maxAmmo)
            {
            	
              
              controller.PlaySound(collectedClip);
              Destroy(gameObject);
              controller.ChangeAmmo(1);
            	//Destroy(gameObject);
        
        }

    }
   
}


    