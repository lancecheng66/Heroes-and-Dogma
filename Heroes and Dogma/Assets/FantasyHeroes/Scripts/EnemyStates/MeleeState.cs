using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private float attackTimer;
    private float attackCoolDown = 5f;
    private bool canattack = true;

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if(enemy.InThrowRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
        else if (enemy.Target == null)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Attack()
    {

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            canattack = true;
            attackTimer = 0;
        }
        if (canattack)
        {
            canattack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
