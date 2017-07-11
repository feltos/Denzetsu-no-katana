using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        checkPosition = transform.position;
        basicSpeed = speed;
        basicGravityScale = body.gravityScale;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        horizontalMovement = 
                    speed * horizontal;

        if (horizontal > 0 && !IsTurnedRight)
        {
                IsTurnedRight = true;
        }
        if(horizontal < 0 && IsTurnedRight)
        {        
                IsTurnedRight = false;                
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = true;
            hitTimer = 0.0f;
            AttackDirection();                        
        }
       if(Input.GetButtonDown("Jump") && groundCheck.GetGroundedValue() >= 1)
        {
            body.velocity = new Vector2(body.velocity.x, jump);
        }
        if (Input.GetButtonDown("Jump") && OnWall)
        {
            speed = basicSpeed;
            body.gravityScale = basicGravityScale;
            body.velocity = new Vector2(horizontalMovement, jump);
            OnWall = false;           
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
                body.velocity = new Vector2(-knockback, knockback/2);
            }
            if(!knockFromRight)
            {
                body.velocity = new Vector2(knockback, knockback/2);
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
            }
            hit = false;
        }
    }
    void AttackDirection()
    {
        if(horizontal > 0)
        {
            hitZones[0].enabled = true;           
        }
        if (horizontal < 0)
        {
            hitZones[1].enabled = true;
        }
        if(vertical > 0)
        {
            hitZones[2].enabled = true;
        }
        if (vertical < 0)
        {
            hitZones[3].enabled = true;
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
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Enemy")|| collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            health -= 1;
            knockbackCount = knockbackLength;
            if(collider.transform.position.x > transform.position.x)
            {
                knockFromRight = true;
            }
            else
            {
                knockFromRight = false;
            }
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
            health -= 1;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("BossBattle"))
        {
            goToBossBattle = true;
            gameManager.StartFadeOut();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            health -= 1;
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
}

