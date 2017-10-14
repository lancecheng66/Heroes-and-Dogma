using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowAxe : MonoBehaviour
{
    public float throwforce;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

    
    
    


    // Use this for initialization
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
       if (transform.localRotation.z>0)
        myRigidbody.AddForce(new Vector2(-1,2)*throwforce, ForceMode2D.Impulse);
       else
            myRigidbody.AddForce(new Vector2(1, 2) * throwforce, ForceMode2D.Impulse);

    }

    void FixedUpdate()
    {

        //myRigidbody.velocity = (direction * speed);
       
        //myRigidbody.AddForce(transform.up*100);

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
      
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}