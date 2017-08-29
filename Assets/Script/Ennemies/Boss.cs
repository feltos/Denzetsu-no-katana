using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

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

    float cooldownBeforeShoot;
    const float periodBeforeShoot = 2f;

    float cooldownBeforeSwitchAttack;
    const float periodBeforeSwitchAttack = 3f;

    float cooldownBeforeMove;
    const float periodBeforeMove = 1.5f;

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
    SkeletonAnimation anim;
    float laserTimer;
    const float laserPeriod = 1f;
    [SerializeField]
    BoxCollider2D hitZone;

    enum State
    {
        MOVING,
        DISTANCE,
        CHARGE,
        STUN,
        WIN,
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
                anim.AnimationName = "Marche";
                if (cooldownBeforeSwitchAttack >= periodBeforeSwitchAttack)
                {
                    state = (State)Random.Range(1,3);
                    cooldownBeforeMove = 0.0f;
                    cooldownBeforeSwitchAttack = 0.0f;
                    stuckTimer = 0.0f;
                    distanceAttack = true;
                    rightCharge = false;
                    leftCharge = false;
                    laserTimer = 0.0f;
                }
                break;

            case State.DISTANCE:
                cooldownBeforeMove += Time.deltaTime;
                laserTimer += Time.deltaTime;
                anim.AnimationName = "Attaque_Rayon";
                if(laserTimer >= laserPeriod)
                {
                    hitZone.enabled = true;
                }
                if(cooldownBeforeMove >= periodBeforeMove)
                {
                    hitZone.enabled = false;
                    distanceAttack = false;
                    state = State.MOVING;
                }
                break;

            case State.CHARGE:
                charging = true;
                anim.AnimationName = "Charge";
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
                    state = State.STUN;
                }
                
                break;
            case State.STUN:
                anim.AnimationName = "Etourdissement";
                stuckTimer += Time.deltaTime;
                if (stuckTimer >= stuckPeriod)
                {
                    charging = false;
                    distanceAttack = false;
                    hitWall = false;
                    state = State.MOVING;
                }
                break;

            case State.WIN:
                anim.AnimationName = "Jean_Claude Van DAB (victoire Boss)";
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
        if (state == State.CHARGE && rightCharge)
        {
            body.velocity = new Vector2(rightWallDirection.x * chargeSpeed, 0.0f);
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isTurnedRight = !isTurnedRight;
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
            anim.AnimationName = "Hit";
        }
    }
}
