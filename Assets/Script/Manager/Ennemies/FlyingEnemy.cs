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
	
    enum State
    {
        NOT_DETECTED,
        DETECTED,
        PREPARING,
        ATTACK
    }
    State state = State.NOT_DETECTED;
	void Start ()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
		
	void Update ()
    {
        Vector3 deltaPos = Camera.main.transform.position - previousCameraPos;
        switch (state)
        {
            case State.NOT_DETECTED:
                float cameraBoundPosX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
                if(transform.position.x + spriteRenderer.bounds.size.x/2 + offsetX < cameraBoundPosX)
                {
                    state = State.DETECTED;                           
                }
                break;
                
            case State.DETECTED:
                
                if(!alreadyAttack)
                {
                    transform.position += deltaPos;
                }

                if (alreadyAttack)
                {
                    Debug.Log("detected");
                    transform.position += deltaPos *-2;
                }                    
                state = State.PREPARING;

                break;
            case State.PREPARING:
                preparingTimer += Time.deltaTime;
                if (!alreadyAttack)
                {
                    transform.position += deltaPos;
                }

                if (alreadyAttack)
                {
                    transform.position += deltaPos * -2;
                }
                if (preparingTimer >= preparingCooldown)
                {
                    state = State.ATTACK;
                    preparingTimer = 0.0f;
                }
                break;
                case State.ATTACK:
                {
                    alreadyAttack = true;
                    state = State.DETECTED;
                }
                break;
        }
        previousCameraPos = Camera.main.transform.position;
	}
}
