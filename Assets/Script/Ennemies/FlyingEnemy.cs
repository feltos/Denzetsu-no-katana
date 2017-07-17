using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : AI
{
    Vector2 direction;
    Vector2 movement;
    [SerializeField]
    Rigidbody2D body;
    Vector3 previousCameraPos;
    SpriteRenderer spriteRenderer;
    float offsetX = 0.5f;
    float preparingTimer = 0.0f;
    float preparingCooldown = 1f;
    bool alreadyAttack = false;
    bool isTurnedRight = false;
	
    enum State
    {
        NOT_DETECTED,
        PREPARING,
        ATTACK
    }
    [SerializeField]State state = State.NOT_DETECTED;
	void Start ()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
		
	void Update ()
    {
        Vector3 deltaPos = Camera.main.transform.position - previousCameraPos;
        float rightCameraBoundX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float leftCameraBoundX = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        switch (state)
        {
            case State.NOT_DETECTED:
               
                if(transform.position.x + spriteRenderer.bounds.size.x/2 + offsetX < rightCameraBoundX)
                {
                    state = State.PREPARING;                           
                }
                break;
                                
            case State.PREPARING:
                preparingTimer += Time.deltaTime;
                transform.position += deltaPos;
                if (preparingTimer >= preparingCooldown)
                {
                    state = State.ATTACK;
                    preparingTimer = 0.0f;
                }
                break;

            case State.ATTACK:
                {
                    if(!alreadyAttack)
                    {
                        transform.position += new Vector3(-0.5f, 0, 0);
                        if (transform.position.x - spriteRenderer.bounds.size.x / 2 + offsetX < leftCameraBoundX)
                        {
                            transform.position += deltaPos;
                            alreadyAttack = true;
                            state = State.PREPARING;
                        }
                    }
                    if (alreadyAttack)
                    {
                        transform.position += new Vector3(0.5f, 0, 0);
                        if (transform.position.x + spriteRenderer.bounds.size.x /2 + offsetX < rightCameraBoundX)
                        {
                            transform.position += deltaPos;
                            alreadyAttack = false;
                            state = State.PREPARING;
                        }
                    }
                }
                    break;
        }
        previousCameraPos = Camera.main.transform.position;
          if(body.velocity.x > 0 && !isTurnedRight)
        {
            Flip();
        }
        if (body.velocity.x < 0 && isTurnedRight)
        {
            Flip();
        }
	}

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isTurnedRight = !isTurnedRight;
    }
}
