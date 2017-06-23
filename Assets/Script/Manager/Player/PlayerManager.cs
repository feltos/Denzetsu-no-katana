using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    Vector2 horizontalMovement;
    [SerializeField] float speed;
    [SerializeField] bool IsTurnedRight = true;

    [SerializeField]
    BoxCollider2D[]hitZones = new BoxCollider2D[4];
    float hitTimer;
    bool hit = false;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();       
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        horizontalMovement = new Vector2(
                    speed * horizontal,0);
        

        if (horizontal > 0 && !IsTurnedRight)
        {
                IsTurnedRight = true;
        }
        if(horizontal < 0 && IsTurnedRight)
        {        
                IsTurnedRight = false;                
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            AttackDirection();               
        }       
      /*  for(int i = 0; i < hitZones.Length; i ++)
        {
            hitZones[i].enabled = false;
        }*/
    }
    void FixedUpdate()
    {
        body.velocity = horizontalMovement;
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

