using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    AudioSource enemyAudio;
    public AudioClip fixAudioClip;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;
   //public GameObject cogCollision; 
    
    
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if(!broken)
        {
            return;
        }
        
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    
    void FixedUpdate()
    {
        if(!broken)
        {
            return;
        }
        
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController >();
        //Instantiate(hitEffect,transform.position,transform.rotation);

        if (player != null)
        {
            player.ChangeHealth(-1);
        }

    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        Debug.Log("Fixed!");
        RobotsFixed.instance.FixRobot();
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        enemyAudio.Stop();
        enemyOneShot(fixAudioClip);
       
    }
      public void enemyOneShot(AudioClip clip)
    {
        enemyAudio.PlayOneShot(clip);
    }
    }

