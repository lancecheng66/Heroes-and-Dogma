﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration=5f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("I'm Idling");
        Idle();
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
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
