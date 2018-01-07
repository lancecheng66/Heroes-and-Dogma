using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration;

    public void Enter(Enemy enemy)
    {
        idleDuration = UnityEngine.Random.Range(1, 10);
        this.enemy = enemy;
    }

    public void Execute()
    {
       
        Idle();
        if (enemy.Target != null)
        {
            Debug.Log("Player Detected");
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag =="PlayerProjectile")
        {
            enemy.Target = GameObject.FindWithTag("Player");
        }
    }
    private void Idle()
    {
        enemy.MyAnimator.SetFloat("Speed", 0);
        idleTimer += Time.deltaTime;

        if (idleTimer>= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
