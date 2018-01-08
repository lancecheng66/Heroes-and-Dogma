using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public float speed;


    private Rigidbody2D myRigidbody;

    private Vector2 direction;

    float destroyTime = 4f;


    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.AddTorque(1 * 1 * -100);
        //Physics2D.IgnoreLayerCollision(8, 11);
    }

    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    

}
