﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spine.Unity;

public class PlayerCharacter : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    float horizontalMovement;
    [SerializeField] float speed;
    float basicSpeed;
    [SerializeField] bool IsTurnedRight = true;

    [SerializeField]
    BoxCollider2D[]hitZones = new BoxCollider2D[4];
    float hitTimer = 0.0f;
    const float hitPeriod = 0.1f;

    bool hit = false;

    [SerializeField]GroundCheck groundCheck;
    [SerializeField]
    float jump;
    float basicGravityScale;

    Vector3 checkPosition;
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    float health;
    [SerializeField]BoxCollider2D m_MainBox;

    static string previousLevel = null;

    bool OnWall = false;

    public bool goToBossBattle = false;

    [SerializeField]float knockback;
    [SerializeField]float knockbackLength;
    [SerializeField]float knockbackCount;
    [SerializeField]bool knockFromRight;
    bool getKnockback = false;
    float knockbackTimer;
    const float knockbackPeriod = 0.5f;

    [SerializeField]
    Slider healthBar;

    float walkDeadZone = 0.01f;
    [SerializeField]
    Animator playerAnim;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        checkPosition = transform.position;
        basicSpeed = speed;
        basicGravityScale = body.gravityScale;
    }

    void Update()
    {
        if(getKnockback)
        {
            knockbackTimer += Time.deltaTime;
            if(knockbackTimer >= knockbackPeriod)
            {
                getKnockback = false;
            }
        }
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        horizontalMovement = 
                    speed * horizontal;

        if (horizontal > 0 && !IsTurnedRight)
        {
            Flip();
        }
        if(horizontal < 0 && IsTurnedRight)
        {
            Flip();
        }
        if(horizontal > 0 || horizontal < 0)
        {
            playerAnim.SetInteger("Course", 1);
        }
        if(horizontal == 0 && groundCheck.GetGroundedValue() >= 1)
        {
            playerAnim.SetInteger("Course", 0);
        }
        if(Input.GetButtonDown("Fire1"))
        {
            playerAnim.SetInteger("Attaque", 1);
            hit = true;
            hitTimer = 0.0f;
            hitZones[0].enabled = true;               
        }
       if(Input.GetButtonDown("Jump") && groundCheck.GetGroundedValue() >= 1)
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            playerAnim.SetInteger("Saut", 1);
        }
        if (Input.GetButtonDown("Jump") && OnWall)
        {
            speed = basicSpeed;
            body.gravityScale = basicGravityScale;
            body.velocity = new Vector2(horizontalMovement, jump);
            OnWall = false;
            playerAnim.SetInteger("Walljump", 0);
        }
     
        if(health <= 0)
        {
            LoadLevel(name);
        }
        if(goToBossBattle)
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
            speed = 0;
        }
    }
    void FixedUpdate()
    {
        if(knockbackCount <= 0)
        {
            body.velocity = new Vector2(horizontalMovement, body.velocity.y);
        }
        else
        {
            if(knockFromRight)
            {
                body.velocity = new Vector2(-knockback,knockback);
            }
            if(!knockFromRight)
            {
                body.velocity = new Vector2(knockback, knockback);
            }
            knockbackCount -= Time.deltaTime;
        }
        if (hit)
        {
            hitTimer += Time.deltaTime;
        }
    if(hitTimer > hitPeriod)
    {
        for (int i = 0; i < hitZones.Length; i++)
        {
            hitZones[i].enabled = false;
            playerAnim.SetInteger("Attaque", 0);
        }
        hit = false;
    }
}
void OnCollisionEnter2D(Collision2D collider)
{
    if(collider.gameObject.layer == LayerMask.NameToLayer("Wall") && groundCheck.GetGroundedValue() <= 0)
    {            
        body.gravityScale = 0;
        body.velocity = Vector2.zero;
        speed = 0;
        OnWall = true;
        playerAnim.SetInteger("Walljump", 1);
    }
    if(collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    {
        KnockbackEffect(collider.gameObject);
    }
}

void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.gameObject.layer == LayerMask.NameToLayer("DeathZone"))
    {
        gameManager.Restart();
    }

    if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet") && collision.IsTouching(m_MainBox))
    {
        KnockbackEffect(collision.gameObject);   
        Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("LaserBeam"))
        {
            KnockbackEffect(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossBattle"))
        {
            goToBossBattle = true;
            gameManager.StartFadeOut();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")&& collision.IsTouching(m_MainBox))
        {
            KnockbackEffect(collision.gameObject);
        }
    }

   void LoadLevel(string name)
    {
        previousLevel = SceneManager.GetActiveScene().name;
        if(previousLevel != null)
        {
            SceneManager.LoadScene(previousLevel);
        }
    }

    public void BasicVelocity()
    {
        speed = basicSpeed;
        body.gravityScale = basicGravityScale;
        body.velocity = new Vector2(horizontalMovement, jump);
    }
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        IsTurnedRight = !IsTurnedRight;
    }

    void KnockbackEffect(GameObject collision)
    {
        knockbackTimer = 0.0f;
        if(!getKnockback)
        {
            getKnockback = true;
            health -= 1;
            healthBar.value = health;
            knockbackCount = knockbackLength;
            if (collision.transform.position.x > transform.position.x)
            {
                knockFromRight = true;
            }
            else
            {
                knockFromRight = false;
            }
        }   
    }
}

