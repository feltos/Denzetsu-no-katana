using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnnemies : AI_Manager
{
    [SerializeField]
    float speed;
    Vector2 direction;
    Vector2 movement;
    [SerializeField]
    Rigidbody2D body;
    bool detect = false;
	
	void Start ()
    {
		
	}
   
	void Update ()
    {
        movement = new Vector2(speed * direction.x, 0.0f);
        direction = (player.transform.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.transform.position) < maxRange &&
            (Vector3.Distance(transform.position, player.transform.position) > minRange))
        {
            detect = true;
            Debug.Log("detect");
        }
        if (Vector3.Distance(transform.position, player.transform.position) > maxRange &&
           (Vector3.Distance(transform.position, player.transform.position) < minRange))
        {
            detect = false;
        }


    }

    void FixedUpdate()
    {
        if(detect)
        {
            body.velocity = movement;
        }       
    }

}
