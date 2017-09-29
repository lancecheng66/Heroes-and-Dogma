using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : Character1
{

    private static Control instance;
    public static Control Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Control>();
            }
        return instance;
        }
    }

   
   
    

    public Transform[] groundPoints; //points on the characters shoes for him to know if he is standing on solid ground
    public float groundRadius;
  
    public LayerMask whatIsGround;

    
    public bool airControl;
   
    public float jumpForce;
    public Rigidbody2D MyRigidbody { get; set; }
    
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool Crouch { get; set; }

    private Vector2 startPos;


    // Use this for initialization
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        
        MyRigidbody = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        HandleInput();
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal_P1"); // "HORIZONTAL" is the name of a unity feature for movement control. You can see it in Edit>Project Settings>Input.
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
       
    }


    //METHODS:

    private void HandleMovement(float horizontal) // The horizontal in the parenthesis gets its value from the float Horizontal = blah blah in the fixed update
    {
       
        if(!Attack && (OnGround||airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }

        if (Jump && MyRigidbody.velocity.y==0)
        {
            MyRigidbody.AddForce(new Vector2(horizontal * movementSpeed, jumpForce));
        }
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Crouch)
        {
            MyRigidbody.velocity = new Vector2(0, MyRigidbody.velocity.y);
        }

    }

    
    private void HandleInput() // where we put in controls (we can use this to make 2-3 player games
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            myAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            myAnimator.SetTrigger("slide");
        }
        if (Input.GetKey(KeyCode.S))
        {
            myAnimator.SetBool("crouch", true);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            myAnimator.SetTrigger("throw");
        }

    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    
    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                
                for (int i=0;i<colliders.Length;i++)
                {
                    if (colliders[i].gameObject !=gameObject)
                    {
                        return true;
                    }
                    
                }
            }
        }
        return false;
    }
    public override void ThrowKnife (int value)
    {
        base.ThrowKnife(value);
    }
    
}

