using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        

        
        if (controller != null)
        {
            if (controller.currentCoin < controller.maxCoin)
            {
            	
              
              controller.PlaySound(collectedClip);
              Destroy(gameObject);
              controller.ChangeCoin(+1);
             
            	//Destroy(gameObject);
        
        }

    }
   
}
}