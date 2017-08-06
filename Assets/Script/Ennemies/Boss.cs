using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    [SerializeField]
    float speed;
    float basicSpeed;
    [SerializeField]
    float chargeSpeed;
    Vector2 playerDirection;
    Vector2 movement;
    [SerializeField]
    Rigidbody2D body;
    GameObject player;
    [SerializeField]bool isTurnedRight = false;

    [SerializeField]
    float minRange;
    [SerializeField]
    BoxCollider2D hitZone;
    float cooldownBeforeCac;
    const float periodBeforeCac = 1f;

    float cooldownBeforeShoot;
    const float periodBeforeShoot = 2f;

    float cooldownBeforeSwitchAttack;
    const float periodBeforeSwitchAttack = 3f;

    float cooldownBeforeMove;
    const float periodBeforeMove = 1f;

    bool hit = false;
    float hitTimer;
    const float hitPeriod = 0.2f;

    [SerializeField]
    GameObject shootZone;
    [SerializeField]
    GameObject bigBbullet;

    [SerializeField]
    GameObject leftWall;
    [SerializeField]
    GameObject rightWall;
    Vector2 leftWallDirection;
    Vector2 rightWallDirection;
    bool hitWall = false;
    float stuckTimer;
    const float stuckPeriod = 3f;
    bool charging = false;
    bool rightCharge = false;
    bool leftCharge = false;

    bool distanceAttack = false;
    [SerializeField]
    BoxCollider2D mainBox;


    [SerializeField]
    float health;
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject katana;

    enum State
    {
        MOVING,
        CAC,
        DISTANCE,
        CHARGE,
        DEAD
    }
    State state = State.MOVING;

    void Start ()
    {
        basicSpeed = speed;
    }
	
	void Update ()
    {
        player = GameObject.Find("Player");
        Debug.Log(player.transform.position.x - transform.position.x);
        playerDirection = (player.transform.position - transform.position).normalized;
        leftWallDirection = (leftWall.transform.position - transform.position).normalized;
        rightWallDirection = (rightWall.transform.position - transform.position).normalized;
        movement = new Vector2(basicSpeed * playerDirection.x, 0.0f);

        if (Vector3.Distance(transform.position, player.transform.position) <= minRange && !charging)
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
        if(health <= 0)
        {
            state = State.DEAD;
        }
        switch(state)
        {
            case State.MOVING:
                cooldownBeforeSwitchAttack += Time.deltaTime;
                speed = basicSpeed;
                if(cooldownBeforeSwitchAttack >= periodBeforeSwitchAttack)
                {
                    state = (State)Random.Range(2,4);
                    cooldownBeforeMove = 0.0f;
                    cooldownBeforeSwitchAttack = 0.0f;
                    stuckTimer = 0.0f;
                    distanceAttack = true;
                    rightCharge = false;
                    leftCharge = false;
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
                Instantiate(bigBbullet, shootZone.transform.position, transform.rotation);
                if(cooldownBeforeMove >= periodBeforeMove)
                {
                    distanceAttack = false;
                    state = State.MOVING;
                }
                break;

            case State.CHARGE:
                charging = true;
                for(int i = 0; i < 1; i++)
                {
                    if (isTurnedRight)
                    {
                        rightCharge = true;

                    }
                    else
                    {
                        leftCharge = true;
                    }
                   
                }
                if(hitWall)
                {
                    stuckTimer += Time.deltaTime;
                }
                if(stuckTimer >= stuckPeriod)
                {
                    charging = false;
                    distanceAttack = false;
                    hitWall = false;
                    state = State.MOVING;
                }
                break;
            case State.DEAD:
                gameManager.StartFadeOut();
                break;
        }
    }

    void FixedUpdate()
    {
        if(state == State.MOVING)
        {
            body.velocity = movement;
        }
        if(state == State.CHARGE && leftCharge)
        {
            body.velocity = new Vector2(leftWallDirection.x * chargeSpeed, 0.0f);
        }
        if (state == State.CHARGE &&rightCharge)
        {
            body.velocity = new Vector2(rightWallDirection.x * chargeSpeed, 0.0f);
        }
        if (hit)
        {
            hitTimer += Time.deltaTime;
        }
        if (hitTimer >= hitPeriod)
        {
            hitZone.enabled = false;
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
        hitZone.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") && collision.IsTouching(mainBox) && state == State.CHARGE)
        {
            hitWall = true;
            body.velocity = Vector3.zero;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.isTrigger)
        {
            health -= 1;
            healthBar.value -= 1;
        }
    }
}
