using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D body;
    BoxCollider2D m_Col;
    float fallTimer;
    float fallPeriod = 0.5f;
    bool fall = false;
    float destroyTimer;
    float destroyPeriod = 5f;
    int number;


	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        m_Col = GetComponent<BoxCollider2D>();
        fallTimer = 0.0f;
        destroyTimer = 0.0f;
       
	}
	
	void Update ()
    {
        if(fall)
        {
            fallTimer += Time.deltaTime;
            Random();
            if(number >= 1)
            {
                Fall();
            }
        }
        
        if(destroyTimer >= destroyPeriod)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            fall = true;
        }
    }
    void Random()
    {
       number = UnityEngine.Random.Range(0, 2);
    }
    void Fall()
    {
        if (fallTimer >= fallPeriod)
        {
            body.gravityScale = 1;
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
            m_Col.isTrigger = true;
            destroyTimer += Time.deltaTime;
            ///animation///
        }

    }
}
