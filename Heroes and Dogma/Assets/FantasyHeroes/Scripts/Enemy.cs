using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character1
{
    private IEnemyState currentState;
    public GameObject Target { get; set;}

    [SerializeField]
    protected Transform ProjectilePos;


    [SerializeField]
    GameObject ProjectilePrefab;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
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
        currentState.Execute();
        LookAtTarget();
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
            MyAnimator.SetFloat("Speed", 1);
            transform.Translate(GetDirection() * movementSpeed * (Time.deltaTime));
        }
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left; //this is the short vestion of an if statement. If facing right> Vector2.right, if facing left>vector2.left

    }
    void OnTriggerEnter2D(Collider2D other)
    {
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

}

