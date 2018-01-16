using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sven : Control
{
  
    [SerializeField]
    protected Transform ShieldPos;

    [SerializeField]
    public GameObject ShieldPrefab;

    [SerializeField]
    protected Transform ChastisePos;

    [SerializeField]
    public GameObject ChastisePrefab;

    public override void HandleInput() // where we put in controls (we can use this to make 2-3 player games
    {
        if (Input.GetButtonDown("Jump_P3"))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetButton("Crouch_P3"))
        {
            MyAnimator.SetBool("crouch", true);
        }

        if (Input.GetButtonDown("Attack_P3"))
        {
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetButtonDown("Skill1_P3"))
        {
            MyAnimator.SetTrigger("throw");
        }
        if (Input.GetButtonDown("Skill2_P3"))
        {
            MyAnimator.SetTrigger("cast");
        }

        if (Input.GetButtonDown("Skill3_P3"))
        {
            MyAnimator.SetTrigger("slide");
        }

    }

    public override void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal_P3"); // "HORIZONTAL" is the name of a unity feature for movement control. You can see it in Edit>Project Settings>Input.
            OnGround = IsGrounded();
            HandleMovement(horizontal);
            Flip(horizontal);
            Physics2D.IgnoreLayerCollision(10, 11);
        }
    }

    public override void ThrowKnife(int value)
    {
        Physics2D.IgnoreLayerCollision(10, 11);
        if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(ShieldPrefab, ShieldPos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                tmp.GetComponent<ShieldBoomerang>().Initialize(Vector2.right); 
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(ShieldPrefab, ShieldPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp.GetComponent<ShieldBoomerang>().Initialize(Vector2.left); 

            }
    }

    public void Skill2()
    {
        Physics2D.IgnoreLayerCollision(10, 11);
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(ChastisePrefab, ChastisePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Chastise>().Initialize(Vector2.down);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(ChastisePrefab, ChastisePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Chastise>().Initialize(Vector2.down);

        }
    }
}

