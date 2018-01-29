using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();
public class Control : Character1
{



    public GameObject currentCheckpoint;

    public event DeadEventHandler Dead;

    public Transform[] groundPoints; //points on the characters shoes for him to know if he is standing on solid ground
    public float groundRadius;
  
    public LayerMask whatIsGround;

    
    public bool airControl;
   
    public float jumpForce;

    private bool immortal = false;

    private SpriteRenderer spriteRenderer;


    [SerializeField]
    private float immortalTime;

    public Rigidbody2D MyRigidbody { get; set; }
    
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool Crouch { get; set; }

    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentValue<= 0)
            {
                OnDead();

            }
            return healthStat.CurrentValue <= 0;
        }
    }

    private Vector2 startPos;


    // Use this for initialization
    public override void Start()
    {
        base.Start();   
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();
        
        
    }
    private void Update()
    {
        if (!TakingDamage && !IsDead)
        {

            if (transform.position.y <= -14f)
            {
                Respawn();
            }
        }
        HandleInput();
    }
    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (!TakingDamage&&!IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal_P1"); // "HORIZONTAL" is the name of a unity feature for movement control. You can see it in Edit>Project Settings>Input.
            OnGround = IsGrounded();
            HandleMovement(horizontal);
            Flip(horizontal);
        }
    }

    public void OnDead()
    {
        if(Dead!= null)
        {
            
            Dead();
        }
    }
    //METHODS:

    public void HandleMovement(float horizontal) // The horizontal in the parenthesis gets its value from the float Horizontal = blah blah in the fixed update
    {
       
        if(!Attack && (OnGround||airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }

        if (Jump && MyRigidbody.velocity.y==0)
        {
            MyRigidbody.AddForce(new Vector2(horizontal * movementSpeed, jumpForce));
        }
        MyAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Crouch)
        {
            MyRigidbody.velocity = new Vector2(0, MyRigidbody.velocity.y);
        }

    }

    
    public virtual void HandleInput() // where we put in controls (we can use this to make 2-3 player games
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKey(KeyCode.S))
        {
            MyAnimator.SetBool("crouch", true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            MyAnimator.SetTrigger("throw");
        }

    }
    public void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    
    public bool IsGrounded()
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

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
                yield return new WaitForSeconds(.1f);

            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = true;
                yield return new WaitForSeconds(.1f);
            
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentValue -= 10;
            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;

            }

            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
                
            }
        }
    }

    public override void Respawn()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        healthStat.CurrentValue = healthStat.MaxVal;
        transform.position = currentCheckpoint.transform.position;
    }
}

