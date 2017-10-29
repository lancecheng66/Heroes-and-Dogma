using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandlerKayla();
public class Kayla : Control
{
    [SerializeField]
    protected Transform AsteroidPos;

    [SerializeField]
    public GameObject AsteroidPrefab;

    [SerializeField]
    protected Transform boltPos;

    [SerializeField]
    public GameObject boltPrefab;


    public override void HandleInput() // where we put in controls (we can use this to make 2-3 player games
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            MyAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            MyAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKey(KeyCode.K))
        {
            MyAnimator.SetBool("crouch", true);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            MyAnimator.SetTrigger("throw");
        }

    }

    public override void ThrowKnife(int value)
    {
        Physics2D.IgnoreLayerCollision(10, 11);
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(AsteroidPrefab, AsteroidPos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Asteroid>().Initialize(Vector2.right); //change knife to fireball so that you can code different behavior for explosions
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(AsteroidPrefab, AsteroidPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Asteroid>().Initialize(Vector2.left); //change knife to fireball so that you can code different behavior for explosions

        }
    }

    public override void MeleeAttack()
    {
        Physics2D.IgnoreLayerCollision(10, 11);
       
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(boltPrefab, boltPos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                tmp.GetComponent<Bolt>().Initialize(Vector2.right); //change knife to fireball so that you can code different behavior for explosions
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(boltPrefab, boltPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp.GetComponent<Bolt>().Initialize(Vector2.left); //change knife to fireball so that you can code different behavior for explosions

            }
        
    }
}

