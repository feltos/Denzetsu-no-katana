using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnnemies : AI_Manager
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
    BoxCollider2D[] hitZones = new BoxCollider2D[3];
    bool hit = false;
    float hitTimer;
    const float hitPeriod = 0.1f;
    float cooldown;
    const float cooldownPeriod = 1f;
	
	void Start ()
    {
        player = GameObject.Find("Player");
    }
   
	void Update ()
    {
        Debug.Log(cooldown);
        direction = (player.transform.position - transform.position).normalized;
        movement = new Vector2(speed * direction.x, 0.0f);
        cooldown += Time.deltaTime;

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
            if(cooldown >= cooldownPeriod)
            {
                hitTimer = 0.0f;
                cooldown = 0.0f;
                hit = true;
                AttackDirection();
            }         
        }
    }

    void FixedUpdate()
    {
        if (detect && Vector3.Distance(transform.position, player.transform.position) > minRange)
        {
            body.velocity = movement;
        }
        if(hit)
        {
            hitTimer += Time.deltaTime;
        }  
        if(hitTimer >= hitPeriod)
        {
            for (int i = 0; i < hitZones.Length; i++)
            {
                hitZones[i].enabled = false;
            }
            hit = false;
        }
                 
    }

    void AttackDirection()
    { 
        if (player.transform.position.x < transform.position.x)
        {
            hitZones[0].enabled = true;
        }
        if (player.transform.position.x > transform.position.x)
        {
            hitZones[1].enabled = true;
        }
        if (player.transform.position.y > transform.position.y)
        {
            hitZones[2].enabled = true;
        }
    }
    
}
