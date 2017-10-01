using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character1
{
    private IEnemyState currentState;
	
    // Use this for initialization
	public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState.Execute();
	}
    public void ChangeState(IEnemyState newState)
    {
        if (currentState !=null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        MyAnimator.SetFloat("Speed", 1);
        transform.Translate(GetDirection() * movementSpeed * (Time.deltaTime));
    }
    public Vector2 GetDirection() 
    {
        return facingRight ? Vector2.right : Vector2.left; //this is the short vestion of an if statement. If facing right> Vector2.right, if facing left>vector2.left
        
    }
}
