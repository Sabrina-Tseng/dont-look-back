using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{    
    Rigidbody2D rb;
    Animator anim;

    //game manager
    private GameManager gameManager;

    //move
    public float speed = 2.5f;
    public bool grounded;
    public bool struggle = false;
    //private int dir = 1;

    //jump
    public int jumpForce = 800;
    public int jumpForceSmall = 150;
    public float jumpTriggerTime = 0.25f;
    public float jumpCooldownTime = 0.25f;
    public bool jump1 = false;
    public bool jump2 = false;
    public bool jumping = false;

    //kick
    public bool kick1 = false;
    public bool kick2 = false;
    //public bool kicking = false;
    public float kickTriggerTime = 0.25f;
    //public float kickCooldownTime = 0.5f;

    //audio
    AudioSource _audioSource;
    public AudioClip struggleSound;
    
    //position
    public LayerMask ground;
    public Transform feet;
    public Transform fog;

    //hp
    public float hp = 100;

    void Start()
    {    
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        hp = 100;
    }

    void Update()
    {
        float xSpeed1 = Input.GetAxis("Horizontal1") * speed;
        float xSpeedSlow1 = Input.GetAxis("Horizontal1") * speed * 0.25f;
        float xSpeed2 = Input.GetAxis("Horizontal2") * speed;
        float xSpeedSlow2 = Input.GetAxis("Horizontal2") * speed * 0.25f;
        float xSpeed = 0;

        //movement
        // if(Input.GetAxis("Horizontal1") < 0 && Input.GetAxis("Horizontal2") < 0) 
        // {
        //     rb.velocity = new Vector2(xSpeed, rb.velocity.y);
        // }
        // if(Input.GetAxis("Horizontal1") > 0 && Input.GetAxis("Horizontal2") > 0) 
        // {
        //     rb.velocity = new Vector2(xSpeed, rb.velocity.y);
        // }

        if (xSpeed == 0)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("DragR", false);
            anim.SetBool("DragL", false);
        }


        //walk if not struggling
        if (!struggle)
        {
            if(Input.GetAxis("Horizontal1") < 0) 
            {
                if(Input.GetAxis("Horizontal2") < 0)
                {
                    //same direction
                    //fast go left
                    rb.velocity = new Vector2(xSpeed1, rb.velocity.y);
                    xSpeed = xSpeed1;
                    anim.SetBool("Walk", true);
                }
                else if(Input.GetAxis("Horizontal2") > 0)
                {
                    //opposite direction
                    StartCoroutine(Struggle());
                }
                else
                {
                    //only player one pressed
                    //slow go left
                    rb.velocity = new Vector2(xSpeedSlow1, rb.velocity.y);
                    xSpeed = xSpeedSlow1;
                    anim.SetBool("DragL", true);
                }
            }
            if(Input.GetAxis("Horizontal1") > 0) 
            {
                if(Input.GetAxis("Horizontal2") > 0)
                {
                    //fast go right
                    rb.velocity = new Vector2(xSpeed1, rb.velocity.y);
                    xSpeed = xSpeed1;
                    anim.SetBool("Walk", true);
                }
                else if(Input.GetAxis("Horizontal2") < 0)
                {
                    StartCoroutine(Struggle());
                }
                else
                {
                    //slow go right
                    rb.velocity = new Vector2(xSpeedSlow1, rb.velocity.y);
                    xSpeed = xSpeedSlow1;
                    anim.SetBool("DragR", true);
                }        
            }
            if(Input.GetAxis("Horizontal2") < 0) 
            {
                if(Input.GetAxis("Horizontal1") < 0)
                {
                    //fast go left
                    rb.velocity = new Vector2(xSpeed2, rb.velocity.y);
                    xSpeed = xSpeed2;
                    anim.SetBool("Walk", true);
                }
                else if(Input.GetAxis("Horizontal1") > 0)
                {
                    StartCoroutine(Struggle());
                }
                else
                {
                    //slow go left
                    rb.velocity = new Vector2(xSpeedSlow2, rb.velocity.y);
                    xSpeed = xSpeedSlow2;
                    anim.SetBool("DragL", true);
                }
            }
            if(Input.GetAxis("Horizontal2") > 0) 
            {
                if(Input.GetAxis("Horizontal1") > 0)
                {
                    //fast go right
                    rb.velocity = new Vector2(xSpeed2, rb.velocity.y);
                    xSpeed = xSpeed2;
                    anim.SetBool("Walk", true);
                }
                else if(Input.GetAxis("Horizontal1") < 0)
                {
                    StartCoroutine(Struggle());
                }
                else
                {
                    //slow go right
                    rb.velocity = new Vector2(xSpeedSlow2, rb.velocity.y);
                    xSpeed = xSpeedSlow2;
                    anim.SetBool("DragR", true);
                }        
            }
            
            //direction
            if (xSpeed < 0)
            {
                // transform.localScale *= new Vector2(-1, 1);
                //dir = -1;
                anim.SetBool("Backward", true);
            }
            if (xSpeed > 0)
            {
                //dir = 1;
                anim.SetBool("Backward", false);
            }


            //jump
            grounded = Physics2D.OverlapCircle(feet.position, .5f, ground); 
            anim.SetBool("Grounded", grounded);

            //jump if grounded
            if ( grounded )
            {
                //trigger
                if (Input.GetButtonDown("Jump1"))
                {
                    StartCoroutine(Jump1Trigger());
                }
                if (Input.GetButtonDown("Jump2"))
                {
                    StartCoroutine(Jump2Trigger());
                }

                //big jump
                if( jump1 && jump2 && !jumping ) 
                {
                    StartCoroutine(JumpCooldown());
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0,jumpForce));
                }
                //small jump
                else if ( ( jump1 && !jump2) || (jump2 && !jump1) )
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0,jumpForceSmall));
                }
            }

            //kick
            //trigger
            if (xSpeed == 0)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    print("fire1");
                    StartCoroutine(Kick1Trigger());
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    print("fire2");
                    StartCoroutine(Kick2Trigger());
                }
            }

        }
        
        //fog kill
        //float fogDistance = Vector3.Distance (fog.transform.position, this.transform.position);
        float fogDistance = this.transform.position.x - fog.transform.position.x;
        //print(fogDistance);
        if (fogDistance < 4)
        {
            hp -= 5f * Time.deltaTime;
            gameManager.HpDown(hp);
        }
        if (fogDistance < -1)
        {
            hp -= 20f * Time.deltaTime;
            gameManager.HpDown(hp);
        }
        if (hp <= 0)
        {
            hp = 100;
            gameManager.LoadLevel("Bad End");      
        }
    }

    //struggle
    IEnumerator Struggle()
    {
        struggle = true;
        anim.SetBool("Struggle",struggle);
        _audioSource.PlayOneShot(struggleSound);
        yield return new WaitForSeconds(1f);
        struggle = false;
        anim.SetBool("Struggle",struggle);
    }

    //jump
    IEnumerator Jump1Trigger()
    {
        jump1 = true;
        yield return new WaitForSeconds(jumpTriggerTime);
        jump1 = false;
    }
    IEnumerator Jump2Trigger()
    {
        jump2 = true;
        yield return new WaitForSeconds(jumpTriggerTime);
        jump2 = false;
    }
    IEnumerator JumpCooldown()
    {
        jumping = true;
        yield return new WaitForSeconds(jumpCooldownTime);
        jumping = false;
    }

    //kick
    IEnumerator Kick1Trigger()
    {
        kick1 = true;
        anim.SetBool("KickR",kick1);
        yield return new WaitForSeconds(kickTriggerTime);
        kick1 = false;
        anim.SetBool("KickR",kick1);
    }
    IEnumerator Kick2Trigger()
    {
        kick2 = true;
        anim.SetBool("KickL",kick2);
        yield return new WaitForSeconds(kickTriggerTime);
        kick2 = false;
        anim.SetBool("KickL",kick2);
    }

    
}
