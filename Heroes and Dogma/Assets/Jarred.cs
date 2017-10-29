using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jarred : Control
{
   

    public override void HandleInput() // where we put in controls (we can use this to make 2-3 player games
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKey(KeyCode.S))
        {
            MyAnimator.SetBool("crouch", true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            MyAnimator.SetTrigger("throw");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnimator.SetTrigger("cast");
        }

    }
   
   
}

