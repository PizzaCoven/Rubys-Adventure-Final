﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
 {
    public float speed = 3.0f;
    public int maxHealth = 5;
    public GameObject projectilePrefab;
    GameObject Player;
    public AudioClip throwSound;
    public AudioClip hitSound;
    //public AudioClip collectedClip;
    //public GameObject twinkleEffect;


    //public AudioClip dieSound;
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    public AudioClip winSound;
    AudioSource audioSource;
    public GameObject hitEffect;
    public AudioClip findSound;
    public int currentAmmo = 6;
    public int maxAmmo = 6; 
   
    public Text ammoText;
    
   
    
  
    
    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
        //RobotsFixed.instance.FixRobot();
        
        audioSource = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey("escape"))
           { Application.Quit();}
    
       

    IEnumerator Reset(float Count)
    {
        yield return new WaitForSeconds(Count);
        yield return null;
    }
        if(health <= 0)
        {
         //PlaySound(dieSound);
         Invoke("Restart", 3.0f);
         
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("RubyFinal2");
        }

        if((Input.GetKeyDown(KeyCode.C) && currentAmmo > 0))
        {
            Launch();
            currentAmmo -= 1;
        }
        ammoText.GetComponent<Text>().text = currentAmmo.ToString();
    

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
               if (character != null)
                {
                    character.DisplayDialog();}
            }
        }


//
        if (Input.GetKeyDown(KeyCode.X))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                    {
                    
                        Debug.Log(RobotsFixed.instance.robotsFixed);
                        if(RobotsFixed.instance.robotsFixed == 6)
                        {
                        character.DisplayDialog2();
                        StartCoroutine(WaitForSec());
                        }
                    IEnumerator WaitForSec()
                    { 
                        yield return new WaitForSeconds(4);
                        SceneManager.LoadScene("RubyFinal2");
                    }
                }
            }
            }
            
//

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Cube"));
                if (hit.collider != null)
            {
                ResourceCube rc = hit.collider.GetComponent<ResourceCube>();
                    if (rc != null)
                        {
                            PlaySound(findSound);
                            rc.BlownUp();
                            
                        }
            }
        }
    }
    
    

       public void ChangeAmmo(int amount)
        {   
        currentAmmo += 3;
    
        }
   

     void FixedUpdate()
    {
        
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    //
       
    Debug.Log(RobotsFixed.instance.robotsFixed);
     if(RobotsFixed.instance.robotsFixed == 1)
         { 
          //GameObject.SetActive(false);
         PlaySound(winSound);
           }

           //
    }
    void Restart()
        {
            StartCoroutine(WaitForSec());
                        
                    IEnumerator WaitForSec()
                    { 
                        yield return new WaitForSeconds(3);
                        SceneManager.LoadScene("RubyFinal");}
        }



     public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            PlaySound(hitSound);
            Instantiate(hitEffect,transform.position,transform.rotation);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    
    void Launch()
    {
        if (currentAmmo > 0)
        {GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
        }

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    
}
