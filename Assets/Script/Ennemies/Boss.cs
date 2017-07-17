using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField]
    float speed;
    Vector2 direction;
    Vector2 movement;
    [SerializeField]
    Rigidbody2D body;
    GameObject player;
    bool isTurnedRight = false;

    [SerializeField]
    float minRange;
    [SerializeField]
    BoxCollider2D[] hitZones = new BoxCollider2D[2];
    float cooldownBeforeCac;
    const float periodBeforeCac = 1f;

    float cooldownBeforeShoot;
    const float periodBeforeShoot = 2f;

    float cooldownBeforeSwitchAttack;
    const float periodBeforeSwitchAttack = 3f;

    float cooldownBeforeMove;
    const float periodBeforeMove = 2f;

    bool hit = false;
    float hitTimer;
    const float hitPeriod = 0.2f;

    [SerializeField]
    GameObject shootZone;
    [SerializeField]
    GameObject laserBeam;
    

    bool distanceAttack = false;

    enum State
    {
        MOVING,
        CAC,
        DISTANCE,
        CHARGE
    }
    State state = State.MOVING;

    void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	void Update ()
    {
        
        direction = (player.transform.position - transform.position).normalized;
        movement = new Vector2(speed * direction.x, 0.0f);

        if (Vector3.Distance(transform.position, player.transform.position) <= minRange)
        {
            state = State.CAC;
        }
        if (Vector3.Distance(transform.position, player.transform.position) > minRange && !distanceAttack)
        {
            state = State.MOVING;
        }

        if (body.velocity.x > 0 && !isTurnedRight)
        {
            Flip();
        }
        if (body.velocity.x < 0 && isTurnedRight)
        {
            Flip();
        }
        switch(state)
        {
            case State.MOVING:
                cooldownBeforeSwitchAttack += Time.deltaTime;
                if(cooldownBeforeSwitchAttack >= periodBeforeSwitchAttack)
                {
                    state = (State)Random.Range(2,4);
                    cooldownBeforeMove = 0.0f;
                    cooldownBeforeSwitchAttack = 0.0f;
                    distanceAttack = true;
                }
                break;

            case State.CAC:
                cooldownBeforeCac += Time.deltaTime;
                if(cooldownBeforeCac >= periodBeforeCac)
                {
                    hitTimer = 0.0f;
                    hit = true;
                    AttackDirection();
                    cooldownBeforeCac = 0.0f;
                    state = State.MOVING;
                }
                break;

            case State.DISTANCE:
                cooldownBeforeMove += Time.deltaTime;
                Instantiate(laserBeam, shootZone.transform.position, transform.rotation);
                if(cooldownBeforeMove >= periodBeforeMove)
                {
                    distanceAttack = false;
                    cooldownBeforeSwitchAttack = 0.0f;
                    state = State.MOVING;
                }
                break;

            case State.CHARGE:
                break;
        }
    }

    void FixedUpdate()
    {
        if(state == State.MOVING)
        {
            body.velocity = movement;
        }
        if (hit)
        {
            hitTimer += Time.deltaTime;
        }
        if (hitTimer >= hitPeriod)
        {
            for (int i = 0; i < hitZones.Length; i++)
            {
                hitZones[i].enabled = false;
            }
            hit = false;
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isTurnedRight = !isTurnedRight;
    }

    void AttackDirection()
    {      
        hitZones[0].enabled = true;
    }
}
