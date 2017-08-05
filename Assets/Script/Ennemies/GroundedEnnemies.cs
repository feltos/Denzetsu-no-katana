using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnnemies : AI
{
    [SerializeField]
    float speed;
    Vector2 direction;
    Vector2 movement;
    [SerializeField]
    Rigidbody2D body;

    bool detect = false;
    [SerializeField]
    float minRange;
    [SerializeField]
    BoxCollider2D hitZone;
    bool hit = false;
    float hitTimer;
    const float hitPeriod = 0.1f;
    float cooldown;
    const float cooldownPeriod = 2f;
    bool isTurnedRight = false;
	
	void Start ()
    {
        player = GameObject.Find("Player");
    }
   
	void Update ()
    {
        direction = (player.transform.position - transform.position).normalized;
        movement = new Vector2(speed * direction.x, 0.0f);

        if (Vector3.Distance(transform.position, player.transform.position) < maxRange)          
        {
            detect = true;                  
        }
        if (Vector3.Distance(transform.position,player.transform.position ) > maxRange)
        {
            detect = false;
        }
        if(detect && Vector3.Distance(transform.position, player.transform.position) <= minRange && !hit)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= cooldownPeriod)
            {
                hitTimer = 0.0f;
                cooldown = 0.0f;
                hit = true;
                AttackDirection();
            }         
        }
        if(body.velocity.x > 0 && !isTurnedRight)
        {
            Flip();
        }
        if (body.velocity.x < 0 && isTurnedRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (detect && Vector3.Distance(transform.position, player.transform.position) > minRange && !fall)
        {
            body.velocity = movement;
        }
        if(hit)
        {
            hitTimer += Time.deltaTime;
        }  
        if(hitTimer >= hitPeriod)
        {
            hitZone.enabled = false;
            hit = false;
        }
                 
    }

    void AttackDirection()
    {        
        hitZone.enabled = true;          
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isTurnedRight = !isTurnedRight;
    }
}
