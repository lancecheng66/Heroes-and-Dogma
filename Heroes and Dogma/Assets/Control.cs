using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour {
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    public float movementSpeed;
    private bool attack;
    private bool slide;
    private bool crouch;
    private bool facingRight;
    public Transform[] groundPoints; //points on the characters shoes for him to know if he is standing on solid ground
    public float groundRadius;
  
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool jump;
    public bool airCOntrol;
    public float jumpForce;

    //controls
    public KeyCode hit;


    // Use this for initialization
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleInput();
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // "HORIZONTAL" is the name of a unity feature for movement control. You can see it in Edit>Project Settings>Input.
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleAttacks();
        ResetValues();
    }


    //METHODS:

    private void HandleMovement(float horizontal) // The horizontal in the parenthesis gets its value from the float Horizontal = blah blah in the fixed update
    {
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) // to stop character from moving when attacking
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); // declaring verctor 2 means you have to put (x,y) values in the parenthesis
        }
        if (isGrounded && jump)
        {
            isGrounded = false; //isGrounded to false while jumping
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            Debug.Log("jump function executed");
        }
        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("Slide", true); //tells unity that the slide box in animator is checked.
            myAnimator.SetBool("Crouch", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("Slide", false);
            myAnimator.SetBool("Crouch", false);

        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal)); //to check if character is moving. For use in walk animation
    }

    private void HandleAttacks()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) // to check if attack animation is pressed AND character is not attacking at the moment
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero; //keep character from moving when attacking
            Debug.Log("Attack function executed");
        }

        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) // to check if attack animation is pressed AND character is not attacking at the moment
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero; //keep character from moving when attacking
            Debug.Log("Attack function executed");
        }
    }
    private void HandleInput() // where we put in controls (we can use this to make 2-3 player games
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            slide = true;
            crouch = true;
        }
        if (Input.GetKeyDown(hit))
        {
            attack = true;
        }

    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    private void ResetValues()
    {
        attack = false;
        slide = false;
        jump = false;
        crouch = false;
  

    }
    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
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
}

