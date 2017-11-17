using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    Vector3 telepoint = new Vector3 (0,0,0);

    public float speed;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;


    // Use this for initialization
    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    void OnBecameInvisible()
    {
        //transform.position = telepoint;
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        //transform.position = telepoint;
        if (other.gameObject.tag != "Player")
        { Destroy(gameObject); }
    }
}
