using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedEnemyController : MonoBehaviour
{
   public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public ParticleSystem sparkEffect;
    AudioSource enemyAudio;
    public AudioClip fixAudioClip;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;
    //public GameObject hitEffect;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        latestDirectionChangeTime = 0f;
        calculateNewMovementVector();
    }

    void calculateNewMovementVector()    
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
     }



    void Update()
    {
         if (Time.time - latestDirectionChangeTime > directionChangeTime)
        { 
            latestDirectionChangeTime = Time.time;
            calculateNewMovementVector();
        }
        
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
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if(!broken)
        {
            return;
        }
        
        Vector2 position = rigidbody2D.position;
    
        rigidbody2D.MovePosition(position);

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        

        if (player != null)
        {
            player.ChangeHealth(-3);
            
        }
    }
    public void Fix()
    {
        smokeEffect.Stop();
        sparkEffect.Stop();
        Debug.Log("Fixed!");
        broken = false;
        rigidbody2D.simulated = false;
        //animator.SetTrigger("Fixed");
        RobotsFixed.instance.FixRobot();
        enemyAudio.Stop();
        enemyOneShot(fixAudioClip);
       

    }
     public void enemyOneShot(AudioClip clip)
    {
        enemyAudio.PlayOneShot(clip);
    }
}