using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    float horizontalMovement;
    [SerializeField] float speed;
    [SerializeField] bool IsTurnedRight = true;

    [SerializeField]
    BoxCollider2D[]hitZones = new BoxCollider2D[4];
    float hitTimer = 0.0f;
    const float hitPeriod = 0.1f;
    bool hit = false;

    [SerializeField]GroundCheck groundCheck;
    [SerializeField]
    float jump;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();       
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
            body.velocity = new Vector2(body.velocity.x, jump );
        }
        Debug.Log(groundCheck.GetGroundedValue());
    }
    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalMovement,body.velocity.y) ;

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
}

