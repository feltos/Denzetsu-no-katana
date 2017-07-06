using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 direction;
    Vector2 movement;
    GameObject player;
    [SerializeField]float speed;
    bool hit = false;

    [SerializeField]
    GameManager gameManager;
	
	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        direction = (player.transform.position - transform.position).normalized;
    }
	
	
	void Update ()
    {      
        movement = new Vector2(speed * direction.x, 0.0f);
    }
    void FixedUpdate()
    {
        body.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.isTrigger && !hit)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        Vector3 theScale = transform.localScale;
        direction *= -1;
        theScale.x *= -1;
        transform.localScale = theScale;
        hit = true;
    }

    public bool GetHit()
    {
        return hit;
    }
}
