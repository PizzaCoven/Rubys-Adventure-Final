using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotsFixed : MonoBehaviour
{
    public int robotsFixed = 0;
    public static RobotsFixed instance;
    //public GameObject JambiUI;
   

    // Start is called before the first frame update
    void Start()
    {
        //JambiUI.SetActive(false);
        instance = this;
    }

    public void FixRobot() 
    {
        robotsFixed = robotsFixed + 1;
        GetComponent<Text>().text = "Robots Fixed:" + robotsFixed.ToString() + "/6";
        
        if(robotsFixed == 6)
        {
        //JambiUI.SetActive(true);
        StartCoroutine(WaitForSec());
        }
        IEnumerator WaitForSec()
        { 
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("MainScene2");
            //Player.transform.position = new Vector4(-12.85f,3.85f,494.55f);
            
            //Destroy(JambiUI);
        }

}
}
