using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jarred : Control
{
    GameObject tmp;
    public Vector3 telepoint;

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
    public override void ThrowKnife(int value)
    {
        Physics2D.IgnoreLayerCollision(10, 11);

        if (facingRight)
        {
            tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.right);
        }
        else
        {
            tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Initialize(Vector2.left);
        }
        
    }



    public void Teleport()
    {
        transform.position = tmp.transform.position;
        Destroy(tmp);
    }
}

