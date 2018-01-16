using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Chastise : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

    float destroyTime = 1.5f;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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

    public void OnTriggerEnter2D(Collider2D other)
    {
       
    }
}
