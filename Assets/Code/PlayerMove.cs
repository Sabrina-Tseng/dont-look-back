using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{    
    Rigidbody2D rb;

    //game manager
    private GameManager gameManager;

    //move
    public int speed = 5;
    public int jumpForce = 1000;
    public int jumpForceSmall = 200;
    public bool grounded;
    private int dir = 1;
    
    public LayerMask ground;
    public Transform feet;
    public Transform fog;

    //hurt
    public bool hurt = false;
    public bool inFog = false;

    //hp
    public float hp = 100;
    
    void Start()
    {    
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float xSpeed1 = Input.GetAxis("Horizontal1") * speed;
        float xSpeedSlow1 = Input.GetAxis("Horizontal1") * speed * 0.2f;
        float xSpeed2 = Input.GetAxis("Horizontal2") * speed;
        float xSpeedSlow2 = Input.GetAxis("Horizontal2") * speed * 0.2f;
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
        if(Input.GetAxis("Horizontal1") < 0) 
        {
            if(Input.GetAxis("Horizontal2") < 0)
            {
                //same direction
                rb.velocity = new Vector2(xSpeed1, rb.velocity.y);
                xSpeed = xSpeed1;
            }
            else if(Input.GetAxis("Horizontal2") > 0)
            {
                //opposite direction
            }
            else
            {
                //only player one pressed
                rb.velocity = new Vector2(xSpeedSlow1, rb.velocity.y);
                xSpeed = xSpeedSlow1;
            }
        }
        if(Input.GetAxis("Horizontal1") > 0) 
        {
            if(Input.GetAxis("Horizontal2") > 0)
            {
                rb.velocity = new Vector2(xSpeed1, rb.velocity.y);
                xSpeed = xSpeed1;
            }
            else if(Input.GetAxis("Horizontal2") < 0)
            {
            
            }
            else
            {
                rb.velocity = new Vector2(xSpeedSlow1, rb.velocity.y);
                xSpeed = xSpeedSlow1;
            }        
        }
        if(Input.GetAxis("Horizontal2") < 0) 
        {
            if(Input.GetAxis("Horizontal1") < 0)
            {
                rb.velocity = new Vector2(xSpeed2, rb.velocity.y);
                xSpeed = xSpeed2;
            }
            else if(Input.GetAxis("Horizontal1") > 0)
            {
            
            }
            else
            {
                rb.velocity = new Vector2(xSpeedSlow2, rb.velocity.y);
                xSpeed = xSpeedSlow2;
            }
        }
        if(Input.GetAxis("Horizontal2") > 0) 
        {
            if(Input.GetAxis("Horizontal1") > 0)
            {
                rb.velocity = new Vector2(xSpeed2, rb.velocity.y);
                xSpeed = xSpeed2;
            }
            else if(Input.GetAxis("Horizontal1") < 0)
            {
            
            }
            else
            {
                rb.velocity = new Vector2(xSpeedSlow2, rb.velocity.y);
                xSpeed = xSpeedSlow2;
            }        
        }
        
        //direction
        if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2(-1, 1);
            dir *= -1;
        }

        //jump
        grounded = Physics2D.OverlapCircle(feet.position, .5f, ground); 
        // anim.SetBool("Grounded", grounded);

        if(Input.GetButtonDown("Jump1") && grounded && Input.GetButtonDown("Jump2")) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce));
        }
        else if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForceSmall));
        }

        float fogDistance = Vector3.Distance (fog.transform.position, this.transform.position);
        // print(fogDistance);
        if (fogDistance < 1)
        {
            // print("hit");
            // StartCoroutine(FogHit());

            if (hp > 60)
            {
                hp -= 0.15f;
            }
            else if (hp > 30)
            {
                hp -= 0.1f;
            }
            else if (hp > 10)
            {
                hp -= 0.01f;
            }
            else
            {
                hp -= 0.005f;
            }

            // hp -= 0.01f;

            float hpPercent = hp/100;
            float hpOpacity = 1 - hpPercent;
            gameManager.HpDown(hpOpacity);
        }
        // //test
        // if(Input.GetButtonDown("Fire1"))
        // {
        //     print("hit");
        //     StartCoroutine(FogHit());
        // }

    }

}
