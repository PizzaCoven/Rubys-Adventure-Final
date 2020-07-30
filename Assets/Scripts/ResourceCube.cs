using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCube : MonoBehaviour
{
    public HealthCollectible healthCollectible;
    public CogCollectible cogCollectible;
  
    int randomNumber;
    int randomNumber2;

    public void BlownUp()
    {

        
        Debug.Log("I Was Hit!");
       //RubyController controller = other.GetComponent<RubyController>();
       
        int randomNumber = UnityEngine.Random.RandomRange(1, 3);
        for (int i = 0; i < randomNumber; i ++);
        {   Vector3 randomPosition = new Vector3 (UnityEngine.Random.Range(-1, 1), 0);
        Instantiate(healthCollectible, transform.position + randomPosition, transform.rotation);
        }

        int randomNumber2 = UnityEngine.Random.RandomRange(2, 4);
        for (int i = 0; i < randomNumber2; i ++);
        {   Vector3 randomPosition = new Vector3 (UnityEngine.Random.Range(-1, 1), 0);
        Instantiate(cogCollectible, transform.position + randomPosition, transform.rotation);
        }
        
        Destroy(gameObject);
    }
}
