using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character1
{

    private IEnemyState currentState;
    public GameObject Target { get; set;}

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float throwRange;

    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

         public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            return false;

        }
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    [SerializeField]
    protected Transform ProjectilePos;


    [SerializeField]
    GameObject ProjectilePrefab;

    protected Enemy()
    {
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        Control.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
        // Physics2D.IgnoreLayerCollision(9,9); Keeps enemies from colliding with each other
    }


    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target !=null)
        {
        float xDir = Target.transform.position.x - transform.position.x;

            if (xDir <0 && facingRight ||xDir >0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if(!TakingDamage)
            {
                currentState.Execute();
            }
            currentState.Execute();
            LookAtTarget();
        }
    }
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x)|| (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                MyAnimator.SetFloat("Speed", 1);
                transform.Translate(GetDirection() * movementSpeed * (Time.deltaTime));
            }
            else if (currentState is PatrolState)
            {
                ChangeDirection();
            }
        }
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left; //this is the short vestion of an if statement. If facing right> Vector2.right, if facing left>vector2.left

    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }
    public override void ThrowKnife(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(ProjectilePrefab, ProjectilePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<ThrowAxe>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(ProjectilePrefab, ProjectilePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<ThrowAxe>().Initialize(Vector2.left);

        }
    }

    public override IEnumerator TakeDamage()
    {
        Debug.Log("hurt");
        health -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            yield return null;
            Physics2D.IgnoreLayerCollision(8, 9); // stops collision with enemies
            Physics2D.IgnoreLayerCollision(8, 8); // stops collision with other players
        }
    }

    public override void Death()
    {
        
        Destroy(gameObject);
    }
}

